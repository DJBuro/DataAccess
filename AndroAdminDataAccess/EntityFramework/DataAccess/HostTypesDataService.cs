using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{

    public class HostV2DataService : IHostV2DataService 
    {
        public void Add(HostV2 model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;

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

                dbContext.SaveChanges();
            }
        }

        public void Disable(Guid id)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;

                var entity = table.SingleOrDefault(e => e.Id == id);

                entity.Enabled = false;

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<HostV2> List(Expression<Func<HostV2, bool>> query)
        {
            IEnumerable<HostV2> results = Enumerable.Empty<HostV2>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2.Include(e=> e.HostType);
                
                var tableQuery = query == null ? table : table.Where(query);
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

                var tableQuery = query == null ? table : table.Where(query);
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
                var queryResult = table.SingleOrDefault(e => e.Id == model.Id);

                if (queryResult != null) 
                {
                    table.Remove(queryResult);
                }

                dbContext.SaveChanges();
            }
        }
    }
    
    public class HostTypesDataService : IHostTypesDataService 
    {
        public void Add(HostType model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.HostTypes;

                if (!table.Any(e => e.Id == model.Id))
                { 
                    table.Add(model);
                }

                dbContext.SaveChanges();
            }
        }

        public void Update(HostType model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostTypes;
                var record = table.SingleOrDefault(e => e.Id == model.Id);

                record.Name = model.Name;
                  
                dbContext.SaveChanges();
            }
        }

        public void Destroy(HostType model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostTypes;
                var record = table.SingleOrDefault(e => e.Id == model.Id);

                if (record != null) 
                {
                    table.Remove(record);
                }

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<HostType> List(System.Linq.Expressions.Expression<Func<HostType, bool>> query)
        {
            IEnumerable<HostType> result = Enumerable.Empty<HostType>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostTypes;
                var tableQuery = query == null ? table : table.Where(query);

                result = tableQuery.ToArray();
            }

            return result;
        }
    }
}