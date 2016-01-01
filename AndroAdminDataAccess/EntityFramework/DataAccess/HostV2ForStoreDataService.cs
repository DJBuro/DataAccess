using System.Linq.Expressions;
using AndroAdminDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class HostV2ForStoreDataService : IHostV2ForStoreDataService 
    {
        public IEnumerable<HostStoreConnection> ListHostConnections(Expression<Func<Store, bool>> query)
        {
            IEnumerable<HostStoreConnection> results = Enumerable.Empty<HostStoreConnection>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var storeTable = dbContext.Stores;
                var hostV2Table = dbContext.HostV2;
               
                var innerQuery = 
                    (
                        from store in storeTable
                        from hostV2List in store.HostV2
                        select new HostStoreConnection
                        {
                            AndromedaSiteId = store.AndromedaSiteId,
                            HostId = hostV2List.Id
                        }
                    );

                results = innerQuery.ToArray();
            }

            return results;
        }

        public IEnumerable<Store> ListByHostId(Guid id)
        {
            IEnumerable<Store> results = Enumerable.Empty<Store>();

            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.Stores;
                var query = table.Where(e=> e.HostV2.Any(host => host.Id == id));

                results = query.ToArray();
            }

            return results;
        }

        public IEnumerable<HostV2> ListConnectedHostsForSite(int storeId)
        {
            IEnumerable<HostV2> results = Enumerable.Empty<HostV2>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;
                var query = table.Where(e => e.Stores.Any(store => store.Id == storeId));

                results = query.ToArray();
            }

            return results;
        }

        public void AddCompleteRange(int storeId, IEnumerable<Guid> selectServerListIds)
        {
            var idCollection = selectServerListIds.ToArray();

            using (var dbContext = new AndroAdminEntities()) 
            {
                var storeTable = dbContext.Stores;
                var hostListEntities = dbContext.HostV2.ToArray();

                var store = storeTable.SingleOrDefault(e => e.Id == storeId);

                if (store.HostV2 == null) { store.HostV2 = new List<HostV2>(); }

                var previousConnectedIds = store.HostV2.ToArray();

                var serversQuery = hostListEntities.Where(e => idCollection.Contains(e.Id)).ToArray();
                var updateRemovedHosts = hostListEntities.Where(e => previousConnectedIds.Any(previousHost => previousHost.Id == e.Id)).ToArray();
                    //previousConnectedIds.Where(e => serversQuery.Any(server => server.Id == e.Id)).ToArray();

                var dataVersion = dbContext.GetNextDataVersionForEntity();
                
                store.HostV2.Clear();
                dbContext.SaveChanges();

                foreach (var server in serversQuery) 
                {
                    server.DataVersion = dataVersion;

                    store.HostV2.Add(server);
                }
                
                foreach (var server in updateRemovedHosts) 
                {
                    server.DataVersion = dataVersion;
                }

                dbContext.SaveChanges();
            }
        }

        public void ClearAll(int storeId)
        {
            using (var dbContext = new AndroAdminEntities())
            {
                var storeTable = dbContext.Stores.Include(e=> e.HostV2);
                //var hostListTable = dbContext.HostV2.ToArray();
                
                var store = storeTable.SingleOrDefault(e => e.Id == storeId);
                var storeHubs = store.HostV2.ToArray();

                if (store.HostV2 == null) { store.HostV2 = new List<HostV2>(); }
                store.HostV2.Clear();

                var nextDataVersion = dbContext.GetNextDataVersionForEntity();
                foreach (var host in storeHubs) 
                {
                    host.DataVersion = nextDataVersion;
                }

                dbContext.SaveChanges();
            }
        }
    }
}