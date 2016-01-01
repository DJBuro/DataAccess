using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IAddressDataAccess
    {
        string UpsertBySiteId(int siteId, Address address);
    }
}
