using System;
using System.Linq;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface ISiteDetailsDataAccess
    {
        string GetBySiteId(int siteId, out SiteDetails siteDetails);
        string Update(int siteId, SiteDetails siteDetails);
    }
}
