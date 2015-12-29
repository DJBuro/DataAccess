using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IFTPSiteDAO
    {
        IList<FTPSite> GetAll();
        void Add(FTPSite amsServer);
        void Update(FTPSite amsServer);
        FTPSite GetById(int id);
        FTPSite GetByName(string name);
    }
}