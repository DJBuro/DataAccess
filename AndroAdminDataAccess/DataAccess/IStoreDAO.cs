using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreDAO
    {
        IList<Store> GetAll();
        void Add(Store store);
        void Update(Store store);
        Store GetById(int id);
        Store GetByAndromedaId(int id);
        Store GetByName(string name);
        IList<Domain.Store> GetByACSApplicationId(int acsApplicationId);
    }
}