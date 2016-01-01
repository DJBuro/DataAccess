using System;
using System.Linq;
using System.Collections.Generic;
using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserSitesDataService : IDependency 
    {
        /// <summary>
        /// Gets the sites for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IEnumerable<Site> GetSitesForUser(int userId);

        IEnumerable<Site> GetSitesForUser(int userId, Expression<Func<Store, bool>> query);

        /// <summary>
        /// Gets the sites for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        IEnumerable<Site> GetSitesForUserAndChain(int userId, int chainId);

        /// <summary>
        /// Gets the sites for user and chain.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="chainId">The chain id.</param>
        /// <param name="deepSearch">The deep search.</param>
        /// <returns></returns>
        IEnumerable<Site> GetSitesForUserAndChain(int userId, int chainId, bool deepSearch);
    }

    public class UserSitesDataService : IUserSitesDataService
    {
        private readonly IUserChainsDataService chainsDataAccessService;

        public UserSitesDataService(IUserChainsDataService chainsDataAccessService) 
        {
            this.chainsDataAccessService = chainsDataAccessService;
        }

        public IEnumerable<Site> GetSitesForUser(int userId, Expression<Func<Store, bool>> query) 
        {
            IEnumerable<Site> sites;
            using (var androAdminDbContext = new Model.AndroAdmin.AndroAdminDbContext())
            {
                IEnumerable<int> accessibleStores = Enumerable.Empty<int>();
                using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext())
                {
                    var userStoresTable = myAndromedaDbContext.UserStores;
                    var userStoresQuery = userStoresTable.Where(e => e.UserRecordId == userId).Select(e => e.StoreId);
                    var userStoresResult = userStoresQuery.ToList();

                    accessibleStores = userStoresResult;
                }

                var storesTable = androAdminDbContext.Stores;
                var storeQuery = storesTable.Where(e => accessibleStores.Any(storeId => storeId == e.Id));
                var storeResults = storeQuery.ToArray();

                sites = storeResults.Select(e => e.ToDomain()).ToArray();
            }

            return sites;
        }

        public IEnumerable<Site> GetSitesForUser(int userId)
        {
            return this.GetSitesForUser(userId, e => true);
        }

        public IEnumerable<Site> GetSitesForUserAndChain(int userId, int chainId)
        {
            IEnumerable<Site> sites = Enumerable.Empty<Site>();
            using (var androAdminDbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var accessibleChains = chainsDataAccessService.GetChainsForUser(userId, e=> e.Id == chainId);

                if (!accessibleChains.Any()) 
                {
                    return sites;
                }

                var storesTable = androAdminDbContext.Stores;
                var storesQuery = storesTable.Where(e => e.ChainId == chainId);
                var storesResult = storesQuery.ToArray();

                sites = storesResult.Select(e => e.ToDomain()).ToArray();
            }

            return sites;
        }


        public IEnumerable<Site> GetSitesForUserAndChain(int userId, int chainId, bool deepSearch)
        {
            if (!deepSearch) { return this.GetSitesForUserAndChain(userId, chainId); }

            throw new NotImplementedException();
        }
    }

}