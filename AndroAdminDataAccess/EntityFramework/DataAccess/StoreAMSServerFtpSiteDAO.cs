using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreAMSServerFtpSiteDAO : IStoreAMSServerFTPSiteDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.StoreAMSServerFtpSiteListItem> GetAllListItems()
        {
            List<Domain.StoreAMSServerFtpSiteListItem> models = new List<Domain.StoreAMSServerFtpSiteListItem>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            join sas in entitiesContext.StoreAMSServers
                                .Include("StoreAMSServerFtpSites")
                                .Include("StoreAMSServer")
                                .Include("StoreAMSServer.Store")
                                .Include("StoreAMSServer.AMSServer")
                                on s.Id equals sas.StoreId into _sas
                                
                                from sas in _sas.DefaultIfEmpty()
//                            orderby s.StoreAMSServer.Store.Name
                            select new
                            {
                                Id = s.Id,
                                StoreName = s.Name,
                                AMSServerName = sas.AMSServer.Description,
                                //FTPSites = s.StoreAMSServer.Store.
                                LastFTPUploadDateTime = s.LastFTPUploadDateTime
                            };

                //var query = from s in entitiesContext.StoreAMSServerFtpSites
                //            .Include("StoreAMSServer")
                //            .Include("StoreAMSServer.Store")
                //            .Include("StoreAMSServer.AMSServer")
                //            orderby s.StoreAMSServer.Store.Name
                //            select new
                //            {
                //                Id = s.StoreAMSServer.Store.Id,
                //                StoreName = s.StoreAMSServer.Store.Name,
                //                AMSServerName = s.StoreAMSServer.AMSServer.Description,
                //                //FTPSites = s.StoreAMSServer.Store.
                //                LastFTPUploadDateTime = s.StoreAMSServer.Store.LastFTPUploadDateTime
                //            };

                //var query = from x in entitiesContext.StoreAMSServerFtpSites
                //            from sd in x.StoreAMSServer.DefaultIfEmpty()
                //         //   from sd in s.StudentDescriptions.DefaultIfEmpty()
                //         //   from sd in s.StudentDescriptions.DefaultIfEmpty()
                //            //join sas in entitiesContext.StoreAMSServers 
                //            //    on x.StoreAMSServerId equals sas.Id into _sas
                //            //    from sas in _sas.DefaultIfEmpty()
                //            //join s in entitiesContext.Stores 
                //            //    on sas.StoreId equals s.Id into _s
                //            //    from s in _s.DefaultIfEmpty()
                //            //join a in entitiesContext.AMSServers 
                //            //    on sas.AMSServerId equals a.Id into _a
                //            //    from a in _a.DefaultIfEmpty()
                //            orderby x.StoreAMSServer.Store.Name
                //            select new
                //            {
                //                Id = x.StoreAMSServer.Store.Id,
                //                StoreName = s.Name,
                //                AMSServerName = a.Description,
                //                LastFTPUploadDateTime = s.LastFTPUploadDateTime
                //            };

                foreach (var entity in query)
                {
                    Domain.StoreAMSServerFtpSiteListItem storeAMSServerFtpSiteListItem = new StoreAMSServerFtpSiteListItem()
                    {
                        AMSServerName = entity.AMSServerName,
                        FTPSite = "",
                        StoreId = entity.Id,
                        LastUploaded = entity.LastFTPUploadDateTime,
                        StoreName = entity.StoreName
                    };

                    //Domain.FTPSite ftpSite = new Domain.FTPSite()
                    //{
                    //    Id = entity.FTPSite.Id,
                    //    Name = entity.FTPSite.Name,
                    //    Url = entity.FTPSite.Url,
                    //    Port = entity.FTPSite.Port,
                    //    Username = entity.FTPSite.Username,
                    //    Password = entity.FTPSite.Password,
                    //    FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSite.FTPSiteType.Id, Name = entity.FTPSite.FTPSiteType.Name }
                    //};

                    //Domain.Store store = new Domain.Store()
                    //{
                    //    Id = entity.StoreAMSServer.Store.Id,
                    //    Name = entity.StoreAMSServer.Store.Name,
                    //    AndromedaSiteId = entity.StoreAMSServer.Store.AndromedaSiteId,
                    //    CustomerSiteId = entity.StoreAMSServer.Store.CustomerSiteId,
                    //    LastFTPUploadDateTime = entity.StoreAMSServer.Store.LastFTPUploadDateTime
                    //};

                    //Domain.AMSServer amsServer = new Domain.AMSServer()
                    //{
                    //    Id = entity.StoreAMSServer.AMSServer.Id,
                    //    Name = entity.StoreAMSServer.AMSServer.Name,
                    //    Description = entity.StoreAMSServer.AMSServer.Description
                    //};

                    //Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                    //{
                    //    Id = entity.StoreAMSServer.Id,
                    //    Priority = entity.StoreAMSServer.Priority,
                    //    Store = store,
                    //    AMSServer = amsServer
                    //};

                    //Domain.StoreAMSServerFtpSite model = new Domain.StoreAMSServerFtpSite()
                    //{
                    //    Id = entity.Id,
                    //    FTPSite = ftpSite,
                    //    StoreAMSServer = storeAMSServer
                    //};

                    models.Add(storeAMSServerFtpSiteListItem);
                }
            }

            return models;
        }

        public IList<Domain.StoreAMSServerFtpSite> GetAll()
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            .Include("StoreAMSServer")
                            .Include("StoreAMSServer.Store")
                            .Include("StoreAMSServer.AMSServer")
                            select s;

                foreach (var entity in query)
                {
                    Domain.FTPSite ftpSite = new Domain.FTPSite()
                    {
                        Id = entity.FTPSite.Id,
                        Name = entity.FTPSite.Name,
                        Url = entity.FTPSite.Url,
                        Port = entity.FTPSite.Port,
                        Username = entity.FTPSite.Username,
                        Password = entity.FTPSite.Password,
                        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSite.FTPSiteType.Id, Name = entity.FTPSite.FTPSiteType.Name }
                    };

                    Domain.Store store = new Domain.Store()
                    {
                        Id = entity.StoreAMSServer.Store.Id,
                        Name = entity.StoreAMSServer.Store.Name,
                        AndromedaSiteId = entity.StoreAMSServer.Store.AndromedaSiteId,
                        CustomerSiteId = entity.StoreAMSServer.Store.CustomerSiteId,
                        LastFTPUploadDateTime = entity.StoreAMSServer.Store.LastFTPUploadDateTime
                    };

                    Domain.AMSServer amsServer = new Domain.AMSServer()
                    {
                        Id = entity.StoreAMSServer.AMSServer.Id,
                        Name = entity.StoreAMSServer.AMSServer.Name,
                        Description = entity.StoreAMSServer.AMSServer.Description
                    };

                    Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                    {
                        Id = entity.StoreAMSServer.Id,
                        Priority = entity.StoreAMSServer.Priority,
                        Store = store,
                        AMSServer = amsServer
                    };

                    Domain.StoreAMSServerFtpSite model = new Domain.StoreAMSServerFtpSite()
                    {
                        Id = entity.Id,
                        FTPSite = ftpSite,
                        StoreAMSServer = storeAMSServer
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public IList<Domain.StoreAMSServerFtpSite> GetBySiteId(int siteId)
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            .Include("StoreAMSServer.Store")
                            .Include("StoreAMSServer.AMSServer")
                            join sa in entitiesContext.StoreAMSServers
                            on s.StoreAMSServerId equals sa.Id
                            where siteId == sa.StoreId
                            select s;

                foreach (var entity in query)
                {
                    Domain.FTPSite ftpSite = new Domain.FTPSite()
                    {
                        Id = entity.FTPSite.Id,
                        Name = entity.FTPSite.Name,
                        Url = entity.FTPSite.Url,
                        Port = entity.FTPSite.Port,
                        Username = entity.FTPSite.Username,
                        Password = entity.FTPSite.Password,
                        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSite.FTPSiteType.Id, Name = entity.FTPSite.FTPSiteType.Name }
                    };

                    Domain.Store store = new Domain.Store()
                    {
                        Id = entity.StoreAMSServer.Store.Id,
                        Name = entity.StoreAMSServer.Store.Name,
                        AndromedaSiteId = entity.StoreAMSServer.Store.AndromedaSiteId,
                        CustomerSiteId = entity.StoreAMSServer.Store.CustomerSiteId,
                        LastFTPUploadDateTime = entity.StoreAMSServer.Store.LastFTPUploadDateTime
                    };

                    Domain.AMSServer amsServer = new Domain.AMSServer()
                    {
                        Id = entity.StoreAMSServer.AMSServer.Id,
                        Name = entity.StoreAMSServer.AMSServer.Name,
                        Description = entity.StoreAMSServer.AMSServer.Description
                    };

                    Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                    {
                        Id = entity.StoreAMSServer.Id,
                        Priority = entity.StoreAMSServer.Priority,
                        Store = store,
                        AMSServer = amsServer
                    };

                    Domain.StoreAMSServerFtpSite model = new Domain.StoreAMSServerFtpSite()
                    {
                        Id = entity.Id,
                        FTPSite = ftpSite,
                        StoreAMSServer = storeAMSServer
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public void Add(Domain.StoreAMSServerFtpSite storeAMSServerFtpSite)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                StoreAMSServerFtpSite entity = new StoreAMSServerFtpSite()
                {
                    FTPSiteId = storeAMSServerFtpSite.FTPSite.Id,
                    StoreAMSServerId = storeAMSServerFtpSite.StoreAMSServer.Id,
                };

                entitiesContext.StoreAMSServerFtpSites.Add(entity);
                entitiesContext.SaveChanges();
            }
        }

        public Domain.StoreAMSServerFtpSite GetBySiteIdAMSServerIdFTPSiteId(int storeAMSServerId, int ftpSiteId)
        {
            Domain.StoreAMSServerFtpSite model = null;

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            where storeAMSServerId == s.StoreAMSServerId
                            && ftpSiteId == s.FTPSiteId
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.StoreAMSServerFtpSite()
                    {
                        Id = entity.Id
                    };
                }
            }

            return model;
        }

        public void DeleteByFTPSiteId(int ftpSiteId)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            where s.FTPSiteId == ftpSiteId
                            select s;

                foreach (var entity in query)
                {
                    entitiesContext.StoreAMSServerFtpSites.Remove(entity);
                }

                entitiesContext.SaveChanges();
            }
        }

        public void DeleteByAMSServerId(int amsServerId)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            where s.StoreAMSServer.AMSServerId == amsServerId
                            select s;

                foreach (var entity in query)
                {
                    entitiesContext.StoreAMSServerFtpSites.Remove(entity);
                }

                entitiesContext.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            where s.Id == id
                            select s;

                var entity = query.FirstOrDefault();

                entitiesContext.StoreAMSServerFtpSites.Remove(entity);

                entitiesContext.SaveChanges();
            }
        }


        public IList<Domain.StoreAMSServerFtpSite> GetByStoreAMSServerId(int storeAMSServerId)
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites.Include("StoreAMSServer")
                                .Include("StoreAMSServer.Store")
                                .Include("StoreAMSServer.AMSServer")
                            where s.StoreAMSServerId == storeAMSServerId
                            select s;

                foreach (var entity in query)
                {
                    Domain.FTPSite ftpSite = new Domain.FTPSite()
                    {
                        Id = entity.FTPSite.Id,
                        Name = entity.FTPSite.Name,
                        Url = entity.FTPSite.Url,
                        Port = entity.FTPSite.Port,
                        Username = entity.FTPSite.Username,
                        Password = entity.FTPSite.Password,
                        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSite.FTPSiteType.Id, Name = entity.FTPSite.FTPSiteType.Name }
                    };

                    Domain.Store store = new Domain.Store()
                    {
                        Id = entity.StoreAMSServer.Store.Id,
                        Name = entity.StoreAMSServer.Store.Name,
                        AndromedaSiteId = entity.StoreAMSServer.Store.AndromedaSiteId,
                        CustomerSiteId = entity.StoreAMSServer.Store.CustomerSiteId,
                        LastFTPUploadDateTime = entity.StoreAMSServer.Store.LastFTPUploadDateTime
                    };

                    Domain.AMSServer amsServer = new Domain.AMSServer()
                    {
                        Id = entity.StoreAMSServer.AMSServer.Id,
                        Name = entity.StoreAMSServer.AMSServer.Name,
                        Description = entity.StoreAMSServer.AMSServer.Description
                    };

                    Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                    {
                        Id = entity.StoreAMSServer.Id,
                        Priority = entity.StoreAMSServer.Priority,
                        Store = store,
                        AMSServer = amsServer
                    };

                    Domain.StoreAMSServerFtpSite model = new Domain.StoreAMSServerFtpSite()
                    {
                        Id = entity.Id,
                        FTPSite = ftpSite,
                        StoreAMSServer = storeAMSServer
                    };

                    models.Add(model);
                }
            }

            return models;
        }


        public Domain.StoreAMSServerFtpSite GetById(int id)
        {
            Domain.StoreAMSServerFtpSite model = null;

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            .Include("StoreAMSServer.Store")
                            .Include("StoreAMSServer.AMSServer")
                            join sa in entitiesContext.StoreAMSServers
                            on s.StoreAMSServerId equals sa.Id
                            where id == s.Id
                            select s;

                var entity = query.FirstOrDefault();
                if (entity != null)
                {
                    Domain.FTPSite ftpSite = new Domain.FTPSite()
                    {
                        Id = entity.FTPSite.Id,
                        Name = entity.FTPSite.Name,
                        Url = entity.FTPSite.Url,
                        Port = entity.FTPSite.Port,
                        Username = entity.FTPSite.Username,
                        Password = entity.FTPSite.Password,
                        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSite.FTPSiteType.Id, Name = entity.FTPSite.FTPSiteType.Name }
                    };

                    Domain.Store store = new Domain.Store()
                    {
                        Id = entity.StoreAMSServer.Store.Id,
                        Name = entity.StoreAMSServer.Store.Name,
                        AndromedaSiteId = entity.StoreAMSServer.Store.AndromedaSiteId,
                        CustomerSiteId = entity.StoreAMSServer.Store.CustomerSiteId,
                        LastFTPUploadDateTime = entity.StoreAMSServer.Store.LastFTPUploadDateTime
                    };

                    Domain.AMSServer amsServer = new Domain.AMSServer()
                    {
                        Id = entity.StoreAMSServer.AMSServer.Id,
                        Name = entity.StoreAMSServer.AMSServer.Name,
                        Description = entity.StoreAMSServer.AMSServer.Description
                    };

                    Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                    {
                        Id = entity.StoreAMSServer.Id,
                        Priority = entity.StoreAMSServer.Priority,
                        Store = store,
                        AMSServer = amsServer
                    };

                    model = new Domain.StoreAMSServerFtpSite()
                    {
                        Id = entity.Id,
                        FTPSite = ftpSite,
                        StoreAMSServer = storeAMSServer
                    };
                }
            }

            return model;
        }
    }
}