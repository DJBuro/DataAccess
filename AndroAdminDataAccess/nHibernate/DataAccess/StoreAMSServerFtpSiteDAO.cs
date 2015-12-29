﻿using System;
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
    }
}