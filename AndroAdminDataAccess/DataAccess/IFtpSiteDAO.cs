using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IFtpSiteDAO
    {
        IEnumerable<AndroAdminDataAccess.Domain.FTPSite> GetAll();
        void Add(AndroAdminDataAccess.Domain.FTPSite amsServer);
        void Update(AndroAdminDataAccess.Domain.FTPSite amsServer);
        AndroAdminDataAccess.Domain.FTPSite GetById(int id);
    }
}