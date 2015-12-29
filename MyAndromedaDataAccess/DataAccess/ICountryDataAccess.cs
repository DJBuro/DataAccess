using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudHelper;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface ICountryDataAccess
    {
        string GetAll(out List<MyAndromedaDataAccess.Domain.Country> countries);
    }
}
