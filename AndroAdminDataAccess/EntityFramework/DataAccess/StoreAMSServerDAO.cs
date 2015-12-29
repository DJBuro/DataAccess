using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreAMSServerDAO : IStoreAMSServerDAO
    {
        public void Add(Domain.StoreAMSServer storeAMSServer)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            StoreAMSServer entity = new StoreAMSServer()
            {
                AMSServerId = storeAMSServer.AMSServer.Id,
                Priority = storeAMSServer.Priority,
                StoreId = storeAMSServer.Store.Id
            };

            androAdminEntities.AddToStoreAMSServers(entity);
            androAdminEntities.SaveChanges();

            storeAMSServer.Id = entity.Id;
        }

        public Domain.StoreAMSServer GetByStoreIdAMServerId(int storeId, int amsServerId)
        {
            Domain.StoreAMSServer model = null;

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.StoreAMSServers
                        where s.StoreId == storeId
                        && s.AMSServerId == amsServerId
                        select s;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model = new Domain.StoreAMSServer()
                {
                    Id = entity.Id,
                    Priority = entity.Priority
                };
            }

            return model;
        }

        public void DeleteByAMSServerId(int amsServerId)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.StoreAMSServers
                        where s.AMSServerId == amsServerId
                        select s;

            foreach (var entity in query)
            {
                androAdminEntities.StoreAMSServers.DeleteObject(entity);
            }

            androAdminEntities.SaveChanges();
        }

        public void DeleteById(int id)
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.StoreAMSServers
                        where s.Id == id
                        select s;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                androAdminEntities.StoreAMSServers.DeleteObject(entity);
                androAdminEntities.SaveChanges();
            }
        }


        public IList<Domain.StoreAMSServer> GetByAMServerName(string amsServerName)
        {
            List<Domain.StoreAMSServer> model = new List<Domain.StoreAMSServer>();

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.StoreAMSServers
                        .Include("Store")
                        .Include("StoreAMSServerFtpSites")
                        .Include("StoreAMSServerFtpSites.FTPSite.FTPSiteType")
                        .Include("AMSServer")
                        where s.AMSServer.Name == amsServerName
                        select s;

            foreach (var entity in query)
            {            
                List<Domain.FTPSite> ftpSites = new List<Domain.FTPSite>(); 
                foreach (var subEntity in entity.StoreAMSServerFtpSites)
                {
                    Domain.FTPSite ftpSite = new Domain.FTPSite()
                    {
                        FTPSiteType = new Domain.FTPSiteType() { Id = subEntity.FTPSite.FTPSiteType.Id, Name = subEntity.FTPSite.FTPSiteType.Name },
                        Name = subEntity.FTPSite.Name,
                        Password = subEntity.FTPSite.Password,
                        Port = subEntity.FTPSite.Port,
                        Url = subEntity.FTPSite.Url,
                        Username = subEntity.FTPSite.Username
                    };
                    
                    ftpSites.Add(ftpSite);
                }

                Domain.Store store = new Domain.Store()
                {
                    AndromedaSiteId = entity.Store.AndromedaSiteId,
                    CustomerSiteId = entity.Store.CustomerSiteId,
                    LastFTPUploadDateTime = entity.Store.LastFTPUploadDateTime,
                    Name = entity.Store.Name,
                    StoreStatus = new Domain.StoreStatus() { Id = entity.Store.StoreStatu.Id, Status = entity.Store.StoreStatu.Status, Description = entity.Store.StoreStatu.Description }
                };

                Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                {
                    Id = entity.Id,
                    Priority = entity.Priority,
                    FTPSites = ftpSites,
                    AMSServer = new Domain.AMSServer() { Name = entity.AMSServer. Name },
                    Store = store
                };
                
                model.Add(storeAMSServer);
            }

            return model;
        }
    }
}