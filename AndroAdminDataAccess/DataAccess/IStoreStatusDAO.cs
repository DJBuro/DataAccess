using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreStatusDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<StoreStatus> GetAll();
    }
}