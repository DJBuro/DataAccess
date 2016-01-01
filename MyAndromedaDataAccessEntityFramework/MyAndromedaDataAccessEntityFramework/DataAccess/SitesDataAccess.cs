using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace MyAndromedaDataAccessEntityFramework.DataAccess
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
                                 .Select(e => e.ExternalApplicationId);
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
                    .Select(e => e.ExternalApplicationId);
                
                var result = query.ToArray();

                externalApplicationIds = result;
            }

            return string.Empty;
        }

        public string GetAcsApplicationIds(int siteId, out IEnumerable<int> application)
        {
            application = null;
            using (var dbContext = new AndroAdminDbContext())
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
                    site = entity.ToDomain();
                }
            }

            return string.Empty;
        }

        public string GetByMyAndromedaUserId(int myAndromedaUserId, out List<MyAndromedaDataAccess.Domain.Site> sites)
        {
            var siteDictionary = new Dictionary<int, MyAndromedaDataAccess.Domain.Site>(); 

            using (var entitiesContext = new AndroAdminDbContext())
            {
                // A user can be associated with zero or more groups of stores.
                // The user has permission to access any of the stores in these groups.
                // For example, there might be a group called "PJ UK" containing all PJ UK stores
                var userChains = entitiesContext.Chains.Where(chain => chain.MyAndromedaUsers.Any(user => user.Id == myAndromedaUserId)).ToArray();
                
                Func<Chain, IEnumerable<Store>> recursivelySelectStores = null;
                recursivelySelectStores = (chain) =>
                {
                    foreach (var link in chain.Children) 
                    {
                        return recursivelySelectStores(link.ChildChain);
                    }

                    return chain.Stores;
                };

                //dig into each chain
                var allAccessibleStores = userChains.SelectMany(e => recursivelySelectStores(e)).ToArray();

                //var allAvailableChainsForUser = entitiesContext.Chains
                //    .Where(e => e.MyAndromedaUsers.Any(user => user.Id == myAndromedaUserId))
                //    .SelectMany(e => e.Children.Select(chain => chain.Child));
                //var userStoresQuery = userChains.SelectMany(e => e.Stores).ToArray();
                
                if (allAccessibleStores != null && allAccessibleStores.Length > 0)
                {
                    foreach (Store entity in allAccessibleStores)
                    {
                        MyAndromedaDataAccess.Domain.Site site = entity.ToDomain();

                        if (!siteDictionary.ContainsKey(site.Id))
                        {
                            siteDictionary.Add(site.Id, site);
                        }
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

                var query2 = entitiesContext.Stores.Where(e => e.MyAndromedaUsers.Any(user => user.Id == myAndromedaUserId)).ToArray();

                if (query2 != null && query2.Count() > 0)
                {
                    foreach (Store entity in query2)
                    {
                        MyAndromedaDataAccess.Domain.Site site = entity.ToDomain();

                        if (!siteDictionary.ContainsKey(site.Id))
                        {
                            siteDictionary.Add(site.Id, site);
                        }
                    }
                }
            }

            sites = siteDictionary.Values.ToList();

            return string.Empty;
        }
    }
}
