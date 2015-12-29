using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class HostV2DataService : IHostV2DataService 
    {
        public void UpdateVersionForAll()
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;
                var tableEntities = table.ToArray();

                int newVersion = dbContext.GetNextDataVersionForEntity();

                foreach (var entity in tableEntities) 
                {
                    entity.DataVersion = newVersion;
                    entity.LastUpdateUtc = DateTime.UtcNow;
                }

                dbContext.SaveChanges();
            }
        }

        public void Add(HostV2 model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;

                //int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                model.DataVersion = dbContext.GetNextDataVersionForEntity();

                table.Add(model);

                dbContext.SaveChanges();
            }
        }

        public void Update(HostV2 model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;

                var entity = table.SingleOrDefault(e => e.Id == model.Id);
                entity.OptInOnly = model.OptInOnly;
                entity.Order = model.Order;
                entity.Public = model.Public;
                entity.Url = model.Url;
                entity.Version = model.Version;
                entity.Enabled = model.Enabled;
                entity.HostTypeId = model.HostTypeId;
                entity.DataVersion = dbContext.GetNextDataVersionForEntity();
                
                model.DataVersion = entity.DataVersion;

                dbContext.SaveChanges();
            }
        }

        public void Disable(Guid id)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;

                var entity = table.SingleOrDefault(e => e.Id == id);
                  
                entity.DataVersion = dbContext.GetNextDataVersionForEntity();
                entity.Enabled = false;

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<HostV2> ListDeleted(Expression<Func<HostV2, bool>> query)
        {
            IEnumerable<HostV2> results = Enumerable.Empty<HostV2>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2.Include(e => e.HostType);

                var tableQuery = query == null ?
                    table :
                    table.Where(query);

                tableQuery = tableQuery.Where(e => e.Deleted);

                var tableResult = tableQuery.ToArray();

                results = tableResult;
            }

            return results;
        }

        public IEnumerable<HostV2> List(Expression<Func<HostV2, bool>> query)
        {
            IEnumerable<HostV2> results = Enumerable.Empty<HostV2>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2.Include(e => e.HostType);
                  
                var tableQuery = query == null ? 
                    table : 
                    table.Where(query);
                
                tableQuery = tableQuery.Where(e => !e.Deleted);

                var tableResult = tableQuery.ToArray();

                results = tableResult;
            }

            return results;
        }

        public IEnumerable<T> List<T>(Expression<Func<HostV2, bool>> query, Func<HostV2, T> transformation)
        {
            IEnumerable<T> results = Enumerable.Empty<T>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2.Include(e => e.HostType);

                var tableQuery = query == null ? 
                    table : 
                    table.Where(query);
                tableQuery = tableQuery.Where(e => !e.Deleted);

                var queryResult = tableQuery.Select(transformation).ToArray();
                results = queryResult;

                dbContext.SaveChanges();
            }

            return results;
        }

        public void Destroy(HostV2 model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.HostV2;
                var entity = table.SingleOrDefault(e => e.Id == model.Id);

                if (entity != null) 
                {
                    entity.Deleted = true;
                    
                    entity.DataVersion = dbContext.GetNextDataVersionForEntity();
                    model.DataVersion = entity.DataVersion;
                }

                dbContext.SaveChanges();
            }
        }
    }
}