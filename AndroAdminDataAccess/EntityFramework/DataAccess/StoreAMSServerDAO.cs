using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreAMSServerDAO : IStoreAMSServerDAO
    {
        public IEnumerable<Domain.StoreAMSServer> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Domain.StoreAMSServer storeAMSServer)
        {
            throw new NotImplementedException();
        }

        public Domain.StoreAMSServer GetByStoreIdAMServerId(int storeId, int amsServerId)
        {
            throw new NotImplementedException();
        }
    }
}