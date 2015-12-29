using System.Linq.Expressions;
using AndroAdminDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void AddRange(int storeId, IEnumerable<Guid> selectServerListIds)
        {
            var idCollection = selectServerListIds.ToArray();
            using (var dbContext = new AndroAdminEntities()) 
            {
                var storeTable = dbContext.Stores;
                var hostListTable = dbContext.HostV2;

                var store = storeTable.SingleOrDefault(e => e.Id == storeId);
                var serversQuery = hostListTable.Where(e=> idCollection.Contains(e.Id)).ToArray();

                if (store.HostV2 == null) { store.HostV2 = new List<HostV2>(); }

                store.HostV2.Clear();

                foreach (var server in serversQuery) 
                {
                    store.HostV2.Add(server);
                }

                dbContext.SaveChanges();
            }
        }

        public void ClearAll(int storeId)
        {
            using (var dbContext = new AndroAdminEntities())
            {
                var storeTable = dbContext.Stores;
                var hostListTable = dbContext.HostV2;

                var store = storeTable.SingleOrDefault(e => e.Id == storeId);

                if (store.HostV2 == null) { store.HostV2 = new List<HostV2>(); }
                store.HostV2.Clear();

                dbContext.SaveChanges();
            }
        }
    }
}