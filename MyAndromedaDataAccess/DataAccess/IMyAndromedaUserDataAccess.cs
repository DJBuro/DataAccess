using MyAndromedaDataAccess.Domain;
using System;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IMyAndromedaUserDataAccess
    {
        bool ValidateUser(string username, string password);
        string GetByUsername(string username, out MyAndromedaUser myAndromedaUser);
        bool CanAccessStoreByExternalStoreId(string userName, string externalStoreId, out MyAndromedaUser myAndromedaUser, out int storeId);
    }
}
