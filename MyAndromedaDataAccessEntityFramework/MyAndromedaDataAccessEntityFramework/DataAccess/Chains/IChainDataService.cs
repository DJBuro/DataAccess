using MyAndromeda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Chains
{
    public interface IChainDataService : IDependency
    {
        MyAndromedaDataAccess.Domain.Chain Get(int chainId);

        IEnumerable<MyAndromedaDataAccess.Domain.Site> GetChainsSiteList(int chainId); 
    }

    public class ChainDataService : IChainDataService 
    {
        public ChainDataService() 
        {
            
        }
        
        public IEnumerable<Site> GetChainsSiteList(int chainId)
        {
            IEnumerable<MyAndromedaDataAccess.Domain.Site> sites;
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Stores;
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
                var query = table.Where(e => e.Id == chainId).Select(e => new { e.Id, entity.Name });
                var result = query.SingleOrDefault();

                entity = new MyAndromedaDataAccess.Domain.Chain() { 
                    Id = result.Id,
                    Name = result.Name
                };
            }
            return entity;
        }
    }
}
