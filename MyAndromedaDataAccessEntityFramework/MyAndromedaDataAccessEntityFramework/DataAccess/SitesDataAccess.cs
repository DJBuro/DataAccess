using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SitesDataAccess : ISiteDataAccess
    {

        public void GetExternalAcsApplicationIds(int siteId, out IEnumerable<string> acsExternalApplicationIds)
        {
            acsExternalApplicationIds = Enumerable.Empty<string>();

            using (var dbContext = new AndroAdminDbContext())
            {
                var table = dbContext.ACSApplications;
                var query = table
                    .Where(e => e.ACSApplicationSites.Any(site => site.SiteId == siteId))
                    .Select(e=> e.ExternalApplicationId);
                var result = query.ToArray();

                acsExternalApplicationIds = result;
            }
        }

        public string GetExternalApplicationIds(int siteId, out IEnumerable<string> externalApplicationIds)
        {
            externalApplicationIds = Enumerable.Empty<string>();

            using (var dbContext = new AndroAdminDbContext()) 
            {
                var query = dbContext
                    .ACSApplications
                    .Where(e => e.ACSApplicationSites.Any(acsSite => acsSite.SiteId == siteId))
                    .Select(e=> e.ExternalApplicationId);
                
                var result = query.ToArray();

                externalApplicationIds = result;
            }

            return string.Empty;
        }

        public string GetAcsApplicationIds(int siteId, out IEnumerable<int> application)
        {
            application = null;
            using(var dbContext = new AndroAdminDbContext())
            {
                var query = dbContext.ACSApplications.Where(e => e.ACSApplicationSites.Any(acsSite => acsSite.SiteId == siteId));
                var result = query.Select(e => e.Id).ToArray();
                application = result;
            }

            return string.Empty;
        }

        public string GetById(int siteId, out MyAndromedaDataAccess.Domain.Site site)
        {
            site = null;
            using (var entitiesContext = new AndroAdminDbContext())
            {
                var query = from s in entitiesContext.Stores
                                 where s.Id == siteId
                                 select s;

                Store entity = query.FirstOrDefault();

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
                        AndromediaSiteId = entity.AndromedaSiteId,
                        ChainId = entity.ChainId
                    };
                }
            }

            return "";
        }

        public string GetSiteCountByAndromedaUserId(int myAndromedaUserId, out int total) 
        {
            using (var entitiesContext = new AndroAdminDbContext()) 
            {
                var table = entitiesContext.Groups;
                var query = table.Where(e => e.MyAndromedaUsers.Any(user => user.Id == myAndromedaUserId));
                var result = query.Count();
                    //.MyAndromedaUsers.Where(e => e.Id == myAndromedaUserId).Select(e=> e.
                    //entitiesContext.MyAndromedaUserGroups.Where(e => e.MyAndromedaUserId == myAndromedaUserId).Count();

                total = result;
            }

            return string.Empty;
        }

        public string GetByMyAndromedaUserId(int myAndromedaUserId, out List<MyAndromedaDataAccess.Domain.Site> sites)
        {
            var siteDictionary = new Dictionary<int, MyAndromedaDataAccess.Domain.Site>(); 

            //sites = new List<MyAndromedaDataAccess.Domain.Site>();

            using (var entitiesContext = new AndroAdminDbContext())
            {
                // A user can be associated with zero or more groups of stores.
                // The user has permission to access any of the stores in these groups.
                // For example, there might be a group called "PJ UK" containing all PJ UK stores
                var storeQuery = entitiesContext.Stores
                    .Where(e => e.Groups.Any(group => group.MyAndromedaUsers.Any(user => user.Id == myAndromedaUserId)))
                    .ToArray();

                if (storeQuery != null && storeQuery.Length > 0)
                {

                    foreach (Store entity in storeQuery)
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
                            AndromediaSiteId = entity.AndromedaSiteId,
                            ChainId = entity.ChainId
                        };

                        if(!siteDictionary.ContainsKey(site.Id))
                            siteDictionary.Add(site.Id, site);
                    }
                }

                // A user can be given permission to access specific stores.
                // For example, if the user is a store manager then he/she would have permission to access that store only
                //var query2 = from u in entitiesContext.MyAndromedaUsers
                //            join mus in entitiesContext.MyAndromedaUserStores
                //                on u.Id equals mus.MyAndromedaUserId
                //            join s in entitiesContext.Stores
                //                on mus.StoreId equals s.Id
                //            where u.Id == myAndromedaUserId
                //            select s;
                var query2 = entitiesContext.Stores.Where(e => e.MyAndromedaUserStores.Any(mapping => mapping.MyAndromedaUserId == myAndromedaUserId));

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
                            AndromediaSiteId = entity.AndromedaSiteId,
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
