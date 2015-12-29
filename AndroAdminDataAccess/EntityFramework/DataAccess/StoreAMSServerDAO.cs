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
            throw new NotImplementedException();
        }

        public Domain.StoreAMSServer GetByStoreIdAMServerId(int storeId, int amsServerId)
        {
            throw new NotImplementedException();
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

                androAdminEntities.SaveChanges();
            }
        }
    }
}