using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SiteMenuDataAccess : ISiteMenuDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetBySiteId(Guid siteId, DataTypeEnum dataType, out AndroCloudDataAccess.Domain.SiteMenu siteMenu)
        {
            siteMenu = null;

            //using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
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
                    siteMenu.menuData = siteMenuEntity.menuData;
                    siteMenu.MenuType = siteMenuEntity.MenuType;
                    siteMenu.SiteID = siteMenuEntity.SiteID.GetValueOrDefault();
                    siteMenu.Version = siteMenuEntity.Version.GetValueOrDefault(0);
                }
            }

            return "";
        }

        public string Put(Guid siteId, string licenseKey, string hardwareKey, string data, int version, DataTypeEnum dataType)
        {
            //using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
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
                    siteMenuEntity.menuData = data;
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
                        menuData = data,
                        SiteID = siteId,
                        ID = Guid.NewGuid()
                    };

                    acsEntities.SiteMenus.Add(siteMenuEntity);

                    acsEntities.SaveChanges();
                }
            }

            return "";
        }
    }
}
