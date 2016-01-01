using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IAMSServerDAO
    {
        IEnumerable<AndroAdminDataAccess.Domain.AMSServer> GetAll();
        void Add(AndroAdminDataAccess.Domain.AMSServer amsServer);
        void Update(AndroAdminDataAccess.Domain.AMSServer amsServer);
        AndroAdminDataAccess.Domain.AMSServer GetById(int id);
    }
}
