using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudHelper;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface ISiteDataAccess
    {
        string GetById(int siteId, out MyAndromedaDataAccess.Domain.Site site);

        string GetByMyAndromedaUserId(int myAndromedaUserId, out List<MyAndromedaDataAccess.Domain.Site> sites);
    }
}
