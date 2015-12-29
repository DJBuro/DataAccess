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
    public class StoreAMSServerFtpSiteDAO : IStoreAMSServerFTPSiteDAO
    {
        public IEnumerable<StoreAMSServerFtpSite> GetAll()
        {
            IEnumerable<StoreAMSServerFtpSite> storeAMSServerFtpSites = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                storeAMSServerFtpSites = session.CreateQuery("from " + typeof(StoreAMSServerFtpSite)).List<StoreAMSServerFtpSite>();
            }

            return storeAMSServerFtpSites;
        }

        public IEnumerable<StoreAMSServerFtpSite> GetBySiteId(int siteId)
        {
            IEnumerable<StoreAMSServerFtpSite> storeAMSServerFtpSites = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                storeAMSServerFtpSites = session.CreateQuery("from " + typeof(StoreAMSServerFtpSite) + " where StoreAMSServer.Store.Id=:siteId")
                    .SetParameter("siteId", siteId)
                    .List<StoreAMSServerFtpSite>();
            }

            return storeAMSServerFtpSites;
        }

        public void Add(StoreAMSServerFtpSite storeAMSServerFtpSite)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Save(storeAMSServerFtpSite);
                transaction.Commit();
            }
        }

        public StoreAMSServerFtpSite GetBySiteIdAMSServerIdFTPSiteId(int storeAMSServerId, int ftpSiteId)
        {
            IList<StoreAMSServerFtpSite> storeAMSServerFtpSites = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                storeAMSServerFtpSites = session.CreateQuery("from " + typeof(StoreAMSServerFtpSite) + " where StoreAMSServer.Id=:storeAMSServerId and FTPSite.Id=:ftpSiteId")
                    .SetParameter("storeAMSServerId", storeAMSServerId)
                    .SetParameter("ftpSiteId", ftpSiteId)
                    .List<StoreAMSServerFtpSite>();
            }

            StoreAMSServerFtpSite storeAMSServerFtpSite = null;

            if (storeAMSServerFtpSites.Count == 1)
            {
                storeAMSServerFtpSite = storeAMSServerFtpSites[0];
            }

            return storeAMSServerFtpSite;
        }


        public IEnumerable<StoreAMSServerFtpSite> GetByFTPSiteId(int ftpSiteId)
        {
            throw new NotImplementedException();
        }

        IList<StoreAMSServerFtpSite> IStoreAMSServerFTPSiteDAO.GetAll()
        {
            throw new NotImplementedException();
        }

        IList<StoreAMSServerFtpSite> IStoreAMSServerFTPSiteDAO.GetBySiteId(int siteId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByFTPSiteId(int ftpSiteId)
        {
            throw new NotImplementedException();
        }


        public void DeleteByAMSServerId(int amsServerId)
        {
            throw new NotImplementedException();
        }
    }
}