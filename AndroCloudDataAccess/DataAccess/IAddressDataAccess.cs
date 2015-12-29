using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IAddressDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string UpsertByExternalSiteIdMyAndromedaUserId(string externalSiteId, string myAndromedaUserId, Address address);
    }
}
