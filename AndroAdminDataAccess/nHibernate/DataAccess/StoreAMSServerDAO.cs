using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using AndroAdminDataAccess.nHibernate.Mappings;
using NHibernate;

namespace AndroAdminDataAccess.nHibernate.DataAccess
{
    public class StoreAMSServerDAO : IStoreAMSServerDAO
    {
        public IEnumerable<StoreAMSServer> GetAll()
        {
            IEnumerable<StoreAMSServer> storeAMSServers = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                storeAMSServers = session.CreateQuery("from " + typeof(StoreAMSServer)).List<StoreAMSServer>();
            }

            return storeAMSServers;
        }

        public void Add(StoreAMSServer storeAMSServer)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Save(storeAMSServer);
                transaction.Commit();
            }
        }

        public StoreAMSServer GetByStoreIdAMServerId(int storeId, int amsServerId)
        {
            IList<StoreAMSServer> storeAMSServers = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                storeAMSServers = session.CreateQuery("from " + typeof(StoreAMSServer) + " where Store.Id=:storeId and AMSServer.Id=:amsServerId")
                    .SetParameter("storeId", storeId)
                    .SetParameter("amsServerId", amsServerId)
                    .List<StoreAMSServer>();
            }

            StoreAMSServer storeAMSServer = null;

            if (storeAMSServers.Count == 1)
            {
                storeAMSServer = storeAMSServers[0];
            }

            return storeAMSServer;
        }


        public void DeleteByAMSServerId(int amsServerId)
        {
            throw new NotImplementedException();
        }
    }
}