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
    public class FtpSiteTypeDAO : IFTPSiteTypeDAO
    {
        public IList<FTPSiteType> GetAll()
        {
            IList<FTPSiteType> ftpSiteTypes = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ftpSiteTypes = session.CreateQuery("from " + typeof(FTPSiteType)).List<FTPSiteType>();
            }

            return ftpSiteTypes;
        }
    }
}