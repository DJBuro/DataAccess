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
        void DeleteByAMSServerId(int amsServerId);
        void DeleteById(int id);
        StoreAMSServer GetByStoreIdAMServerId(int storeId, int amsServerId);
    }
}