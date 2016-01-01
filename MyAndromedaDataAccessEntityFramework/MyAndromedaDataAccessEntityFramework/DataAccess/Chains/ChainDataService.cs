using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Chains
{
    public class ChainDataService : IChainDataService 
    {
        public IEnumerable<Site> GetChainsSiteList(int chainId)
        {
            IEnumerable<MyAndromedaDataAccess.Domain.Site> sites;
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Stores.Include(e => e.Address);
                var query = table.Where(e => e.Chain.Id == chainId);
                var results = query.ToArray();

                sites = results.Select(e => e.ToDomain());
            }

            return sites;
        }

        public MyAndromedaDataAccess.Domain.Chain Get(int chainId)
        {
            MyAndromedaDataAccess.Domain.Chain entity = null;
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext())
            {
                var table = dbContext.Chains;
                var query = table.Where(e => e.Id == chainId).ToArray();
                var result = query.SingleOrDefault();

                entity = new MyAndromedaDataAccess.Domain.Chain()
                {
                    Id = result.Id,
                    Name = result.Name
                };
            }

            return entity;
        }

        public IEnumerable<Chain> List(Expression<Func<Model.AndroAdmin.Chain, bool>> query)
        {
            IEnumerable<Chain> chains = Enumerable.Empty<Chain>();

            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Chains;
                var tableQuery = table.Where(query);
                var result = tableQuery.ToArray();

                chains = result.Select(e=> new MyAndromedaDataAccess.Domain.Chain()
                {
                    Id = e.Id,
                    Name = e.Name
                }).ToArray();
            }

            return chains;
        }
    }
}