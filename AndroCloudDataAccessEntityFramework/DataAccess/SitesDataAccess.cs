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

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SitesDataAccess : ISiteDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetByFilter(
            int applicationId, 
            float? filterByMaxDistance, 
            double? filterByLongitude,
            double? filterByLatitude, 
            DataTypeEnum dataType, 
            out List<AndroCloudDataAccess.Domain.Site> sites)
        {
            sites = new List<AndroCloudDataAccess.Domain.Site>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                string dataTypeString = dataType.ToString();
                var sitesQuery = from s in acsEntities.Sites
                                 join acss in acsEntities.ACSApplicationSites
                                   on s.ID equals acss.SiteId
                                 join a in acsEntities.ACSApplications
                                   on acss.ACSApplicationId equals a.Id
                                 join sm in acsEntities.SiteMenus
                                   on s.ID equals sm.SiteID
                                 join ss in acsEntities.SiteStatuses
                                   on s.SiteStatusID equals ss.ID
                                 where sm.MenuType == dataTypeString
                                   && a.Id == applicationId
                                   && ss.Status == "Live"
                                 select new
                                 {
                                     s.ID,
                                     s.EstimatedDeliveryTime,
                                     s.StoreConnected,
                                     sm.Version,
                                     s.ExternalSiteName,
                                     s.ExternalId,
                                     s.LicenceKey,
                                     s.Address.Lat,
                                     s.Address.Long,
                                     s.AndroID
                                 };

                var siteEntities = sitesQuery.ToList();

                foreach (var siteEntity in siteEntities)
                {
                    bool returnSite = true;

                    // Do we need to filter by distance i.e. only return the closest X stores?
                    if (filterByMaxDistance != null && filterByLongitude != null && filterByLatitude != null)
                    {
                        double storeLatitude = 0;
                        double storeLongitude = 0;

                        // Do we have the location of the site?
                        if (siteEntity.Lat == null ||
                            siteEntity.Long == null ||
                            filterByLatitude == null || 
                            filterByLongitude == null ||
                            !double.TryParse(siteEntity.Long, out storeLongitude) || 
                            !double.TryParse(siteEntity.Lat, out storeLatitude))
                        {
                            // Don't know where the site is so don't return it
                            returnSite = false;
                        }
                        else
                        {
                            // Calculate the distance between the site and the customer
                            double distance = SpacialHelper.CalcDistanceBetweenTwoPoints(filterByLongitude.Value, filterByLatitude.Value, storeLongitude, storeLatitude);

                            // Is the site within X km of the customer?
                            if (distance > filterByMaxDistance)
                            {
                                // Out of range - don't return the site
                                returnSite = false;
                            }
                        }
                    }

                    if (returnSite)
                    {
                        AndroCloudDataAccess.Domain.Site site = new AndroCloudDataAccess.Domain.Site();
                        site.Id = siteEntity.ID;
                        site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                        site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                        site.MenuVersion = siteEntity.Version.GetValueOrDefault(0);
                        site.Name = siteEntity.ExternalSiteName;
                        site.ExternalId = siteEntity.ExternalId;
                        site.LicenceKey = siteEntity.LicenceKey;
                        site.AndroId = siteEntity.AndroID;

                        sites.Add(site);
                    }
                }
            }

            return "";
        }

        public string GetByExternalSiteId(string externalSiteId, out AndroCloudDataAccess.Domain.Site site)
        {
            site = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var sitesQuery = from s in acsEntities.Sites
                                 join ss in acsEntities.SiteStatuses
                                   on s.SiteStatusID equals ss.ID
                                 where s.ExternalId == externalSiteId
                                   && ss.Status == "Live"
                                 select s;
                Model.Site siteEntity = sitesQuery.FirstOrDefault();

                if (siteEntity != null)
                {
                    site = new AndroCloudDataAccess.Domain.Site();
                    site.Id = siteEntity.ID;
                    site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                    site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                    site.MenuVersion = 0;
                    site.Name = siteEntity.ExternalSiteName;
                    site.ExternalId = siteEntity.ExternalId;
                    site.LicenceKey = siteEntity.LicenceKey;
                    site.AndroId = siteEntity.AndroID;
                }
            }

            return "";
        }

        public string GetByAndromedaSiteIdAndLive(int andromedaSiteId, out AndroCloudDataAccess.Domain.Site site)
        {
            site = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var sitesQuery = from s in acsEntities.Sites
                                 join ss in acsEntities.SiteStatuses
                                   on s.SiteStatusID equals ss.ID
                                 where s.AndroID == andromedaSiteId
                                   && ss.Status == "Live"
                                 select s;
                Model.Site siteEntity = sitesQuery.FirstOrDefault();

                if (siteEntity != null)
                {
                    site = new AndroCloudDataAccess.Domain.Site();
                    site.Id = siteEntity.ID;
                    site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                    site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                    site.MenuVersion = 0;
                    site.Name = siteEntity.ExternalSiteName;
                    site.ExternalId = siteEntity.ExternalId;
                    site.LicenceKey = siteEntity.LicenceKey;
                    site.AndroId = siteEntity.AndroID;
                }
            }

            return "";
        }

        public string GetByAndromedaSiteId(int andromedaSiteId, out AndroCloudDataAccess.Domain.Site site)
        {
            site = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var sitesQuery = from s in acsEntities.Sites
                                 join ss in acsEntities.SiteStatuses
                                   on s.SiteStatusID equals ss.ID
                                 where s.AndroID == andromedaSiteId
                                 select s;
                Model.Site siteEntity = sitesQuery.FirstOrDefault();

                if (siteEntity != null)
                {
                    site = new AndroCloudDataAccess.Domain.Site();
                    site.Id = siteEntity.ID;
                    site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                    site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                    site.MenuVersion = 0;
                    site.Name = siteEntity.ExternalSiteName;
                    site.ExternalId = siteEntity.ExternalId;
                    site.LicenceKey = siteEntity.LicenceKey;
                    site.AndroId = siteEntity.AndroID;
                }
            }

            return "";
        }

        public string GetByIdAndApplication(int applicationId, Guid siteId, out AndroCloudDataAccess.Domain.Site site)
        {
            site = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var sitesQuery = from s in acsEntities.Sites
                                 join acss in acsEntities.ACSApplicationSites
                                   on s.ID equals acss.SiteId
                                 join a in acsEntities.ACSApplications
                                   on acss.ACSApplicationId equals a.Id
                                 join sm in acsEntities.SiteMenus
                                   on s.ID equals sm.SiteID
                                 join ss in acsEntities.SiteStatuses
                                   on s.SiteStatusID equals ss.ID
                                 where a.Id == applicationId
                                   && s.ID == siteId
                                   && ss.Status == "Live"
                                 select new { s.ID, s.EstimatedDeliveryTime, s.StoreConnected, sm.Version, s.ExternalSiteName, s.ExternalId, s.LicenceKey, s.AndroID };

       //         string x = ((ObjectQuery)sitesQuery).ToTraceString();
       //         Console.WriteLine(x);

                var siteEntity = sitesQuery.FirstOrDefault();

                if (siteEntity != null)
                {
                    site = new AndroCloudDataAccess.Domain.Site();
                    site.Id = siteEntity.ID;
                    site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                    site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                    site.MenuVersion = siteEntity.Version.GetValueOrDefault(0);
                    site.Name = siteEntity.ExternalSiteName;
                    site.ExternalId = siteEntity.ExternalId;
                    site.LicenceKey = siteEntity.LicenceKey;
                    site.AndroId = siteEntity.AndroID;
                }
            }

            return "";
        }
    }
}
