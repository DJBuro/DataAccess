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

        StoreBringgSettings GetById(int id);
        bool Add(StoreBringgSettings storeBringgSettings);
        bool Update(StoreBringgSettings storeBringgSettings);
    }
}