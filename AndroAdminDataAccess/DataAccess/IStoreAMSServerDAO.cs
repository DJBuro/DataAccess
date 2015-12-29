using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreAMSServerDAO
    {
        IEnumerable<StoreAMSServer> GetAll();
        void Add(StoreAMSServer storeAMSServer);
        StoreAMSServer GetByStoreIdAMServerId(int storeId, int amsServerId);
    }
}