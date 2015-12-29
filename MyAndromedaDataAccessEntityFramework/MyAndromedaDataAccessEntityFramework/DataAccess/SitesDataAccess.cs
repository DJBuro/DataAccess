using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Collections.Generic;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SitesDataAccess : ISiteDataAccess
    {
        public string GetById(int siteId, out MyAndromedaDataAccess.Domain.Site site)
        {
            site = null;
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var sitesQuery = from s in androAdminEntities.Stores
                             where s.Id == siteId
                             select s;
            MyAndromedaDataAccessEntityFramework.Model.Store siteEntity = sitesQuery.FirstOrDefault();

            if (siteEntity != null)
            {
                site = new MyAndromedaDataAccess.Domain.Site();
                site.Id = siteEntity.Id;
                site.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                site.MenuVersion = 0;
                site.Name = siteEntity.ExternalSiteName;
                site.ExternalId = siteEntity.ExternalId;
                site.LicenceKey = siteEntity.LicenceKey;
            }

            return "";
        }

        public string GetByMyAndromedaUserId(int myAndromedaUserId, out List<MyAndromedaDataAccess.Domain.Site> sites)
        {
            sites = new List<MyAndromedaDataAccess.Domain.Site>();

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            // A user can be associated with zero or more groups of stores.
            // The user has permission to access any of the stores in these groups.
            // For example, there might be a group called "PJ UK" containing all PJ UK stores
            var myAndromedaQuery = from u in androAdminEntities.MyAndromedaUsers
                                   join mug in androAdminEntities.MyAndromedaUserGroups
                                     on u.Id equals mug.MyAndromedaUserId
                                   join g in androAdminEntities.Groups
                                     on mug.GroupId equals g.Id
                                   join sg in androAdminEntities.StoreGroups
                                     on g.Id equals sg.GroupId
                                   join s in androAdminEntities.Stores
                                     on sg.StoreId equals s.Id
                                   where u.Id == myAndromedaUserId
                                   select s;

            if (myAndromedaQuery != null)
            {
                foreach (Store storeEntity in myAndromedaQuery)
                {
                    MyAndromedaDataAccess.Domain.Site site = new MyAndromedaDataAccess.Domain.Site();
                    site.Id = storeEntity.Id;
                    site.EstDelivTime = storeEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                    site.MenuVersion = 0;
                    site.Name = storeEntity.ExternalSiteName;
                    site.ExternalId = storeEntity.ExternalId;
                    site.LicenceKey = storeEntity.LicenceKey;

                    sites.Add(site);
                }
            }

            // A user can be given permission to access specific stores.
            // For example, if the user is a store manager then he/she would have permission to access that store only
            var myAndromedaQuery2 = from u in androAdminEntities.MyAndromedaUsers
                                    join mus in androAdminEntities.MyAndromedaUserStores
                                      on u.Id equals mus.MyAndromedaUserId
                                    join s in androAdminEntities.Stores
                                      on mus.StoreId equals s.Id
                                    where u.Id == myAndromedaUserId
                                    select s;

            if (myAndromedaQuery2 != null && myAndromedaQuery2.Count() > 0)
            {
                foreach (Store storeEntity in myAndromedaQuery2)
                {
                    MyAndromedaDataAccess.Domain.Site site = new MyAndromedaDataAccess.Domain.Site();
                    site.Id = storeEntity.Id;
                    site.EstDelivTime = storeEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                    site.MenuVersion = 0;
                    site.Name = storeEntity.ExternalSiteName == null ? "" : storeEntity.ExternalSiteName;
                    site.ExternalId = storeEntity.ExternalId == null ? "" : storeEntity.ExternalId;
                    site.LicenceKey = storeEntity.LicenceKey == null ? "" : storeEntity.LicenceKey;

                    sites.Add(site);
                }
            }

            return "";
        }
    }
}
