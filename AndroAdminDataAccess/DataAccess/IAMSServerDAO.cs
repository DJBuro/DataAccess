using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IAMSServerDAO
    {
        IList<AMSServer> GetAll();
        void Add(AMSServer amsServer);
        void Update(AMSServer amsServer);
        AMSServer GetById(int id);
    }
}
