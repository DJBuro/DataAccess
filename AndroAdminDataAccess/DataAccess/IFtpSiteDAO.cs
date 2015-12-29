using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IFTPSiteDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<FTPSite> GetAll();
        void Add(FTPSite ftpSite);
        void Update(FTPSite ftpSite);
        void Delete(int ftpSiteId);
        FTPSite GetById(int id);
        FTPSite GetByName(string name);
        IList<Domain.FTPSite> GetByChainId(int chainId);
    }
}