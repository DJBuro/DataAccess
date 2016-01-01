using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface ICountryDAO
    {
        string ConnectionStringOverride { get; set; }
        List<Country> GetAll();
    }
}