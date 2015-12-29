using System.Collections.Generic;
using MyAndromeda.Core;
using System;
using System.Linq;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserAccessDataService : IDependency
    {
        /// <summary>
        /// Determines whether the user is associated with store.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="storeId">The store id.</param>
        /// <returns></returns>
        bool IsTheUserAssociatedWithStore(int userId, int storeId);

        /// <summary>
        /// Determines whether [is the user associated with chain] [the specified user id].
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        bool IsTheUserAssociatedWithChain(int userId, int chainId);

        /// <summary>
        /// Determines whether the user is associated by chain.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="chainId">The chain id.</param>
        /// <param name="storeId">The store id.</param>
        /// <returns></returns>
        bool IsTheUserAssociatedByChainAndStore(int userId, int chainId, int storeId);

        /// <summary>
        /// Lists the chains user can access.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IList<Chain> ListChainsUserCanAccess(int userId);

        /// <summary>
        /// Lists the stores user can access.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IList<Site> ListStoresUserCanAccess(int userId);
    }

    public class UserAccessDataService : IUserAccessDataService 
    {
        private readonly IUserSitesDataService userSiteDataService;
        private readonly IUserChainsDataService userChainsDataServoce;

        public UserAccessDataService(IUserSitesDataService userSiteDataService, IUserChainsDataService userChainsDataServoce) 
        {
            this.userChainsDataServoce = userChainsDataServoce;
            this.userSiteDataService = userSiteDataService;
        }

        public IList<Chain> ListChainsUserCanAccess(int userId)
        {
            return userChainsDataServoce.GetChainsForUser(userId).ToList();
            //using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            //{
            //    var table = dbContext.Chains;
            //    var query = table.Where(e=> e.MyAndromedaUsers.Any(user => user.Id == userId));
            //    var result = query.ToList();

            //    return result;
            //}
        }

        public IList<Site> ListStoresUserCanAccess(int userId)
        {
            return this.userSiteDataService.GetSitesForUser(userId).ToList();
            //using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext())
            //{
            //    var table = dbContext.Stores;
            //    var query = table.Where(e => e.MyAndromedaUsers.Any(user => user.Id == userId));
            //    var result = query.ToList();

            //    return result;
            //}
        }

        public bool IsTheUserAssociatedWithStore(int userId, int storeId)
        {
            //fetch the missing chain id
            int chainId; 
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Stores;
                
                chainId = table
                    .Where(e => e.Id == storeId)
                    .Select(e=> e.ChainId)
                    .Single();
            }

            //lets go digging 
            return this.IsTheUserAssociatedByChainAndStore(userId, chainId, storeId);
        }

        private IEnumerable<Chain> FlatternChains(IEnumerable<Chain> accessibleChains)
        {
            Func<IEnumerable<Chain>, IEnumerable<Chain>> flatern = null;
            
            flatern = (nodes) => {
                return nodes.SelectMany(e=> flatern(e.Children)).Union(nodes);
            };

            var all = accessibleChains.Union(accessibleChains.SelectMany(e => flatern(e.Children)));

            return all;
        }

        public bool IsTheUserAssociatedWithChain(int userId, int chainId)
        {
            //get the entire structure that the user is allowed within 
            var chains = this.userChainsDataServoce.GetChainsForUser(userId);

            var flatListOfAllChains = this.FlatternChains(chains);

            var resultOfAny = flatListOfAllChains.Any(e => e.Id == chainId);

            return resultOfAny;
        }

        public bool IsTheUserAssociatedByChainAndStore(int userId, int chainId, int storeId)
        {
            bool associated = false;
            bool isTheUserAssociatedWithinTheChain = this.IsTheUserAssociatedWithChain(userId, chainId);
            
            //check the top level of chains associated to the user. Quick efficient
            int storesChainId;
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var store = dbContext.Stores.Single(e => e.Id == storeId);
                storesChainId = store.ChainId;
            }

            //anything found 
            associated = storesChainId == chainId ? true :
                //dont know why i would need to go off again as the correct chainid should already be provided.
                this.IsTheUserAssociatedWithChain(userId, storesChainId);

            if (!associated) 
            {
                System.Diagnostics.Trace.WriteLine("checking redundency - store - user table");
                //is the user connected to the user-store in the redundancy table?
                var sites = this.userSiteDataService.GetSitesForUser(userId, e => e.ChainId == chainId && e.Id == storeId);
                associated = sites.Any();
            }

            if (associated)
                return associated;

            return isTheUserAssociatedWithinTheChain && associated;
        }
    }
}

