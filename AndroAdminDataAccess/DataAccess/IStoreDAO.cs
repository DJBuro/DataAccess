using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<StoreListItem> GetAllStoreListItems();
        IList<Store> GetAll();
        void Add(Store store);
        void Update(Store store);
        Store GetById(int id);
        Store GetByAndromedaId(int id);
        Store GetByName(string name);
        IList<Domain.Store> GetByACSApplicationId(int acsApplicationId);
        IList<Domain.Store> GetAfterDataVersion(int dataVersion);
        IList<Domain.Store> GetByACSApplicationIdAfterDataVersion(int acsApplicationId, int dataVersion);
        IList<Domain.Store> GetAllStores();
    }
}