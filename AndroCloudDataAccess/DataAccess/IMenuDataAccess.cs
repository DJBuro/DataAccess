using AndroCloudDataAccess.Domain;
using System;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IMenuDataAccess
    {
        bool Put(Guid sessionToken, string data, int version, string dataType);
        SiteMenu Get(string sessionToken, string siteID);

    }
}
