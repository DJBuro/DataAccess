using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IFTPSiteTypeDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<FTPSiteType> GetAll();
    }
}