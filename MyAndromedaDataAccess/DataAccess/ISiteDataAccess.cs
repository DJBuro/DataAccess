using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface ISiteDataAccess : IDependency
    {
        string GetById(int siteId, out MyAndromedaDataAccess.Domain.Site site);

        string GetAcsApplicationIds(int siteId, out IEnumerable<int> application);
        string GetExternalApplicationIds(int sideIT, out IEnumerable<string> externalApplicationIds);
        string GetByMyAndromedaUserId(int myAndromedaUserId, out List<MyAndromedaDataAccess.Domain.Site> sites);
    }
}
