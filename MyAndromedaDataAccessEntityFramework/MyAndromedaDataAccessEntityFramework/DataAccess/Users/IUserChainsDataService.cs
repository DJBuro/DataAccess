using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserChainsDataService : IDependency
    {
        IEnumerable<Chain> GetChainForUser(int userId);
    }

    public class UserChainsDataService : IUserChainsDataService
    {
        public IEnumerable<Chain> GetChainForUser(int userId) 
        {
            IEnumerable<Chain> chains = null;
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Chains;
                var query = table.Where(e => e.MyAndromedaUsers.Any(user => user.Id == userId));

                //top node on the chain structure 
                var result = query.ToArray();

                if (result.Length == 0)
                    return Enumerable.Empty<Chain>();

                chains = this.CreateStructure(dbContext, result);
            }

            return chains;
        }

        private IEnumerable<Chain> CreateStructure(Model.AndroAdmin.AndroAdminDbContext dbContext, Model.AndroAdmin.Chain[] results)
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
            
            var linkResult = linkQuery.ToDictionary(e=> e.Key, e=> e.ToArray());

            Action<Chain> buildTree = null;
            buildTree = (node) =>
            {
                var lookupByParentId = linkResult[node.Id]; 
                var children = new List<Chain>(lookupByParentId.Length);
                
                node.Children = children;
                foreach(var lookup in lookupByParentId)
                {
                    var chain = new Chain() 
                    { 
                        Id = lookup.Id,
                        Name = lookup.Name
                    };

                    buildTree(chain);
                }
            };

            foreach (var result in results) 
            {
                var chain = new Chain() 
                {  
                    Id = result.Id,
                    Name = result.Name,
                };

                buildTree(chain);

                chains.Add(chain);
            }

            return chains;
        }

        
    }

    public class ChainStructure 
    {
        public Chain Head {get;set;}
    }
}
