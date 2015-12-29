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
        public IEnumerable<Domain.StoreAMSServer> GetAll()
        {
            //List<Domain.StoreAMSServer> StoreAMSServers = new List<Domain.StoreAMSServer>();

            //AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            //var query = from s in androAdminEntities.FTPSites.Include("FTPSiteType")
            //            select s;

            //foreach (var entity in query)
            //{
            //    Domain.FTPSite model = new Domain.FTPSite()
            //    {
            //        Id = entity.Id,
            //        Name = entity.Name,
            //        Url = entity.Url,
            //        Port = entity.Port,
            //        Username = entity.Username,
            //        Password = entity.Password,
            //        FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSiteType.Id, Name = entity.FTPSiteType.Name }
            //    };

            //    StoreAMSServers.Add(model);
            //}

            //return StoreAMSServers;

            throw new NotImplementedException();
        }

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
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

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
    }
}