using AndroCloudDataAccess.Domain;
using System;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IMyAndromedaUserDataAccess
    {
        bool ValidateUser(string username, string password);
        string GetByUsername(string username, out MyAndromedaUser myAndromedaUser);
    }
}
