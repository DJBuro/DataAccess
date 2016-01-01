using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudWCFHelper;
using AndroCloudHelper;
using CloudSyncModel;
using System.Data.Common;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SyncDataAccess : ISyncDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string Sync(CloudSyncModel.SyncModel syncModel)
        {
            string errorMessage = "";

            using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
            {
                acsEntities.Connection.Open();

                using (DbTransaction transaction = acsEntities.Connection.BeginTransaction())
                {
                    // Update the data version.  We're using this as a guard to prevent multiple simultanous syncs
                    bool success = DataVersionHelper.SetVersion(syncModel.FromDataVersion, syncModel.ToDataVersion, acsEntities, transaction);

                    // Did we successfully update the version
                    if (!success)
                    {
                        // Someone probably sneaked in and applied another sync
                        transaction.Rollback();

                        return "SetVersion failed.  From:" + syncModel.FromDataVersion + " to: " + syncModel.ToDataVersion;
                    }

                    // Update all stores in the local db that have changed on the server
                    foreach (CloudSyncModel.Store store in syncModel.Stores)
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

                    // Update all partners in the local db that have changed on the server
                    foreach (CloudSyncModel.Partner partner in syncModel.Partners)
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
                                this.AddApplication(acsEntities, partnerId.Value, application);
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
                                var sitesQuery = from s in acsEntities.Sites
                                                     join acss in acsEntities.ACSApplicationSites
                                                       on s.ID equals acss.SiteId
                                                     join a in acsEntities.ACSApplications
                                                       on acss.ACSApplicationId equals a.Id
                                                     join ss in acsEntities.SiteStatuses
                                                       on s.SiteStatusID equals ss.ID
                                                     where a.Id == acsApplication.Id
                                                     select s;

                                //string sql = ((ObjectQuery)sitesQuery).ToTraceString();
                                //Console.WriteLine(sql);

                                List<Model.Site> existingSites = sitesQuery.ToList();

                                if (sitesQuery != null && existingSites.Count > 0)
                                {
                                    // Check each existing site to see if it's still in the application
                                    foreach (Model.Site existingSite in sitesQuery)
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

                    // Fin...
                    transaction.Commit();
                }
            }

            return errorMessage;
        }

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

            acsEntities.Addresses.AddObject(addressEntity);
            acsEntities.SaveChanges();

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
                TimeZone = store.TimeZone
            };

            acsEntities.Sites.AddObject(siteEntity);
            acsEntities.SaveChanges();
        }

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

                siteEntity.AndroID = store.AndromedaSiteId;
                siteEntity.EstimatedDeliveryTime = null;
                siteEntity.ExternalId = store.ExternalSiteId;
                siteEntity.ExternalSiteName = store.ExternalSiteName;
                siteEntity.LastUpdated = DateTime.Now;
                siteEntity.LicenceKey = "A24C92FE-92D1-4705-8E33-202F51BCE38D";  // harcode on server and pass in via xml
                siteEntity.SiteStatus = siteStatusEntity;
                siteEntity.Telephone = store.Phone;
                siteEntity.TimeZone = store.TimeZone;

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

            acsEntities.Partners.AddObject(entity);
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
                PartnerId = partnerId
            };

            acsEntities.ACSApplications.AddObject(entity);
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

            acsEntities.ACSApplicationSites.AddObject(entity);
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
                acsEntities.ACSApplicationSites.DeleteObject(entity);
                acsEntities.SaveChanges();
            }
        }
    }
}
