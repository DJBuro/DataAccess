using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreDriverDAO
    {
        string ConnectionStringOverride { get; set; }
        List<StoreDriver> GetAllStoreDrivers(int storeId);
        bool Add(StoreDriver storeDriver);
        bool Update(StoreDriver storeDriver);
        bool Delete(StoreDriver storeDriver);
    }
}