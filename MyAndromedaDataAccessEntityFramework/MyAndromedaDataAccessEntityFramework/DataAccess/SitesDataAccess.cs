using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Collections.Generic;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Comparer;
using MyAndromedaDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SitesDataAccess : ISiteDataAccess
    {
        public string GetById(int siteId, out MyAndromedaDataAccess.Domain.Site site)
        {
            site = null;
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Stores
                                 where s.Id == siteId
                                 select s;

                MyAndromedaDataAccessEntityFramework.Model.Store entity = query.FirstOrDefault();

                if (entity != null)
                {
                    site = new MyAndromedaDataAccess.Domain.Site()
                    {
                        Id = entity.Id,
                        EstDelivTime = entity.EstimatedDeliveryTime.GetValueOrDefault(0),
                        MenuVersion = 0,
                        ClientSiteName = entity.ClientSiteName == null ? "" : entity.ClientSiteName,
                        CustomerSiteId = entity.CustomerSiteId == null ? "" : entity.CustomerSiteId,
                        LicenceKey = entity.LicenseKey,
                        ExternalSiteId = entity.ExternalId,
                        ChainId = entity.ChainId
                    };
                }
            }

            return "";
        }

        public string GetSiteCountByAndromedaUserId(int myAndromedaUserId, out int total) 
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities()) 
            {
                var count = entitiesContext.MyAndromedaUserGroups.Where(e => e.MyAndromedaUserId == myAndromedaUserId).Count();

                total = count; ;
            }

            return string.Empty;
        }

        public string GetByMyAndromedaUserId(int myAndromedaUserId, out List<MyAndromedaDataAccess.Domain.Site> sites)
        {
            var siteDictionary = new Dictionary<int, MyAndromedaDataAccess.Domain.Site>(); 

            //sites = new List<MyAndromedaDataAccess.Domain.Site>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                // A user can be associated with zero or more groups of stores.
                // The user has permission to access any of the stores in these groups.
                // For example, there might be a group called "PJ UK" containing all PJ UK stores
                var query = (from u in entitiesContext.MyAndromedaUsers
                            join mug in entitiesContext.MyAndromedaUserGroups
                                on u.Id equals mug.MyAndromedaUserId
                            join g in entitiesContext.Groups
                                on mug.GroupId equals g.Id
                            join sg in entitiesContext.StoreGroups
                                on g.Id equals sg.GroupId
                            join s in entitiesContext.Stores
                                on sg.StoreId equals s.Id
                            where u.Id == myAndromedaUserId
                            select s).ToArray();

                if (query != null && query.Length > 0)
                {

                    foreach (Store entity in query)
                    {
                        MyAndromedaDataAccess.Domain.Site site = new MyAndromedaDataAccess.Domain.Site()
                        {
                            Id = entity.Id,
                            EstDelivTime = entity.EstimatedDeliveryTime.GetValueOrDefault(0),
                            MenuVersion = 0,
                            ClientSiteName = entity.ClientSiteName == null ? "" : entity.ClientSiteName,
                            CustomerSiteId = entity.CustomerSiteId == null ? "" : entity.CustomerSiteId,
                            LicenceKey = entity.LicenseKey,
                            ExternalSiteId = entity.ExternalId,
                            ChainId = entity.ChainId
                        };

                        if(!siteDictionary.ContainsKey(site.Id))
                            siteDictionary.Add(site.Id, site);
                    }
                }

                // A user can be given permission to access specific stores.
                // For example, if the user is a store manager then he/she would have permission to access that store only
                var query2 = from u in entitiesContext.MyAndromedaUsers
                            join mus in entitiesContext.MyAndromedaUserStores
                                on u.Id equals mus.MyAndromedaUserId
                            join s in entitiesContext.Stores
                                on mus.StoreId equals s.Id
                            where u.Id == myAndromedaUserId
                            select s;

                if (query2 != null && query2.Count() > 0)
                {
                    foreach (Store entity in query2)
                    {
                        MyAndromedaDataAccess.Domain.Site site = new MyAndromedaDataAccess.Domain.Site()
                        {
                            Id = entity.Id,
                            EstDelivTime = entity.EstimatedDeliveryTime.GetValueOrDefault(0),
                            MenuVersion = 0,
                            ClientSiteName = entity.ClientSiteName == null ? "" : entity.ClientSiteName,
                            CustomerSiteId = entity.CustomerSiteId == null ? "" : entity.CustomerSiteId,
                            LicenceKey = entity.LicenseKey == null ? "" : entity.LicenseKey,
                            ExternalSiteId = entity.ExternalId,
                            ChainId = entity.ChainId
                        };

                        if (!siteDictionary.ContainsKey(site.Id))
                            siteDictionary.Add(site.Id, site);
                    }
                }
            }

            sites = siteDictionary.Values.ToList();

            return "";
        }
    }
}
