using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreGPSSettingsDAO
    {
        string ConnectionStringOverride { get; set; }

        StoreGPSSettings GetById(int id);
        bool Add(StoreGPSSettings storeBringgSettings);
        bool Update(StoreGPSSettings storeBringgSettings);
    }
}