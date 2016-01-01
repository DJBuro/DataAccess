using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IMenuDataAccess
    {
        bool Put(string sessionToken, string data, int version);
        SiteMenu Get(string sessionToken, string siteID);
    }
}
