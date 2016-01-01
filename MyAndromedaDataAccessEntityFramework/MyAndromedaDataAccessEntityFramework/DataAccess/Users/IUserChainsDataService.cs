using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserChainsDataService : IDependency
    {
        IEnumerable<Chain> GetChainsForUser(int userId);
        IEnumerable<Chain> GetChainsForUser(int userId, Expression<Func<Model.AndroAdmin.Chain, bool>> query);
    }

    public class UserChainsDataService : IUserChainsDataService
    {
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

                    var userChainsResult = userChainsQuery.ToList();

                    accessibleChains = userChainsResult;
                }

                var chainTable = androAdminDbContext.Chains;
                var chainQuery = chainTable.Where(e => accessibleChains.Any(chainId => chainId == e.Id));

                //top node on the chain structure 
                var chainResult = chainQuery.ToArray();

                if (chainResult.Length == 0)
                    return chains;

                chains = this.CreateHierarchyStructure(androAdminDbContext, chainResult);
            }

            return chains;
        }

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

                    var userChainsResult = userChainsQuery.ToList();

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

        private IEnumerable<Chain> CreateHierarchyStructure(Model.AndroAdmin.AndroAdminDbContext dbContext, Model.AndroAdmin.Chain[] results)
        {
            var chains = new List<Chain>(results.Length);

            var linkTable = dbContext.ChainChains;
            var linkQuery = linkTable
                .Where(e => e.ParentChainId > 0)
                .Select(e=> new {
                    e.ParentChainId,
                    e.ChildChain.Id,
                    e.ChildChain.Name
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
                
                node.Children = children;
                foreach(var lookup in lookupByParentId)
                {
                    var chain = new Chain() 
                    { 
                        Id = lookup.Id,
                        Name = lookup.Name,
                        Children = new List<Chain>()
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
                    Children = Enumerable.Empty<Chain>()
                };

                buildTree(chain);

                chains.Add(chain);
            }

            return chains;
        }

        
    }

}
