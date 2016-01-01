using System.Collections.Generic;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System;
using System.Linq;

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

        IList<Chain> ChainsUserCanAccess(int userId);
        IList<Store> StoresUserCanAccess(int userId);
    }

    public class UserAccessDataService : IUserAccessDataService 
    {
        public UserAccessDataService() 
        {
        }

        public IList<Chain> ChainsUserCanAccess(int userId)
        {
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Chains;
                var query = table.Where(e=> e.MyAndromedaUsers.Any(user => user.Id == userId));
                var result = query.ToList();

                return result;
            }
        }

        public IList<Store> StoresUserCanAccess(int userId)
        {
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext())
            {
                var table = dbContext.Stores;
                var query = table.Where(e => e.MyAndromedaUsers.Any(user => user.Id == userId));
                var result = query.ToList();

                return result;
            }
        }

        public bool IsTheUserAssociatedWithStore(int userId, int storeId)
        {
            int chainId; 
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Stores;
                var query = table
                    .Where(e => e.Id == storeId)
                    .Where(e=> e.MyAndromedaUsers.Any(user => user.Id == userId))
                    .Select(e=> e);
                
                var result = query.FirstOrDefault();

                if (result != null) { return true; }

                chainId = table
                    .Where(e => e.Id == storeId)
                    .Select(e=> e.ChainId)
                    .Single();
            }

            //try again on the chain level
            //failed with the simple link ... lets go digging 
            return this.IsTheUserAssociatedByChainAndStore(userId, chainId, storeId);
        }

        public bool IsTheUserAssociatedWithChain(int userId, int chainId)
        {
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Chains;
                
                Func<Chain, bool> recursivelyCheckChain = null;
                recursivelyCheckChain = (chain) =>
                {
                    if (chain.Id == chainId)
                        return true;

                    foreach (var link in chain.Children)
                    {
                        if (recursivelyCheckChain(link.ChildChain))
                            return true; ;
                    }

                    return false;
                };

                var availableChainsQuery = table.Where(e => e.MyAndromedaUsers.Any(user => user.Id == userId));
                var availableChainsQueryResult = availableChainsQuery.ToArray();

                //already attained that none of these chains are the ones we are looking for. 
                //dig into the chain hierarchy
                foreach (var chain in availableChainsQueryResult)
                {
                    //dig into each chain to n levels of china chain 
                    if (recursivelyCheckChain(chain))
                        return true;
                }
            }

            return false;
        }

        public bool IsTheUserAssociatedByChainAndStore(int userId, int chainId, int storeId)
        {
            bool associated = false;
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Chains;
                
                //check the top level of chains associated to the user. Quick efficient
                var quickQuery = table
                    .Where(chain => chain.MyAndromedaUsers.Any(user => user.Id == userId))
                    .Where(chain => chain.Id == chainId);

                //anything found 
                associated = quickQuery.Any();

                if (associated)
                    return associated;
            }

            return IsTheUserAssociatedWithChain(userId, chainId);
        }
    }
}

