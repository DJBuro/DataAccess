using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface ISettingsDAO
    {
        string ConnectionStringOverride { get; set; }
        string GetByName(string name, out string value);
    }
}