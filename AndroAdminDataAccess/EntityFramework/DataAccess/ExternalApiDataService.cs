using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class ExternalApiDataService : IExternalApiDataService
    {
        public IEnumerable<ExternalApi> List()
        {
            var results = Enumerable.Empty<ExternalApi>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis.Where(e => !e.Removed);
                results = table.ToArray();
            }

            return results;
        }

        public IEnumerable<ExternalApi> ListRemoved(Expression<Func<ExternalApi, bool>> query)
        {
            var results = Enumerable.Empty<ExternalApi>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                var tableQuery = table
                    .Where(e => e.Removed)
                    .Where(query);

                results = tableQuery.ToArray();
            }

            return results;
        }

        public IEnumerable<ExternalApi> List(Expression<Func<ExternalApi, bool>> query)
        {
            var results = Enumerable.Empty<ExternalApi>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                var tableQuery = table
                                      .Where(e => !e.Removed)
                                      .Where(query);
                results = tableQuery.ToArray();
            }

            return results;
        }

        public void Update(ExternalApi model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                var entity = table.Single(e => e.Id == model.Id);

                entity.Name = model.Name;
                entity.DefinitionParameters = model.DefinitionParameters;
                entity.Parameters = model.Parameters;

                entity.DataVersion = dbContext.GetNextDataVersionForEntity();

                dbContext.SaveChanges();
            }
        }

        public void Delete(ExternalApi model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                var entity = table.SingleOrDefault(e => e.Id == model.Id);

                if (entity == null)
                {
                    return;
                }

                entity.Removed = true;
                entity.DataVersion = dbContext.GetNextDataVersionForEntity();

                dbContext.SaveChanges();
            }
        }

        public ExternalApi New()
        {
            return new ExternalApi()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                DefinitionParameters = string.Empty,
                Parameters = string.Empty
            };
        }

        public void Create(ExternalApi model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                table.Add(model);
                
                if (model.Id == default(Guid))
                {
                    model.Id = Guid.NewGuid();
                }

                if (model.Parameters == null)
                {
                    model.Parameters = string.Empty;
                }

                if (model.DefinitionParameters == null)
                {
                    model.DefinitionParameters = string.Empty;
                }

                model.DataVersion = dbContext.GetNextDataVersionForEntity();
                
                dbContext.SaveChanges();
            }
        }

        public ExternalApi Get(Guid id)
        {
            ExternalApi result;
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                var entity = table.Single(e => e.Id == id);

                result = entity;
            }

            return result;
        }


        
    }
}