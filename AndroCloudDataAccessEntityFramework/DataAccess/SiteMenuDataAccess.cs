using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;
using System.Collections.Generic;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SiteMenuDataAccess : ISiteMenuDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetBySiteId(Guid siteId, DataTypeEnum dataType, out AndroCloudDataAccess.Domain.SiteMenu siteMenu)
        {
            siteMenu = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                string dataTypeString = dataType.ToString();
                var siteMenuQuery = from sm in acsEntities.SiteMenus
                                    where sm.SiteID == siteId
                                    && sm.MenuType == dataTypeString
                                    select sm;

                var siteMenuEntity = siteMenuQuery.FirstOrDefault();

                if (siteMenuEntity != null)
                {
                    siteMenu = new AndroCloudDataAccess.Domain.SiteMenu();
                    siteMenu.MenuData = siteMenuEntity.MenuData;
                    siteMenu.MenuDataThumbnails = siteMenuEntity.MenuDataThumbnails;
                    siteMenu.MenuType = siteMenuEntity.MenuType;
                    siteMenu.SiteID = siteMenuEntity.SiteID.GetValueOrDefault();
                    siteMenu.Version = siteMenuEntity.Version.GetValueOrDefault(0);
                }
            }

            return "";
        }

        public string GetMenuImagesBySiteId(Guid siteId, DataTypeEnum dataType, out string siteImages)
        {
            siteImages = "";

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                string dataTypeString = dataType.ToString();
                var siteMenuQuery = from sm in acsEntities.SiteMenus
                                    where sm.SiteID == siteId
                                    && sm.MenuType == dataTypeString
                                    select sm.MenuDataThumbnails;

                siteImages = siteMenuQuery.FirstOrDefault();
            }

            return "";
        }

        public string Put(Guid siteId, string licenseKey, string hardwareKey, string data, int version, DataTypeEnum dataType)
        {
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                string dataTypeString = dataType.ToString();
                var siteMenuQuery = from sm in acsEntities.SiteMenus
                                    where sm.SiteID == siteId
                                    && sm.MenuType == dataTypeString
                                    select sm;

                var siteMenuEntity = siteMenuQuery.FirstOrDefault();

                // Update the menu record
                if (siteMenuEntity != null)
                {
                    siteMenuEntity.MenuData = data;
                    siteMenuEntity.Version = version;
                    siteMenuEntity.LastUpdated = DateTime.UtcNow;

                    acsEntities.SaveChanges();
                }
                else
                {
                    siteMenuEntity = new Model.SiteMenu
                    {
                        MenuType = dataType.ToString(),
                        Version = version,
                        MenuData = data,
                        SiteID = siteId,
                        ID = Guid.NewGuid()
                    };

                    acsEntities.SiteMenus.Add(siteMenuEntity);

                    acsEntities.SaveChanges();
                }
            }

            return "";
        }

        public string UpdateThumbnailData(Guid siteId, string data, DataTypeEnum dataType) 
        {
            using (var dbContext = new ACSEntities()) 
            {
                DataAccessHelper.FixConnectionString(dbContext, this.ConnectionStringOverride);

                string dataTypeString = dataType.ToString();
                var table = dbContext.SiteMenus;
                var query = table
                    .Where(e => e.SiteID == siteId)
                    .Where(e => e.MenuType == dataTypeString);

                var result = query.Single();
                result.MenuDataThumbnails = data;

                dbContext.SaveChanges();
            }

            return string.Empty;
        }

        public ACSEntities AcsEntities { get; set; }
    }
}
