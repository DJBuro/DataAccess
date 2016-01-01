using System;
using System.Collections.Generic;
using System.Linq;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
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