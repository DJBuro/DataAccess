using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IMenuDataAccess
    {
        bool Put(string sessionToken, string data);
        SiteMenu Get(string sessionToken, string siteID);
    }
}
