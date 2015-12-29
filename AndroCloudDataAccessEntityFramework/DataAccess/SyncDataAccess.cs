using System;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using CloudSyncModel;
using System.Transactions;
using CloudSyncModel.HostV2;
using CloudSyncModel.Hubs;
using CloudSyncModel.Menus;
using CloudSyncModel.StoreDeviceModels;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SyncDataAccess : ISyncDataAccess
    {
        /// <summary>
        /// Gets or sets the connection string override.
        /// </summary>
        /// <value>The connection string override.</value>
        public string ConnectionStringOverride { get; set; }

        Action<string> successActions;
        Action<string> failureActions;

        public string Sync(CloudSyncModel.SyncModel syncModel, Action<string> successActions = null, Action<string> failureActions = null)
        {
            this.successActions = successActions == null ? (msg) => { } : successActions;
            this.failureActions = failureActions == null ? (msg) => { } : failureActions;

            string errorMessage = string.Empty;

            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (ACSEntities acsEntities = new ACSEntities())
                {
                    DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                    acsEntities.Database.Connection.Open();

                    // Update the data version.  We're using this as a guard to prevent multiple simultaneous syncs
                    bool success = DataVersionHelper.SetVersion(syncModel.FromDataVersion, syncModel.ToDataVersion, acsEntities);

                    // Did we successfully update the version
                    if (!success)
                    {
                        // Someone probably sneaked in and applied another sync
                        return "SetVersion failed.  From:" + syncModel.FromDataVersion + " to: " + syncModel.ToDataVersion;
                    }

                    this.SyncStorePaymentProviders(acsEntities, syncModel.StorePaymentProviders);
                    this.SyncStoreModels(acsEntities, syncModel.Stores);

                    //when syncing partners then sync applications
                    this.SyncPartners(acsEntities, syncModel.Partners, (withPartner) =>
                    {
                        //... then sync applications 
                        this.SyncPartnerApplications(acsEntities, withPartner);
                    });

                    //this.SyncHubAddresses(acsEntities, syncModel.HubUpdates, (withHub) => {
                    //    //... then sync relationship to sites
                    //    this.SyncStoreAndHubLinks(acsEntities, withHub);
                    //});

                    this.SyncHubResets(acsEntities, syncModel.HubUpdates);

                    //update the host v2 tables - types - relations
                    this.SyncHostTypes(acsEntities, syncModel.HostV2Models.HostTypes);
                    this.SyncHostV2TablesAddUpdateAndRemove(acsEntities, syncModel.HostV2Models);
                    this.SyncHostV2Relations(acsEntities, syncModel.HostV2Models);
                    // Commit the transaction

                    this.SyncStoreMenuChanges(acsEntities, syncModel.MenuUpdates);

                    this.SyncStoreDevices(acsEntities, syncModel.StoreDeviceModels);

                    transactionScope.Complete();
                }
            }

            return errorMessage;
        }

        private void SyncStoreDevices(ACSEntities acsEntities, StoreDevicesModels storeDeviceModels)
        {
            //add in apis 
            foreach (var model in storeDeviceModels.ExternalApis)
            {
                var table = acsEntities.ExternalApis;
                table.AddOrUpdate(e => e.Id == model.Id,
                    () => new ExternalApi()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Parameters = model.Parameters
                    },
                    (entity) =>
                    {
                        entity.Name = model.Name;
                        entity.Parameters = model.Parameters;
                    });
            }
            acsEntities.SaveChanges();

            //add in devices 
            foreach (var model in storeDeviceModels.Devices)
            {
                var table = acsEntities.Devices;

                table.AddOrUpdate(e => e.Id == model.Id,
                    () => new Device()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        ExternalApiId = model.ExternalApiId
                    },
                    (entity) =>
                    {
                        entity.Name = model.Name;
                        entity.ExternalApiId = model.ExternalApiId;
                    });
            }
            acsEntities.SaveChanges();


            //add in store device link.
            foreach (var model in storeDeviceModels.SiteDevices)
            {
                var table = acsEntities.SiteDevices;
                Site site = acsEntities.Sites.SingleOrDefault(s => s.AndroID == model.AndromedaSiteId);

                table.AddOrUpdate(e =>
                    e.Site.AndroID == model.AndromedaSiteId &&
                    e.DeviceId == model.DeviceId,
                    () => new SiteDevice()
                    {
                        DeviceId = model.DeviceId,
                        SiteId = site.ID,
                        Site = site,
                        Parameters = model.Parameters == null ? string.Empty : model.Parameters
                    },
                    (entity) =>
                    {
                        entity.Parameters = model.Parameters == null ? string.Empty : model.Parameters;
                        entity.DeviceId = model.DeviceId;

                    });
            }
            acsEntities.SaveChanges();
            //remove things

            foreach (var model in storeDeviceModels.RemovedDevices)
            {
                var table = acsEntities.Devices;
                table.RemoveIfExists(e => e.Id == model.Id);
            }
            acsEntities.SaveChanges();

            foreach (var model in storeDeviceModels.RemovedExternalApis)
            {
                var table = acsEntities.ExternalApis;
                table.RemoveIfExists(e => e.Id == model.Id);
            }
            acsEntities.SaveChanges();

            foreach (var model in storeDeviceModels.RemovedSiteDevices)
            {
                var table = acsEntities.SiteDevices;
                table.RemoveIfExists(e => e.DeviceId == model.DeviceId && e.Site.AndroID == model.AndromedaSiteId);
            }
            acsEntities.SaveChanges();

        }

        private void SyncHostV2Relations(ACSEntities acsEntities, HostV2Models hostV2Models)
        {
            var andromedaSiteIds = hostV2Models.StoreLinks.Select(e => e.AndromedaStoreId).Distinct().ToArray();
            var applicationIds = hostV2Models.ApplicationLinks.Select(e => e.ApplicationId).Distinct().ToArray();

            var hostV2Entities = acsEntities.HostsV2
                .ToArray();
            //get all stores mentioned in the update
            var siteEntities = acsEntities.Sites.Where(e => andromedaSiteIds.Contains(e.AndroID)).ToArray();
            //get all applications mentioned in the update
            var appplicationEntities = acsEntities.ACSApplications.Where(e => applicationIds.Contains(e.Id)).ToArray();

            //each update for a host will contain all of its relevant links for stores and applications
            //any exception will mean that a entity does not exist by an id that should be there. :-/

            //update site - host links
            var siteList = hostV2Models.StoreLinks.GroupBy(e => e.HostId).ToDictionary(e => e.Key, e => e.ToArray());
            foreach (var host in hostV2Models.Hosts)
            {
                var hostEntity = hostV2Entities.FirstOrDefault(e => e.Id == host.Id);

                if (hostEntity.Sites == null) { hostEntity.Sites = new List<Site>(); }

                if (hostEntity.Sites.Any())
                {
                    hostEntity.Sites.Clear();
                }

                if (!siteList.ContainsKey(hostEntity.Id))
                {
                    continue;
                }

                var relations = siteList[hostEntity.Id];

                foreach (var relation in relations)
                {
                    var site = siteEntities.Single(e => e.AndroID == relation.AndromedaStoreId);
                    hostEntity.Sites.Add(site);
                }
            }

            var applicationLists = hostV2Models.ApplicationLinks.GroupBy(e => e.HostId).ToDictionary(e => e.Key, e => e.ToArray());
            //update application - host links 
            foreach (var host in hostV2Models.Hosts)
            {
                var hostEntity = hostV2Entities.FirstOrDefault(e => e.Id == host.Id);
                if (hostEntity.ACSApplications == null) { hostEntity.ACSApplications = new List<ACSApplication>(); }

                if (hostEntity.ACSApplications.Any())
                {
                    hostEntity.ACSApplications.Clear();
                }

                if (!applicationLists.ContainsKey(hostEntity.Id))
                {
                    continue;
                }

                var relations = applicationLists[hostEntity.Id];

                foreach (var relation in relations)
                {
                    var application = appplicationEntities.Single(e => e.Id == relation.ApplicationId);
                    hostEntity.ACSApplications.Add(application);
                }
            }

            acsEntities.SaveChanges();
        }

        private void SyncHostV2TablesAddUpdateAndRemove(ACSEntities acsEntities, HostV2Models hostV2Models)
        {
            var hostV2Table = acsEntities.HostsV2;
            var hostTypesTableEntities = acsEntities.HostTypes.ToArray();

            var removeHostsFromTable = hostV2Models.DeletedHosts.Select(e => e.Id).ToArray();

            //don't need these at the moment or possibly ever again. 
            foreach (var host in hostV2Table.Where(e => removeHostsFromTable.Contains(e.Id)))
            {
                hostV2Table.Remove(host);
            }

            acsEntities.SaveChanges();

            var hostV2Entities = acsEntities.HostsV2.ToArray();

            //do want to add or update these. 
            foreach (var host in hostV2Models.Hosts)
            {
                var entity = hostV2Entities.FirstOrDefault(e => e.Id == host.Id);
                if (entity == null)
                {
                    hostV2Table.Add(new HostsV2()
                    {
                        Id = host.Id,
                        HostType = hostTypesTableEntities.FirstOrDefault(e => e.Name == host.HostTypeName),
                        OptInOnly = host.OptInOnly,
                        Order = host.Order,
                        Public = host.Public,
                        Url = host.Url,
                        Version = host.Version
                    });

                    continue;
                }

                entity.HostType = hostTypesTableEntities.FirstOrDefault(e => e.Name == host.HostTypeName);
                entity.OptInOnly = host.OptInOnly;
                entity.Order = host.Order;
                entity.Public = host.Public;
                entity.Url = host.Url;
                entity.Version = host.Version;
            }

            acsEntities.SaveChanges();
        }

        private void SyncHostTypes(ACSEntities acsEntities, IEnumerable<string> hostTypes)
        {
            var table = acsEntities.HostTypes;
            var tableEntities = table.ToArray();

            var addList = hostTypes.Where(e => !tableEntities.Any(entity => entity.Name.Equals(e)));

            foreach (var item in addList)
            {
                table.Add(new HostType()
                {
                    Id = Guid.NewGuid(),
                    Name = item
                });
            }

            acsEntities.SaveChanges();
        }

        private void SyncStoreMenuChanges(ACSEntities acsEntities, StoreMenuUpdates menuUpdates)
        {
            ISiteMenuDataAccess dataAccess = new SiteMenuDataAccess()
            {
                ConnectionStringOverride = this.ConnectionStringOverride
            };

            foreach (var updateMenuAction in menuUpdates.MenuChanges) 
            {
                var site = acsEntities.Sites.Single(e => e.AndroID == updateMenuAction.AndromediaSiteId);
                var menus = site.SiteMenus.ToArray();

                if (updateMenuAction.MenuType.Equals("xml", StringComparison.InvariantCultureIgnoreCase) && 
                    !string.IsNullOrWhiteSpace(updateMenuAction.Data)) 
                {
                    var xmlMenuEntity = menus
                        .Where(e => e.MenuType.Equals("xml", StringComparison.InvariantCultureIgnoreCase))
                        .SingleOrDefault();

                    if (xmlMenuEntity == null) continue;

                    xmlMenuEntity.menuData = updateMenuAction.Data;
                    xmlMenuEntity.LastUpdated = DateTime.UtcNow;
                    xmlMenuEntity.Version +=1;
                }

                if (updateMenuAction.MenuType.Equals("json", StringComparison.InvariantCultureIgnoreCase) && 
                    !string.IsNullOrWhiteSpace(updateMenuAction.Data)) 
                {
                    var jsonMenuEntity = menus
                        .Where(e => e.MenuType.Equals("json", StringComparison.InvariantCultureIgnoreCase))
                        .SingleOrDefault();

                    if (jsonMenuEntity == null) continue;

                    jsonMenuEntity.menuData = updateMenuAction.Data;
                    jsonMenuEntity.LastUpdated = DateTime.UtcNow;
                    jsonMenuEntity.Version += 1;
                }

                acsEntities.SaveChanges();
            }

            foreach (var menuUpdate in menuUpdates.MenuThumbnailChanges)
            {
                var site = acsEntities.Sites.Single(e => e.AndroID == menuUpdate.AndromediaSiteId);

                if (menuUpdate.MenuType.Equals("xml", StringComparison.InvariantCultureIgnoreCase))
                {
                    dataAccess.UpdateThumbnailData(site.ID, menuUpdate.Data, AndroCloudHelper.DataTypeEnum.XML);
                    continue;
                }

                dataAccess.UpdateThumbnailData(site.ID, menuUpdate.Data, AndroCloudHelper.DataTypeEnum.JSON);
            }
        }

        private void SyncHubResets(ACSEntities acsEntities, HubUpdates hubUpdates)
        {
            if (hubUpdates == null) { return; }
            if (hubUpdates.SiteHubHardwareKeyResets == null) { return; }

            var andromedaIds = hubUpdates.SiteHubHardwareKeyResets.Select(e => e.AndromedaSiteId).Distinct().ToArray();
            var stores = acsEntities.Sites.Where(e => andromedaIds.Contains(e.AndroID)).ToArray();

            foreach (var store in stores)
            {
                store.HardwareKey = null;
            }

            successActions(string.Format("SyncHubResets: {0} store hardware keys reset", stores.Length));

            if (andromedaIds.Length > stores.Length)
            {
                failureActions(string.Format("SyncHubResets: {0} sites were expected. But only found {1} sites", andromedaIds.Length, stores.Length));
            }

            acsEntities.SaveChanges();
        }

        //private void SyncHubAddresses(ACSEntities acsEntities, HubUpdates hubUpdates, Action<HubHostModel> withHub)
        //{
        //    IHubDataAccess hubDataAccess = new HubDataAccess() { 
        //        ConnectionStringOverride = this.ConnectionStringOverride,
        //        AcsEntities = acsEntities
        //    };

        //    if (hubUpdates == null) { return; }

        //    if (hubUpdates.InActiveHubList != null)
        //    {  
        //        //remove all of these
        //        foreach (var update in hubUpdates.InActiveHubList) 
        //        {
        //            hubDataAccess.TryToRemoveHub(update.Id);
        //        }
        //    }


        //    if (hubUpdates.ActiveHubList != null)
        //    {
        //        //make sure all these exist
        //        foreach (var model in hubUpdates.ActiveHubList) 
        //        {
        //            hubDataAccess.AddOrUpdate(model);

        //            withHub(model);
        //        }
        //    }

        //}

        //private void SyncStoreAndHubLinks(ACSEntities acsEntities, HubHostModel withHub)
        //{
        //    ISiteHubDataAccess siteHubDataAccess = new SiteHubDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride };

        //    //go and remove all the associated records
        //    siteHubDataAccess.ClearByHub(withHub);

        //    foreach (var model in withHub.SiteHubs) 
        //    {
        //        siteHubDataAccess.AddLink(model);
        //    }

        //}


        /// <summary>
        /// Syncs the applications.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="partner">The partner.</param>
        private void SyncPartnerApplications(ACSEntities acsEntities, CloudSyncModel.Partner partner)
        {
            ISiteDataAccess sitesDataAccess = new SitesDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride };

            // Update all the partners applications
            foreach (CloudSyncModel.Application application in partner.Applications)
            {
                // Does the application exist?
                IACSApplicationDataAccess applicationDataAccess = new ACSApplicationDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride };
                AndroCloudDataAccess.Domain.ACSApplication acsApplication = null;
                applicationDataAccess.GetById(application.Id, out acsApplication);

                if (acsApplication == null)
                {
                    // Application does not exist.  Create it
                    this.AddApplication(acsEntities, partner.Id, application);
                }
                else
                {
                    // The application already exists.  Update it
                    this.UpdateApplication(acsEntities, application);
                }

                // Update all the sites in the application
                string[] siteIds = application.Sites.Split(',');
                foreach (string siteIdText in siteIds)
                {
                    int androSiteId = 0;
                    if (Int32.TryParse(siteIdText, out androSiteId))
                    {
                        // Get the site that the application has permission to access
                        AndroCloudDataAccess.Domain.Site existingSite = null;
                        sitesDataAccess.GetByAndromedaSiteId(androSiteId, out existingSite);

                        if (existingSite != null)
                        {
                            var query = from acsa in acsEntities.ACSApplications
                                        join acss in acsEntities.ACSApplicationSites
                                        on acsa.Id equals acss.ACSApplicationId
                                        where acsa.Id == application.Id
                                        && acss.SiteId == existingSite.Id
                                        select acss;

                            var entity = query.FirstOrDefault();

                            // Is the site already associated with the application?
                            if (entity == null)
                            {
                                // Site not associated with the application.  Associate it
                                this.AddApplicationSite(acsEntities, existingSite.Id, application.Id);
                            }
                        }
                    }
                }

                // Is there an existing application?
                if (acsApplication != null)
                {
                    // REMOVE EXISTING SITES NOT IN siteIds

                    // Get a list of existing sites
                    var sitesQuery = from site in acsEntities.Sites
                                     join acss in acsEntities.ACSApplicationSites
                                         on site.ID equals acss.SiteId
                                     join a in acsEntities.ACSApplications
                                         on acss.ACSApplicationId equals a.Id
                                     join ss in acsEntities.SiteStatuses
                                         on site.SiteStatusID equals ss.ID
                                     where a.Id == acsApplication.Id
                                     select site;

                    //string sql = ((ObjectQuery)sitesQuery).ToTraceString();
                    //Console.WriteLine(sql);

                    IList<Model.Site> existingSites = sitesQuery.ToList();

                    if (sitesQuery != null && existingSites.Count > 0)
                    {
                        // Check each existing site to see if it's still in the application
                        foreach (Model.Site existingSite in existingSites)
                        {
                            // Is the existing site in the application?
                            if (!siteIds.Contains(existingSite.AndroID.ToString()))
                            {
                                // Site is no longer in the application
                                this.DeleteApplicationSite(acsEntities, existingSite.ID, application.Id);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Syncs the partners.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="partners">The partners.</param>
        /// <param name="withPartner">The with partner.</param>
        private void SyncPartners(ACSEntities acsEntities, List<CloudSyncModel.Partner> partners, Action<CloudSyncModel.Partner> withPartner)
        {
            // Update all partners in the local db that have changed on the master server
            foreach (CloudSyncModel.Partner partner in partners)
            {
                // Does the partner exist?
                IPartnersDataAccess partnersDataAccess = new PartnersDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride };
                AndroCloudDataAccess.Domain.Partner existingPartner = null;
                partnersDataAccess.GetById(partner.Id, out existingPartner);

                int? partnerId = null;

                if (existingPartner == null)
                {
                    // Partner does not exist.  Create it
                    this.AddPartner(acsEntities, partner, out partnerId);
                }
                else
                {
                    // The partner already exists.  Update it
                    this.UpdatePartner(acsEntities, partner);
                    partnerId = existingPartner.Id;
                }

                partner.Id = partnerId.Value;
                withPartner(partner);
            }
        }

        /// <summary>
        /// Syncs the store models.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="stores">The stores.</param>
        private void SyncStoreModels(ACSEntities acsEntities, List<Store> stores)
        {
            // Update all stores in the local db that have changed on the master server
            foreach (CloudSyncModel.Store store in stores)
            {
                // Does the site already exist?
                ISiteDataAccess sitesDataAccess = new SitesDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride };
                AndroCloudDataAccess.Domain.Site existingSite = null;
                sitesDataAccess.GetByAndromedaSiteId(store.AndromedaSiteId, out existingSite);

                if (existingSite == null)
                {
                    // Site does not exist.  Create it
                    this.AddSite(acsEntities, store);
                }
                else
                {
                    // The site already exists.  Update it
                    this.UpdateSite(acsEntities, store);
                }
            }
        }

        /// <summary>
        /// Syncs the store payment providers.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="storePaymentProviders">The store payment providers.</param>
        private void SyncStorePaymentProviders(ACSEntities acsEntities, List<CloudSyncModel.StorePaymentProvider> storePaymentProviders)
        {
            // Update all store payment providers in the local db that have changed on the master server
            foreach (CloudSyncModel.StorePaymentProvider storePaymentProvider in storePaymentProviders)
            {
                // Does the store payment provider already exist?
                IStorePaymentProviderDataAccess storePaymentProviderDataAccess = new StorePaymentProviderDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride };

                var existingStorePaymentProvider = acsEntities.StorePaymentProviders.Where(e => e.ID == storePaymentProvider.Id).FirstOrDefault();

                if (existingStorePaymentProvider == null)
                {
                    // Store payment provider does not exist.  Create it
                    this.AddStorePaymentProvider(acsEntities, storePaymentProvider);
                }
                else
                {
                    // The store payment provider already exists.  Update it
                    this.UpdateStorePaymentProvider(acsEntities, existingStorePaymentProvider, storePaymentProvider);
                }
            }
        }

        /// <summary>
        /// Adds the store payment provider.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="storePaymentProvider">The store payment provider.</param>
        private void AddStorePaymentProvider(ACSEntities acsEntities, CloudSyncModel.StorePaymentProvider storePaymentProvider)
        {
            Model.StorePaymentProvider entity = new Model.StorePaymentProvider()
            {
                ClientId = storePaymentProvider.ClientId,
                ClientPassword = storePaymentProvider.ClientPassword,
                ID = storePaymentProvider.Id,
                ProviderName = storePaymentProvider.ProviderName
            };

            acsEntities.StorePaymentProviders.Add(entity);
            acsEntities.SaveChanges();
        }

        /// <summary>
        /// Updates the store payment provider.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="existingStorePaymentProvider">The existing store payment provider.</param>
        /// <param name="storePaymentProvider">The store payment provider.</param>
        private void UpdateStorePaymentProvider(ACSEntities acsEntities, Model.StorePaymentProvider existingStorePaymentProvider, CloudSyncModel.StorePaymentProvider storePaymentProvider)
        {
            existingStorePaymentProvider.ClientId = storePaymentProvider.ClientId;
            existingStorePaymentProvider.ClientPassword = storePaymentProvider.ClientPassword;
            existingStorePaymentProvider.ProviderName = storePaymentProvider.ProviderName;

            acsEntities.SaveChanges();
        }

        /// <summary>
        /// Adds the site.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="store">The store.</param>
        private void AddSite(ACSEntities acsEntities, Store store)
        {
            // Get the site status
            var siteStatusQuery = from s in acsEntities.SiteStatuses
                                  where s.Status == store.StoreStatus
                                  select s;

            Model.SiteStatus siteStatusEntity = siteStatusQuery.FirstOrDefault();

            if (siteStatusEntity == null)
            {
                //TODO ERROR??
            }

            // Get the country
            var countryQuery = from s in acsEntities.Countries
                               where s.Id == store.Address.CountryId
                               select s;

            Model.Country countryEntity = countryQuery.FirstOrDefault();

            if (countryEntity == null)
            {
                //TODO ERROR??
            }

            // Create the address
            Model.Address addressEntity = new Model.Address()
            {
                ID = Guid.NewGuid(),
                Country = countryEntity,
                County = store.Address.County,
                DPS = store.Address.DPS,
                Lat = store.Address.Lat,
                Locality = store.Address.Locality,
                Long = store.Address.Long,
                Org1 = store.Address.Org1,
                Org2 = store.Address.Org2,
                Org3 = store.Address.Org3,
                PostCode = store.Address.PostCode,
                Prem1 = store.Address.Prem1,
                Prem2 = store.Address.Prem2,
                Prem3 = store.Address.Prem3,
                Prem4 = store.Address.Prem4,
                Prem5 = store.Address.Prem5,
                Prem6 = store.Address.Prem6,
                RoadName = store.Address.RoadName,
                RoadNum = store.Address.RoadNum,
                State = store.Address.State,
                Town = store.Address.Town
            };

            acsEntities.Addresses.Add(addressEntity);
            acsEntities.SaveChanges();

            int? storePaymentProviderId = null;
            int storePaymentProviderIdTemp = 0;
            if (store.StorePaymentProviderId != null && int.TryParse(store.StorePaymentProviderId, out storePaymentProviderIdTemp))
            {
                storePaymentProviderId = storePaymentProviderIdTemp;
            }

            // Create the site
            Model.Site siteEntity = new Model.Site()
            {
                ID = Guid.NewGuid(),
                AddressID = addressEntity.ID,
                AndroID = store.AndromedaSiteId,
                EstimatedDeliveryTime = null,
                ExternalId = store.ExternalSiteId,
                ExternalSiteName = store.ExternalSiteName,
                LastUpdated = DateTime.Now,
                LicenceKey = "A24C92FE-92D1-4705-8E33-202F51BCE38D",
                SiteStatus = siteStatusEntity,
                Telephone = store.Phone,
                TimeZone = store.TimeZone,
                StorePaymentProviderID = storePaymentProviderId
            };

            acsEntities.Sites.Add(siteEntity);
            acsEntities.SaveChanges();
        }

        /// <summary>
        /// Updates the site.
        /// </summary>
        /// <param name="acsEntities">The acs entities.</param>
        /// <param name="store">The store.</param>
        private void UpdateSite(ACSEntities acsEntities, Store store)
        {
            // Get the site status
            var siteStatusQuery = from s in acsEntities.SiteStatuses
                                  where s.Status == store.StoreStatus
                                  select s;
            Model.SiteStatus siteStatusEntity = siteStatusQuery.FirstOrDefault();

            if (siteStatusEntity == null)
            {
                //TODO ERROR??
            }

            // Get the site so we can update it
            var sitesQuery = from s in acsEntities.Sites
                             where s.AndroID == store.AndromedaSiteId
                             select s;

            Model.Site siteEntity = sitesQuery.FirstOrDefault();

            if (siteEntity != null)
            {
                // Update the address
                var addressStatusQuery = from s in acsEntities.Addresses
                                         where s.ID == siteEntity.AddressID
                                         select s;
                Model.Address addressEntity = addressStatusQuery.FirstOrDefault();

                if (addressStatusQuery == null)
                {
                    //TODO ERROR??
                }

                // Get the country
                var countryQuery = from s in acsEntities.Countries
                                   where s.Id == store.Address.CountryId
                                   select s;
                Model.Country countryEntity = countryQuery.FirstOrDefault();

                if (countryEntity == null)
                {
                    //TODO ERROR??
                }

                // Update the address
                if (addressEntity == null)
                {
                    // No address - we need to create one
                    addressEntity = new Model.Address()
                    {
                        ID = Guid.NewGuid(),
                        County = string.Empty,
                        DPS = string.Empty,
                        Lat = string.Empty,
                        Locality = string.Empty,
                        Long = string.Empty,
                        Org1 = string.Empty,
                        Org2 = string.Empty,
                        Org3 = string.Empty,
                        PostCode = string.Empty,
                        Prem1 = string.Empty,
                        Prem2 = string.Empty,
                        Prem3 = string.Empty,
                        Prem4 = string.Empty,
                        Prem5 = string.Empty,
                        Prem6 = string.Empty,
                        RoadName = string.Empty,
                        RoadNum = string.Empty,
                        State = string.Empty,
                        Town = string.Empty,
                        Country = countryEntity
                    };

                    acsEntities.Addresses.Add(addressEntity);
                }
                else
                {
                    addressEntity.Country = countryEntity;
                    addressEntity.County = store.Address.County;
                    addressEntity.DPS = store.Address.DPS;
                    addressEntity.Lat = store.Address.Lat;
                    addressEntity.Locality = store.Address.Locality;
                    addressEntity.Long = store.Address.Long;
                    addressEntity.Org1 = store.Address.Org1;
                    addressEntity.Org2 = store.Address.Org2;
                    addressEntity.Org3 = store.Address.Org3;
                    addressEntity.PostCode = store.Address.PostCode;
                    addressEntity.Prem1 = store.Address.Prem1;
                    addressEntity.Prem2 = store.Address.Prem2;
                    addressEntity.Prem3 = store.Address.Prem3;
                    addressEntity.Prem4 = store.Address.Prem4;
                    addressEntity.Prem5 = store.Address.Prem5;
                    addressEntity.Prem6 = store.Address.Prem6;
                    addressEntity.RoadName = store.Address.RoadName;
                    addressEntity.RoadNum = store.Address.RoadNum;
                    addressEntity.State = store.Address.State;
                    addressEntity.Town = store.Address.Town;
                }

                // Update the payment provider
                int? storePaymentProviderId = null;
                int storePaymentProviderIdTemp = 0;
                if (store.StorePaymentProviderId != null && int.TryParse(store.StorePaymentProviderId, out storePaymentProviderIdTemp))
                {
                    storePaymentProviderId = storePaymentProviderIdTemp;
                }

                // Remove all opening hours
                if (siteEntity.OpeningHours != null)
                {
                    // Get the opening hours to delete
                    List<OpeningHour> openingHoursToDelete = new List<OpeningHour>();
                    foreach (OpeningHour openingHour in siteEntity.OpeningHours)
                    {
                        openingHoursToDelete.Add(openingHour);
                    }

                    // Delete the opening hours
                    foreach (OpeningHour openingHour in openingHoursToDelete)
                    {
                        acsEntities.OpeningHours.Remove(openingHour);
                    }
                }

                // Update the site details
                siteEntity.AndroID = store.AndromedaSiteId;
                siteEntity.EstimatedDeliveryTime = null;
                siteEntity.ExternalId = store.ExternalSiteId;
                siteEntity.ExternalSiteName = store.ExternalSiteName;
                siteEntity.LastUpdated = DateTime.Now;
                siteEntity.LicenceKey = "A24C92FE-92D1-4705-8E33-202F51BCE38D";  // hardcode on server and pass in via xml
                siteEntity.SiteStatus = siteStatusEntity;
                siteEntity.Telephone = store.Phone;
                siteEntity.TimeZone = store.TimeZone;
                siteEntity.StorePaymentProviderID = storePaymentProviderId;

                // Commit the first lot of changes
                acsEntities.SaveChanges();

                // Add the correct opening hours back in
                foreach (CloudSyncModel.TimeSpanBlock timeSpanBlock in store.OpeningHours)
                {
                    // Get the day
                    var daysQuery = from d in acsEntities.Days
                                    where d.Description == timeSpanBlock.Day
                                    select d;

                    Day dayEntity = daysQuery.FirstOrDefault();

                    // Take the textual representation of the start and end time and split them into seperate times
                    TimeSpan startTimeSpan = new TimeSpan();
                    TimeSpan endTimeSpan = new TimeSpan();

                    if (!timeSpanBlock.OpenAllDay)
                    {
                        string[] startTimeBits = timeSpanBlock.StartTime.Split(':');
                        startTimeSpan = new TimeSpan(int.Parse(startTimeBits[0]), int.Parse(startTimeBits[1]), 0);
                        string[] endTimeBits = timeSpanBlock.EndTime.Split(':');
                        endTimeSpan = new TimeSpan(int.Parse(endTimeBits[0]), int.Parse(endTimeBits[1]), 0);
                    }

                    // Create an object we can add
                    Model.OpeningHour openingHour = new Model.OpeningHour();

                    openingHour.Day = dayEntity;
                    openingHour.OpenAllDay = timeSpanBlock.OpenAllDay;
                    openingHour.SiteID = siteEntity.ID;
                    openingHour.TimeStart = startTimeSpan;
                    openingHour.TimeEnd = endTimeSpan;
                    openingHour.ID = Guid.NewGuid();

                    // Add the opening times
                    acsEntities.OpeningHours.Add(openingHour);
                }

                // Commit the first lot of changes
                acsEntities.SaveChanges();
            }
        }

        private void AddPartner(ACSEntities acsEntities, CloudSyncModel.Partner partner, out int? id)
        {
            id = null;

            Model.Partner entity = new Model.Partner()
            {
                Id = partner.Id,
                ExternalId = partner.ExternalId,
                Name = partner.Name
            };

            acsEntities.Partners.Add(entity);
            acsEntities.SaveChanges();

            id = entity.Id;
        }

        private void UpdatePartner(ACSEntities acsEntities, CloudSyncModel.Partner partner)
        {
            var query = from s in acsEntities.Partners
                        where s.Id == partner.Id
                        select s;

            Model.Partner entity = query.FirstOrDefault();

            if (entity != null)
            {
                entity.ExternalId = partner.ExternalId;
                entity.Name = partner.Name;

                acsEntities.SaveChanges();
            }
        }

        private void AddApplication(ACSEntities acsEntities, int partnerId, CloudSyncModel.Application application)
        {
            Model.ACSApplication entity = new Model.ACSApplication()
            {
                Id = application.Id,
                ExternalApplicationId = application.ExternalApplicationId,
                Name = application.Name,
                PartnerId = partnerId,
                ExternalDisplayName = application.ExternalDisplayName
            };

            acsEntities.ACSApplications.Add(entity);
            acsEntities.SaveChanges();
        }

        private void UpdateApplication(ACSEntities acsEntities, CloudSyncModel.Application application)
        {
            var query = from s in acsEntities.ACSApplications
                        where s.Id == application.Id
                        select s;
            Model.ACSApplication entity = query.FirstOrDefault();

            if (entity != null)
            {
                entity.ExternalApplicationId = application.ExternalApplicationId;
                entity.Name = application.Name;
                entity.ExternalDisplayName = application.ExternalDisplayName;

                acsEntities.SaveChanges();
            }
        }

        private void AddApplicationSite(ACSEntities acsEntities, Guid siteId, int applicationId)
        {
            Model.ACSApplicationSite entity = new Model.ACSApplicationSite()
            {
                ACSApplicationId = applicationId,
                SiteId = siteId
            };

            acsEntities.ACSApplicationSites.Add(entity);
            acsEntities.SaveChanges();
        }

        private void DeleteApplicationSite(ACSEntities acsEntities, Guid siteId, int applicationId)
        {
            var query = from s in acsEntities.ACSApplicationSites
                        where s.ACSApplicationId == applicationId
                        && s.SiteId == siteId
                        select s;
            Model.ACSApplicationSite entity = query.FirstOrDefault();

            if (entity != null)
            {
                acsEntities.ACSApplicationSites.Remove(entity);
                acsEntities.SaveChanges();
            }
        }
    }
}
