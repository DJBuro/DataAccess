using MyAndromeda.Core;
using MyAndromeda.Core.User;
using MyAndromedaDataAccess.Domain;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserChainsDataService : IDependency
    {
        /// <summary>
        /// Gets the chains for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IEnumerable<Chain> GetChainsForUser(int userId);

        /// <summary>
        /// Gets the chains for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<Chain> GetChainsForUser(int userId, Expression<Func<Model.AndroAdmin.Chain, bool>> query);

        /// <summary>
        /// Adds the chain to user.
        /// </summary>
        /// <param name="chain">The chain.</param>
        /// <param name="userId">The user id.</param>
        void AddChainLinkToUser(Chain chain, int userId);

        /// <summary>
        /// Finds the users belonging to chain.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        IEnumerable<MyAndromedaUser> FindUsersDirectlyBelongingToChain(int chainId);

        /// <summary>
        /// Finds the chains directly belonging to user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IEnumerable<Chain> FindChainsDirectlyBelongingToUser(int userId);

        /// <summary>
        /// Removes the chain link to user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="chainId">The chain id.</param>
        void RemoveChainLinkToUser(int userId, int chainId);
    }

    public class UserChainsDataService : IUserChainsDataService
    {
        /// <summary>
        /// Gets the chains for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public IEnumerable<Chain> GetChainsForUser(int userId) 
        {
            IEnumerable<Chain> chains = Enumerable.Empty<Chain>();
            using (var androAdminDbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var accessibleChains = Enumerable.Empty<int>();
                using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext())
                {
                    var userChainsTable = myAndromedaDbContext.UserChains;
                    var userChainsQuery = userChainsTable
                        .Where(e => e.UserRecordId == userId)
                        .Select(e => e.ChainId);

                    var userChainsResult = userChainsQuery.ToArray();

                    accessibleChains = userChainsResult;
                }

                var chainTable = androAdminDbContext.Chains
                    .Include(e=> e.ChildChains)
                    .Include(e=> e.ParentChains)
                    .ToArray();

                var chainQuery = chainTable.Where(e => accessibleChains.Contains(e.Id)); 

                //top node on the chain structure 
                var chainResult = chainQuery.ToArray();

                if (chainResult.Length == 0)
                    return chains;

                chains = this.CreateHierarchyStructure(androAdminDbContext, chainResult).ToArray();
            }

            return chains;
        }

        /// <summary>
        /// Gets the chains for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<Chain> GetChainsForUser(int userId, Expression<Func<Model.AndroAdmin.Chain, bool>> query) 
        {
            if (query == null) { query = (_) => true; }

            IEnumerable<Chain> chains = Enumerable.Empty<Chain>();
            using (var androAdminDbContext = new Model.AndroAdmin.AndroAdminDbContext())
            {
                var accessibleChains = Enumerable.Empty<int>();
                using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext())
                {
                    var userChainsTable = myAndromedaDbContext.UserChains;
                    var userChainsQuery = userChainsTable
                        .Where(e => e.UserRecordId == userId)
                        .Select(e => e.ChainId);

                    var userChainsResult = userChainsQuery.ToArray();

                    accessibleChains = userChainsResult;
                }

                var chainTable = androAdminDbContext.Chains;
                var chainQuery = chainTable
                    .Where(query)
                    .Where(e => accessibleChains.Any(chainId => chainId == e.Id));

                //top node on the chain structure 
                var chainResult = chainQuery.ToArray();

                if (chainResult.Length == 0)
                    return chains;

                chains = this.CreateHierarchyStructure(androAdminDbContext, chainResult);
            }

            return chains;
        }

        /// <summary>
        /// Adds the chain to user.
        /// </summary>
        /// <param name="chain">The chain.</param>
        /// <param name="userId">The user id.</param>
        public void AddChainLinkToUser(Chain chain, int userId)
        {
            using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var userChainsTable = myAndromedaDbContext.UserChains;
                if (userChainsTable.Any(e => e.ChainId == chain.Id && e.UserRecordId == userId))
                    return;
                
                var link = userChainsTable.Create();
                link.ChainId = chain.Id;
                link.UserRecordId = userId;

                userChainsTable.Add(link);
                myAndromedaDbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Finds the users belonging to chain.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        public IEnumerable<MyAndromedaUser> FindUsersDirectlyBelongingToChain(int chainId)
        {
            IEnumerable<MyAndromedaUser> myAndromedaUserusers;
            
            using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                IEnumerable<UserRecord> userRecords = myAndromedaDbContext.UserChains.Where(e => e.ChainId == chainId).Select(e => e.UserRecord);
                var result = userRecords.ToArray();

                myAndromedaUserusers = result.Select(e => e.ToDomain()).ToArray();
            }

            return myAndromedaUserusers;
        }

        public IEnumerable<Chain> FindChainsDirectlyBelongingToUser(int userId)
        {
            IEnumerable<Chain> chains = Enumerable.Empty<Chain>();

            using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var userChainsTable = myAndromedaDbContext.UserChains;
                var userChainsquery = userChainsTable.Where(e => e.UserRecordId == userId).Select(e => e.ChainId).ToArray();

                using (var androAdminDbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
                {
                    var chainsquery = androAdminDbContext.Chains.Where(chain => userChainsquery.Contains(chain.Id));
                    var chainsResult = chainsquery.ToArray().Select(e => new Chain() { 
                        Id = e.Id,
                        Name = e.Name,
                        Culture = e.Culture
                    }).ToArray();

                    chains = chainsResult;
                }
            }

            return chains;
        }

        public void RemoveChainLinkToUser(int userId, int chainId)
        {
            using (var myAndromedaDbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = myAndromedaDbContext.UserChains;
                var query = table.Where(e=> e.ChainId == chainId && e.UserRecordId == userId);
                var results = query.ToArray();

                foreach (var result in results) { myAndromedaDbContext.UserChains.Remove(result); }

                myAndromedaDbContext.SaveChanges();
            }
        }

        private IEnumerable<Chain> CreateHierarchyStructure(Model.AndroAdmin.AndroAdminDbContext dbContext, Model.AndroAdmin.Chain[] results)
        {
            var chains = new List<Chain>(results.Length);

            var linkTable = dbContext.ChainChains;
            var linkQuery = linkTable
                .Where(e => e.ParentChainId > 0)
                .Select(e=> new {
                    e.ParentChainId,
                    e.ChildChain.Id,
                    e.ChildChain.Name,
                    e.ChildChain.Culture
                })
                .ToLookup(e => e.ParentChainId);
            
            //create a dictionary 
            var linkResult = linkQuery.ToDictionary(e=> e.Key, e=> e.ToArray());

            Action<Chain> buildTree = null;
            buildTree = (node) =>
            {
                if (!linkResult.ContainsKey(node.Id))
                    return;

                var lookupByParentId = linkResult[node.Id]; 
                var children = new List<Chain>(lookupByParentId.Length);
                
                node.Items = children;
                foreach(var lookup in lookupByParentId)
                {
                    var chain = new Chain() 
                    { 
                        Id = lookup.Id,
                        Name = lookup.Name,
                        Culture = lookup.Culture,
                        Items = new List<Chain>()
                    };

                    children.Add(chain);

                    buildTree(chain);
                }
            };

            foreach (var result in results) 
            {
                var chain = new Chain() 
                {  
                    Id = result.Id,
                    Name = result.Name,
                    Culture = result.Culture,
                    Items = Enumerable.Empty<Chain>()
                };

                buildTree(chain);

                chains.Add(chain);
            }

            return chains;
        }
    }
}
