using System;
using System.Collections.Generic;
using System.Linq;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Data.Entity;

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

                var table = entitiesContext.StoreAMSServers
                    .Include(e => e.Store)
                    .Include(e => e.Store.StoreStatu)
                    .Include(e => e.Store.Address)
                    .Include(e => e.Store.Address.Country)
                    .Include(e => e.AMSServer);

                var tableQuery = table.Select(s => new
                {
                    StoreId = s.Store.Id,
                    StoreName = s.Store.Name,
                    AndroStoreId = s.Store.AndromedaSiteId,
                    StoreStatus = s.Store.StoreStatu.Status,
                    AMSServerName = s.AMSServer.Description, // sas.AMSServer.Description,
                    LastFTPUploadDateTime = s.Store.LastFTPUploadDateTime,
                    Country = s.Store.Address.Country.CountryName
                });

                var result = tableQuery.ToArray();

                foreach (var entity in result)
                {
                    Domain.StoreAMSServerFtpSiteListItem storeAMSServerFtpSiteListItem = new StoreAMSServerFtpSiteListItem()
                    {
                        AMSServerName = entity.AMSServerName,
                        FTPSite = string.Empty,
                        StoreId = entity.StoreId,
                        AndroStoreId = entity.AndroStoreId,
                        StoreStatus = entity.StoreStatus,
                        LastUploaded = entity.LastFTPUploadDateTime,
                        StoreName = entity.StoreName,
                        Country = entity.Country
                    };

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

                var table = entitiesContext.StoreAMSServerFtpSites
                    .Include(e=> e.StoreAMSServer)
                    .Include(e=> e.StoreAMSServer.Store)
                    .Include(e=> e.StoreAMSServer.AMSServer)
                    .Include(e=> e.FTPSite)
                    .Include(e=> e.FTPSite.FTPSiteType);

                foreach (var entity in table)
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

                    Domain.StoreAMSServer storeAmsServer = new Domain.StoreAMSServer()
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
                        StoreAMSServer = storeAmsServer
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

                var table = entitiesContext.StoreAMSServerFtpSites
                    .Include(e => e.StoreAMSServer)
                    .Include(e => e.StoreAMSServer.Store)
                    .Include(e => e.StoreAMSServer.AMSServer)
                    .Include(e=> e.FTPSite)
                    .Include(e=> e.FTPSite.FTPSiteType);

                var tableQuery = table.Where(e => e.StoreAMSServer.Store.Id == siteId);
                var tableResult = tableQuery.ToArray();

                foreach (var entity in tableResult)
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

        public void Add(Domain.StoreAMSServerFtpSite storeAmsServerFtpSite)
        {

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                StoreAMSServerFtpSite entity = new StoreAMSServerFtpSite()
                {
                    FTPSiteId = storeAmsServerFtpSite.FTPSite.Id,
                    StoreAMSServerId = storeAmsServerFtpSite.StoreAMSServer.Id,
                };

                entitiesContext.StoreAMSServerFtpSites.Add(entity);
                entitiesContext.SaveChanges();
            }
        }

        public Domain.StoreAMSServerFtpSite GetBySiteIdAMSServerIdFTPSiteId(int storeAmsServerId, int ftpSiteId)
        {
            Domain.StoreAMSServerFtpSite model = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServerFtpSites
                            where storeAmsServerId == s.StoreAMSServerId
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

                var table = entitiesContext.StoreAMSServerFtpSites
                    .Include(e => e.StoreAMSServer)
                    .Include(e => e.StoreAMSServer.Store)
                    .Include(e => e.StoreAMSServer.AMSServer);

                var query = table.Where(s => s.StoreAMSServerId == storeAMSServerId);
                var result = query.ToArray();

                foreach (var entity in result)
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

                var table = entitiesContext.StoreAMSServerFtpSites
                    .Include(e=> e.StoreAMSServer)
                    .Include(e=> e.StoreAMSServer.Store)
                    .Include(e=> e.StoreAMSServer.AMSServer);

                var tableQuery = table
                    .Where(e => e.Id == id);

                var entity = tableQuery.FirstOrDefault();

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