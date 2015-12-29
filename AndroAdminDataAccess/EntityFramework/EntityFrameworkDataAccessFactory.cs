using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.DataAccess;
using AndroAdminDataAccess.EntityFramework.DataAccess;

namespace AndroAdminDataAccess.EntityFramework
{
    public class EntityFrameworkDataAccessFactory
    {
        public IAMSServerDAO AMSServerDAO
        {
            get { return new AMSServerDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IFTPSiteDAO FTPSiteDAO
        {
            get { return new FtpSiteDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IFTPSiteTypeDAO FTPSiteTypeDAO
        {
            get { return new FtpSiteTypeDAO(); }
            set { throw new NotImplementedException(); }
        }

        public ILogDAO LogDAO
        {
            get { return new LogDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreAMSServerDAO StoreAMSServerDAO
        {
            get { return new StoreAMSServerDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreAMSServerFTPSiteDAO StoreAMSServerFTPSiteDAO
        {
            get { return new StoreAMSServerFtpSiteDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreDAO IStoreDAO
        {
            get { return new StoreDAO(); }
            set { throw new NotImplementedException(); }
        }
    }
}
