using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IAMSServerFTPServerPairDAO
    {
        void Add(AMSServerFTPServerPair amsServerFTPServerPair);
        void Delete(int id);
        void Update(AMSServerFTPServerPair amsServerFTPServerPair);
    }
}
