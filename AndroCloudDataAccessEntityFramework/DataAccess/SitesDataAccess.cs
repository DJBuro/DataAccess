using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SitesDataAccess : ISiteDataAccess
    {
        public string GetByFilter(
            Guid partnerId, 
            Guid? groupId, 
            float? maxDistance, 
            float? longitude, 
            float? latitude, 
            DataTypeEnum dataType, 
            out List<AndroCloudDataAccess.Domain.Site> sites)
        {
            sites = new List<AndroCloudDataAccess.Domain.Site>();
            ACSEntities acsEntities = new ACSEntities();

            string dataTypeString = dataType.ToString();
            var sitesQuery = from s in acsEntities.Sites
                             join sg in acsEntities.SitesGroups
                             on s.ID equals sg.SiteID
                             join g in acsEntities.Groups
                             on sg.GroupID equals g.ID
                             join p in acsEntities.Partners
                             on g.PartnerID equals p.ID
                             join sm in acsEntities.SiteMenus
                             on s.ID equals sm.SiteID
                             where sg.GroupID == (groupId.HasValue ? groupId : sg.GroupID)
                             && sm.MenuType == dataTypeString
                             && p.ID == partnerId
                             select new { s.ID, s.EstimatedDeliveryTime, s.StoreConnected, sm.Version, s.SiteName, s.ExternalId, s.LicenceKey };

            var siteEntities = sitesQuery.ToList();

            foreach (var siteEntity in siteEntities)
            {
                AndroCloudDataAccess.Domain.Site site = new AndroCloudDataAccess.Domain.Site();
                site.Id = siteEntity.ID;
                site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                site.MenuVersion = siteEntity.Version.GetValueOrDefault(0);
                site.Name = siteEntity.SiteName;
                site.ExternalId = siteEntity.ExternalId;
                site.LicenceKey = siteEntity.LicenceKey;

                sites.Add(site);
            }

            return "";
        }

        public string GetByExternalSiteId(string externalSiteId, out AndroCloudDataAccess.Domain.Site site)
        {
            site = null;
            ACSEntities acsEntities = new ACSEntities();

            var sitesQuery = from s in acsEntities.Sites
                             where s.ExternalId == externalSiteId
                             select s;
            Model.Site siteEntity = sitesQuery.FirstOrDefault();

            if (siteEntity != null)
            {
                site = new AndroCloudDataAccess.Domain.Site();
                site.Id = siteEntity.ID;
                site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                site.MenuVersion = 0;
                site.Name = siteEntity.SiteName;
                site.ExternalId = siteEntity.ExternalId;
                site.LicenceKey = siteEntity.LicenceKey;
            }

            return "";
        }

        public string GetByIdAndPartner(Guid partnerId, Guid siteId, out AndroCloudDataAccess.Domain.Site site)
        {
            site = null;
            ACSEntities acsEntities = new ACSEntities();

            var sitesQuery = from s in acsEntities.Sites
                             join sg in acsEntities.SitesGroups
                             on s.ID equals sg.SiteID
                             join g in acsEntities.Groups
                             on sg.GroupID equals g.ID
                             join p in acsEntities.Partners
                             on g.PartnerID equals p.ID
                             join sm in acsEntities.SiteMenus
                             on s.ID equals sm.SiteID
                             where p.ID == partnerId
                             && s.ID == siteId
                             select new { s.ID, s.EstimatedDeliveryTime, s.StoreConnected, sm.Version, s.SiteName, s.ExternalId, s.LicenceKey };

            var siteEntity = sitesQuery.FirstOrDefault();

            if (siteEntity != null)
            {
                site = new AndroCloudDataAccess.Domain.Site();
                site.Id = siteEntity.ID;
                site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                site.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                site.MenuVersion = siteEntity.Version.GetValueOrDefault(0);
                site.Name = siteEntity.SiteName;
                site.ExternalId = siteEntity.ExternalId;
                site.LicenceKey = siteEntity.LicenceKey;
            }

            return "";
        }
    }
}
