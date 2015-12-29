using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudHelper;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface ISiteDetailsDataAccess
    {
        string GetBySiteId(int siteId, out SiteDetails siteDetails);
        string Update(int siteId, SiteDetails siteDetails);
    }
}
