using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using AndroAdminDataAccess.nHibernate.Mappings;
using NHibernate;

namespace AndroAdminDataAccess.nHibernate.DataAccess
{
    public class FtpSiteDAO : IFtpSiteDAO
    {
        public IEnumerable<AndroAdminDataAccess.Domain.FTPSite> GetAll()
        {
            IEnumerable<AndroAdminDataAccess.Domain.FTPSite> ftpSites = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ftpSites = session.CreateQuery("from " + typeof(AndroAdminDataAccess.Domain.FTPSite)).List<AndroAdminDataAccess.Domain.FTPSite>();
            }

            return ftpSites;
        }

        public void Add(AndroAdminDataAccess.Domain.FTPSite ftpSite)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Save(ftpSite);
                transaction.Commit();
            }
        }

        public void Update(AndroAdminDataAccess.Domain.FTPSite ftpSite)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Update(ftpSite);
                transaction.Commit();
            }
        }

        public AndroAdminDataAccess.Domain.FTPSite GetById(int id)
        {
            AndroAdminDataAccess.Domain.FTPSite ftpSite = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ftpSite = session.Get<AndroAdminDataAccess.Domain.FTPSite>(id);
            }

            return ftpSite;
        }
    }
}