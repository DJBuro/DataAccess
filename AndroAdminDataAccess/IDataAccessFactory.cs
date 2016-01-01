using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess
{
    public interface IDataAccessFactory
    {
        IAMSServerDAO AMSServerDAO { get; set; }
        IFTPSiteDAO FTPSiteDAO { get; set; }
        IFTPSiteTypeDAO FTPSiteTypeDAO { get; set; }
        ILogDAO LogDAO { get; set; }
        IStoreAMSServerDAO StoreAMSServerDAO { get; set; }
        IStoreAMSServerFTPSiteDAO StoreAMSServerFTPSiteDAO { get; set; }
        IStoreDAO StoreDAO { get; set; }
    }
}
