using AndroAdminDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AndroAdminDataAccess.Domain;
using System.Transactions;
using System.Data.Entity.Validation;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class AndroWebOrderingThemeDAO : IAndroWebOrderingThemeDAO
    {
        public string ConnectionStringOverride { get; set; }


        public IList<Domain.AndroWebOrderingTheme> GetAll()
        {
            List<Domain.AndroWebOrderingTheme> models = new List<Domain.AndroWebOrderingTheme>();
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.AndroWebOrderingThemes
                            select s;

                foreach (var entity in query)
                {
                    Domain.AndroWebOrderingTheme model = new Domain.AndroWebOrderingTheme()
                    {
                        Id = entity.Id,
                        Source = entity.Source,
                        FileName = entity.FileName,
                        Height = entity.Height,
                        Width = entity.Width,
                        InternalName = entity.InternalName,
                        Description = entity.Description
                    };
                    models.Add(model);
                }
            }
            return models;
        }

        public Domain.AndroWebOrderingTheme GetAndroWebOrderingThemeById(int id)
        {
            Domain.AndroWebOrderingTheme model = null;
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                var query = from s in entitiesContext.AndroWebOrderingThemes.Where(s => s.Id == id)
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.AndroWebOrderingTheme()
                    {
                        Id = entity.Id,
                        Source = entity.Source,
                        FileName = entity.FileName,
                        Height = entity.Height,
                        Width = entity.Width,
                        InternalName = entity.InternalName,
                        Description = entity.Description
                    };
                }
            }
            return model;
        }

        public void Add(Domain.AndroWebOrderingTheme webOrderingTheme)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                AndroWebOrderingTheme entity = new AndroWebOrderingTheme()
                {
                    Id = webOrderingTheme.Id,
                    Source = webOrderingTheme.Source,
                    FileName = webOrderingTheme.FileName,
                    Height = webOrderingTheme.Height,
                    Width = webOrderingTheme.Width,
                    InternalName = webOrderingTheme.InternalName,
                    Description = webOrderingTheme.Description
                };
                entitiesContext.AndroWebOrderingThemes.Add(entity);
                entitiesContext.SaveChanges();
            }
        }

        public void Update(Domain.AndroWebOrderingTheme webOrderingTheme)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                var query = from s in entitiesContext.AndroWebOrderingThemes.Where(s => s.Id == webOrderingTheme.Id)
                            select s;
                var entity = query.FirstOrDefault();
                if (entity != null)
                {
                    entity.Source = webOrderingTheme.Source;
                    entity.FileName = webOrderingTheme.FileName;
                    entity.Height = webOrderingTheme.Height;
                    entity.Width = webOrderingTheme.Width;
                    entity.InternalName = webOrderingTheme.InternalName;
                    entity.Description = webOrderingTheme.Description;
                };
                entitiesContext.SaveChanges();
            }
        }

        public void Delete(Domain.AndroWebOrderingTheme webOrderingTheme)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                var query = from s in entitiesContext.AndroWebOrderingThemes.Where(s => s.Id == webOrderingTheme.Id)
                            select s;
                var entity = query.FirstOrDefault();
                if (entity != null)
                {
                    entitiesContext.AndroWebOrderingThemes.Remove(entity);
                    entitiesContext.SaveChanges();
                };
            }
        }
    }
}
