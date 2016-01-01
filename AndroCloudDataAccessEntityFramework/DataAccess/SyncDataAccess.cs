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
        public string Sync(CloudSyncModel.SyncModel syncModel)
        {
            string errorMessage = "";

            using (ACSEntities entitiesContext = new ACSEntities())
            {
                entitiesContext.Connection.Open();

                using (DbTransaction transaction = entitiesContext.Connection.BeginTransaction())
                {
                    // Update all stores in the local db that have changed on the server
                    foreach (CloudSyncModel.Store store in syncModel.Stores)
                    {
                        // Does the site already exist?
                        ISiteDataAccess sitesDataAccess = new SitesDataAccess();
                        AndroCloudDataAccess.Domain.Site existingSite = null;
                        sitesDataAccess.GetByAndromedaSiteId(store.AndromedaSiteId, out existingSite);

                        if (existingSite == null)
                        {
                            // Site does not exist.  Create it
                            this.AddSite(entitiesContext, store);
                        }
                        else
                        {
                            // The site already exists.  Update it
                            this.UpdateSite(entitiesContext, store);
                        }
                    }

                    // Update all partners in the local db that have changed on the server
                    foreach (CloudSyncModel.Partner partner in syncModel.Partners)
                    {
                        // Does the partner exist?
                        IPartnersDataAccess partnersDataAccess = new PartnersDataAccess();
                        AndroCloudDataAccess.Domain.Partner existingPartner = null;
                        partnersDataAccess.Get(partner.ExternalId, out existingPartner);

                        Guid partnerId = Guid.Empty;

                        if (existingPartner == null)
                        {
                            // Partner does not exist.  Create it
                            this.AddPartner(entitiesContext, partner, out partnerId);
                        }
                        else
                        {
                            // The partner already exists.  Update it
                            this.UpdatePartner(entitiesContext, partner);
                            partnerId = existingPartner.Id;
                        }

                        // Update all the partners applications
                        foreach (CloudSyncModel.Application application in partner.Applications)
                        {
                            // Does the application exist?
                            IACSApplicationDataAccess applicationDataAccess = new ACSApplicationDataAccess();
                            AndroCloudDataAccess.Domain.ACSApplication acsApplication = null;
                            applicationDataAccess.Get(application.ExternalApplicationId, out acsApplication);

                            if (acsApplication == null)
                            {
                                // Application does not exist.  Create it
                                this.AddApplication(entitiesContext, partnerId, application);
                            }
                            else
                            {
                                // The application already exists.  Update it
                                this.UpdateApplication(entitiesContext, application);
                            }

                            // Update all the sites in the application
                            string[] siteIds = application.Sites.Split(',');
                            foreach (string siteIdText in siteIds)
                            {
                                int androSiteId = 0;
                                if (Int32.TryParse(siteIdText, out androSiteId))
                                {
                                    ISiteDataAccess sitesDataAccess = new SitesDataAccess();
                                    AndroCloudDataAccess.Domain.Site existingSite = null;
                                    sitesDataAccess.GetByAndromedaSiteId(androSiteId, out existingSite);

                                    if (existingSite != null)
                                    {
                                        // Is the site associated with the application?
                                        if (!applicationDataAccess.StoreExists(existingSite.Id, acsApplication.Id))
                                        {
                                            // Site not associated with the application.  Associate it
                                            this.AddApplicationSite(entitiesContext, existingSite.Id, acsApplication.Id);
                                        }
                                    }
                                }
                            }

                            // REMOVE EXISTING SITES NOT IN siteIds
                        }
                    }

                    // Fin...
                    transaction.Commit();
                }
            }

            return errorMessage;
        }

        private void AddSite(ACSEntities entitiesContext, Store store)
        {
            Model.Site entity = new Model.Site()
            {
                AndroID = store.AndromedaSiteId,
                EstimatedDeliveryTime = null,
                ExternalId = store.ExternalSiteId,
                ExternalSiteName = store.ExternalSiteName,
                LastUpdated = DateTime.Now,
                LicenceKey = "A24C92FE-92D1-4705-8E33-202F51BCE38D",
                SiteStatus = store.StoreStatus,
                Telephone = "",
                TimeZone = ""
            };

            entitiesContext.Sites.AddObject(entity);
            entitiesContext.SaveChanges();
        }

        private void UpdateSite(ACSEntities entitiesContext, Store store)
        {
            var sitesQuery = from s in entitiesContext.Sites
                             where s.AndroID == store.AndromedaSiteId
                             select s;
            Model.Site siteEntity = sitesQuery.FirstOrDefault();

            if (siteEntity != null)
            {
                siteEntity.AndroID = store.AndromedaSiteId;
                siteEntity.EstimatedDeliveryTime = null;
                siteEntity.ExternalId = store.ExternalSiteId;
                siteEntity.ExternalSiteName = store.ExternalSiteName;
                siteEntity.LastUpdated = DateTime.Now;
                siteEntity.LicenceKey = "A24C92FE-92D1-4705-8E33-202F51BCE38D";  //harcode on server and pass in via xml
                siteEntity.SiteStatus = store.StoreStatus;
                siteEntity.Telephone = ""; // implement on server side (get from db)
                siteEntity.TimeZone = ""; // implement on server side (get from db)

                entitiesContext.Sites.AddObject(siteEntity);
                entitiesContext.SaveChanges();
            }
        }

        private void AddPartner(ACSEntities entitiesContext, CloudSyncModel.Partner partner, out Guid id)
        {
            id = Guid.Empty;

            Model.Partner entity = new Model.Partner()
            {
                ExternalId = partner.ExternalId,
                Name = partner.Name
            };

            entitiesContext.Partners.AddObject(entity);
            entitiesContext.SaveChanges();
            
            id = entity.ID;
        }

        private void UpdatePartner(ACSEntities entitiesContext, CloudSyncModel.Partner partner)
        {
            var query = from s in entitiesContext.Partners
                             where s.ExternalId == partner.ExternalId
                             select s;
            Model.Partner entity = query.FirstOrDefault();

            if (entity != null)
            {
                entity.ExternalId = partner.ExternalId;
                entity.Name = partner.Name;

                entitiesContext.Partners.AddObject(entity);
                entitiesContext.SaveChanges();
            }
        }

        private void AddApplication(ACSEntities entitiesContext, Guid partnerId, CloudSyncModel.Application application)
        {
            Model.ACSApplication entity = new Model.ACSApplication()
            {
                ExternalApplicationId = application.ExternalApplicationId,
                Name = application.Name,
                PartnerId = partnerId
            };

            entitiesContext.ACSApplications.AddObject(entity);
            entitiesContext.SaveChanges();
        }

        private void UpdateApplication(ACSEntities entitiesContext, CloudSyncModel.Application application)
        {
            var query = from s in entitiesContext.ACSApplications
                             where s.ExternalApplicationId == application.ExternalApplicationId
                             select s;
            Model.ACSApplication entity = query.FirstOrDefault();

            if (entity != null)
            {
                entity.ExternalApplicationId = application.ExternalApplicationId;
                entity.Name = application.Name;

                entitiesContext.ACSApplications.AddObject(entity);
                entitiesContext.SaveChanges();
            }
        }

        private void AddApplicationSite(ACSEntities entitiesContext, Guid siteId, Guid applicationId)
        {
            Model.ACSApplicationSite entity = new Model.ACSApplicationSite()
            {
                ACSApplicationId = applicationId,
                SiteId = siteId
            };

            entitiesContext.ACSApplicationSites.AddObject(entity);
            entitiesContext.SaveChanges();
        }
    }
}
