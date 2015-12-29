using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class FtpSiteDAO : IFTPSiteDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.FTPSite> GetAll()
        {
            List<Domain.FTPSite> ftpSites = new List<Domain.FTPSite>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.FTPSites.Include("FTPSiteType")
                            select s;

                foreach (var entity in query)
                {
                    Domain.FTPSite model = new Domain.FTPSite()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Url = entity.Url,
                        Port = entity.Port,
                        Username = entity.Username,
                        Password = entity.Password,
                        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSiteType.Id, Name = entity.FTPSiteType.Name }
                    };

                    ftpSites.Add(model);
                }
            }

            return ftpSites;
        }

        public void Add(Domain.FTPSite ftpSite)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                FTPSite entity = new FTPSite()
                {
                    Name = ftpSite.Name,
                    Url = ftpSite.Url,
                    Port = ftpSite.Port,
                    Username = ftpSite.Username,
                    Password = ftpSite.Password,
                    FTPSiteType_Id = ftpSite.FTPSiteType.Id
                };

                entitiesContext.FTPSites.Add(entity);
                entitiesContext.SaveChanges();
            }
        }

        public void Update(Domain.FTPSite ftpSite)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.FTPSites
                            where ftpSite.Id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = ftpSite.Name;
                    entity.Url = ftpSite.Url;
                    entity.Port = ftpSite.Port;
                    entity.Username = ftpSite.Username;
                    entity.Password = ftpSite.Password;
                    entity.FTPSiteType_Id = ftpSite.FTPSiteType.Id;

                    entitiesContext.SaveChanges();
                }
            }
        }

        public Domain.FTPSite GetById(int id)
        {
            Domain.FTPSite model = null;

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.FTPSites
                            where id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.FTPSite()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Url = entity.Url,
                        Port = entity.Port,
                        Username = entity.Username,
                        Password = entity.Password,
                        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSiteType.Id, Name = entity.FTPSiteType.Name }
                    };
                }
            }

            return model;
        }

        public Domain.FTPSite GetByName(string name)
        {
            Domain.FTPSite model = null;

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.FTPSites
                            where name == s.Name
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.FTPSite()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Url = entity.Url,
                        Port = entity.Port,
                        Username = entity.Username,
                        Password = entity.Password,
                        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSiteType.Id, Name = entity.FTPSiteType.Name }
                    };
                }
            }

            return model;
        }


        public void Delete(int ftpSiteId)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.FTPSites
                            where ftpSiteId == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entitiesContext.FTPSites.Remove(entity);

                    entitiesContext.SaveChanges();
                }
            }
        }
    }
}