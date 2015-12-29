﻿using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudWCFHelper;
using AndroCloudHelper;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;

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
            string deliveryZoneFilter,
            DataTypeEnum dataType, 
            out List<AndroCloudDataAccess.Domain.Site> sites)
        {
            sites = new List<AndroCloudDataAccess.Domain.Site>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);
                string dataTypeString = dataType.ToString();

                if (deliveryZoneFilter == null || deliveryZoneFilter.Length == 0)
                {
                    // Don't filter by delivery zone
                    var sitesQuery = from s in acsEntities.Sites.Include(e => e.SiteLoyalties)
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
                            GetLoyaltyConfiguration(site, siteEntity);

                            sites.Add(site);
                        }
                    }
                }
                else
                {
                    // Get rid of spaces
                    deliveryZoneFilter = deliveryZoneFilter.Replace(" ", "").Trim();

                    // Filter by delivery zone
                    var sitesQuery = from s in acsEntities.Sites
                                     join acss in acsEntities.ACSApplicationSites
                                       on s.ID equals acss.SiteId
                                     join a in acsEntities.ACSApplications
                                       on acss.ACSApplicationId equals a.Id
                                     join sm in acsEntities.SiteMenus
                                       on s.ID equals sm.SiteID
                                     join ss in acsEntities.SiteStatuses
                                       on s.SiteStatusID equals ss.ID
                                     join da in acsEntities.DeliveryAreas
                                       on s.ID equals da.SiteId
                                     where sm.MenuType == dataTypeString
                                       && a.Id == applicationId
                                       && ss.Status == "Live"
                                       && deliveryZoneFilter.StartsWith(da.DeliveryArea1.ToUpper().Replace(" ", "").Trim())
                                       //&& da.DeliveryArea1.ToUpper().Replace(" ", "").Trim() == (deliveryZoneFilter.Length > da.DeliveryArea1.Replace(" ", "").Trim().Length ? deliveryZoneFilter.Substring(0, da.DeliveryArea1.Replace(" ", "").Trim().Length).ToUpper() : "")
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
                            GetLoyaltyConfiguration(site, siteEntity);

                            sites.Add(site);
                        }
                    }
                }
            }

            return "";
        }

        public string GetById(Guid siteId, out AndroCloudDataAccess.Domain.Site site)
        {
            site = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var sitesQuery = from s in acsEntities.Sites.Include(e => e.SiteLoyalties)
                                 join ss in acsEntities.SiteStatuses
                                   on s.SiteStatusID equals ss.ID
                                 where s.ID == siteId
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
                    GetLoyaltyConfiguration(site, siteEntity);
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

                var sitesQuery = from s in acsEntities.Sites.Include(e => e.SiteLoyalties)
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
                    GetLoyaltyConfiguration(site, siteEntity);
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

                var sitesQuery = from s in acsEntities.Sites.Include(e => e.SiteLoyalties)
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
                    GetLoyaltyConfiguration(site, siteEntity);
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

                var sitesQuery = from s in acsEntities.Sites.Include(e => e.SiteLoyalties)
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
                    GetLoyaltyConfiguration(site, siteEntity);
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

                var sitesQuery = from s in acsEntities.Sites.Include(e => e.SiteLoyalties)
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
                                 select new { s.ID, s.EstimatedDeliveryTime, s.StoreConnected, sm.Version, s.ExternalSiteName, s.ExternalId, s.LicenceKey, s.AndroID, s.SiteLoyalties };

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
                    GetLoyaltyConfiguration(site, siteEntity);
                }
            }

            return "";
        }

        public string Update(int andromedaSiteId, int etd)
        {
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from o in acsEntities.Sites
                               where o.AndroID == andromedaSiteId
                               select o;

                var acsQueryEntity = acsQuery.FirstOrDefault();

                // Update the site record
                if (acsQueryEntity != null)
                {
                    acsQueryEntity.EstimatedDeliveryTime = etd;
                    acsEntities.SaveChanges();
                }
            }

            return "";
        }

        private void GetLoyaltyConfiguration(AndroCloudDataAccess.Domain.Site site, dynamic siteEntity)
        {
            site.SiteLoyalties = new List<AndroCloudDataAccess.Domain.SiteLoyalty>();
            try
            {
                if (siteEntity.GetType().GetProperty("SiteLoyalties") != null && siteEntity.SiteLoyalties != null)
                {
                    foreach (var config in siteEntity.SiteLoyalties)
                    {
                        AndroCloudDataAccess.Domain.SiteLoyalty siteConfig = new AndroCloudDataAccess.Domain.SiteLoyalty();
                        siteConfig.Id = config.Id;
                        siteConfig.SiteId = config.SiteId;
                        siteConfig.Configuration = config.Configuration;
                        siteConfig.ProviderName = config.ProviderName;
                        if (!string.IsNullOrEmpty(config.Configuration))
                        {
                            siteConfig.ConfigurationTypes = new LoyaltyConfiguration();
                            //try
                            //{
                                siteConfig.ConfigurationTypes = JsonConvert.DeserializeObject<LoyaltyConfiguration>(config.Configuration);
                            //}
                            //catch (Exception ex)
                            //{
                            //    // log - 
                            //}
                        }
                        if (siteConfig.ConfigurationTypes != null && siteConfig.ConfigurationTypes.isEnabled)
                        {
                            site.SiteLoyalties.Add(siteConfig);
                        }
                    }
                }
            }
            catch (Exception ex)
            { 
                // log - 
            }
        }
    }
}
