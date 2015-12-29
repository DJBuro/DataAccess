using System;
using System.Linq;
using System.Data.Entity;
using System.Data.EntityModel;
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
        //IEnumerable<Site> GetSitesForUserAndChain(int userId, int chainId, bool deepSearch);
        void RemoveStoreLinkToUser(int userId, int storeId);

        /// <summary>
        /// Adds store to user
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        void AddStoreLinkToUser(int storeId, int userId);
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
                int[] accessibleStores;
                using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext())
                {
                    var userStoresTable = myAndromedaDbContext.UserStores;
                    var userStoresQuery = userStoresTable.Where(e => e.UserRecordId == userId).Select(e => e.StoreId);
                    var userStoresResult = userStoresQuery.ToList();

                    accessibleStores = userStoresResult.ToArray();
                }

                var storesTable = androAdminDbContext.Stores.Include(e=> e.Address);
                var storeQuery = storesTable.Where(e => accessibleStores.Contains(e.Id));
                var storeResults = storeQuery.ToArray();

                sites = storeResults.Select(e => e.ToDomainModel()).ToArray();
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

                var storesTable = androAdminDbContext.Stores.Include(e => e.Address);
                var storesQuery = storesTable.Where(e => e.ChainId == chainId);
                var storesResult = storesQuery.ToArray();

                sites = storesResult.Select(e => e.ToDomainModel()).ToArray();
            }

            return sites;
        }

        public void RemoveStoreLinkToUser(int userId, int storeId)
        {
            using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = myAndromedaDbContext.UserStores;
                var query = table.Where(e => e.StoreId == storeId && e.UserRecordId == userId);
                var results = query.ToArray();

                foreach (var result in results) { myAndromedaDbContext.UserStores.Remove(result); }

                myAndromedaDbContext.SaveChanges();
            }
        }

        public void AddStoreLinkToUser(int storeId, int userId)
        {
            using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var userStoreTable = myAndromedaDbContext.UserStores;
                if (userStoreTable.Any(e => e.StoreId == storeId && e.UserRecordId == userId))
                    return;

                var link = userStoreTable.Create();
                link.StoreId = storeId;
                link.UserRecordId = userId;

                userStoreTable.Add(link);
                myAndromedaDbContext.SaveChanges();
            }
        }
        //public IEnumerable<Site> GetSitesForUserAndChain(int userId, int chainId, bool deepSearch)
        //{
        //    if (!deepSearch) { return this.GetSitesForUserAndChain(userId, chainId); }

        //    throw new NotImplementedException();
        //}
    }

}