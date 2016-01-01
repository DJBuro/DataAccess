using MyAndromedaDataAccess.Domain;
using System;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IMyAndromedaUserDataAccess
    {
        bool ValidateUser(string username, string password);
        string GetByUsername(string username, out MyAndromedaUser myAndromedaUser);
        bool CanAccessStoreByExternalSiteId(string userName, string customerSiteId, out MyAndromedaUser myAndromedaUser, out int storeId);
    }
}
