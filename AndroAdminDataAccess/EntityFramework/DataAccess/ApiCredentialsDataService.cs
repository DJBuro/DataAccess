using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public interface IExternalApiDataService
    {
        IEnumerable<ExternalApi> List();
        IEnumerable<ExternalApi> List(Expression<Func<ExternalApi, bool>> query);

        void Update(ExternalApi api);
        void Delete(ExternalApi api);
        void Create(ExternalApi api);

        ExternalApi New();
        ExternalApi Get(Guid id);
    }

    public class ExternalApiDataService : IExternalApiDataService
    {
        public IEnumerable<ExternalApi> List()
        {
            var results = Enumerable.Empty<ExternalApi>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                results = table.ToArray();
            }

            return results;
        }

        public IEnumerable<ExternalApi> List(Expression<Func<ExternalApi, bool>> query)
        {
            var results = Enumerable.Empty<ExternalApi>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                var tableQuery = table.Where(query);
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

                dbContext.SaveChanges();
            }
        }

        public void Delete(ExternalApi model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                var entity = table.SingleOrDefault(e => e.Id == model.Id);

                if (entity != null)
                {
                    table.Remove(entity);
                }

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

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ExternalApis;
                table.Add(model);

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
