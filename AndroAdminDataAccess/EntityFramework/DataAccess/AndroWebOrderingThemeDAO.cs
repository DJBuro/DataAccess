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


        public IList<Domain.ThemeAndroWebOrdering> GetAll()
        {
            List<Domain.ThemeAndroWebOrdering> models = new List<Domain.ThemeAndroWebOrdering>();
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.AndroWebOrderingThemes
                            select s;

                foreach (var entity in query)
                {
                    Domain.ThemeAndroWebOrdering model = new Domain.ThemeAndroWebOrdering()
                    {
                        Id = entity.Id,
                        Source = entity.ThemePath,
                        FileName = entity.ThemeName,
                        Height = entity.Height,
                        Width = entity.Width,
                        //InternalName = entity.InternalName,
                        Description = entity.Description
                    };
                    models.Add(model);
                }
            }
            return models;
        }

        public Domain.ThemeAndroWebOrdering GetAndroWebOrderingThemeById(int id)
        {
            Domain.ThemeAndroWebOrdering model = null;
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                var query = from s in entitiesContext.AndroWebOrderingThemes.Where(s => s.Id == id)
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.ThemeAndroWebOrdering()
                    {
                        Id = entity.Id,
                        Source = entity.ThemePath,
                        FileName = entity.ThemeName,
                        Height = entity.Height,
                        Width = entity.Width,
                        //InternalName = entity.InternalName,
                        Description = entity.Description
                    };
                }
            }
            return model;
        }

        public Domain.ThemeAndroWebOrdering Add(Domain.ThemeAndroWebOrdering webOrderingTheme)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                AndroWebOrderingTheme entity = new AndroWebOrderingTheme()
                {
                    Id = webOrderingTheme.Id,
                    ThemePath = webOrderingTheme.Source,
                    ThemeName = webOrderingTheme.FileName,
                    Height = webOrderingTheme.Height,
                    Width = webOrderingTheme.Width,
                    //InternalName = webOrderingTheme.InternalName,
                    Description = webOrderingTheme.Description
                };
                entitiesContext.AndroWebOrderingThemes.Add(entity);
                entitiesContext.SaveChanges();                
                webOrderingTheme.Id = entity.Id;                     
            }
            return webOrderingTheme;
        }

        public void Update(Domain.ThemeAndroWebOrdering webOrderingTheme)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                var query = from s in entitiesContext.AndroWebOrderingThemes.Where(s => s.Id == webOrderingTheme.Id)
                            select s;
                var entity = query.FirstOrDefault();
                if (entity != null)
                {
                    entity.ThemePath = webOrderingTheme.Source;
                    entity.ThemeName = webOrderingTheme.FileName;
                    entity.Height = webOrderingTheme.Height;
                    entity.Width = webOrderingTheme.Width;
                    //entity.InternalName = webOrderingTheme.InternalName;
                    entity.Description = webOrderingTheme.Description;
                };
                entitiesContext.SaveChanges();               
            }
        }

        public void Delete(Domain.ThemeAndroWebOrdering webOrderingTheme)
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
