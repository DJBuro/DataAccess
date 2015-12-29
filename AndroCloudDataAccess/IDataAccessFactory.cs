using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.DataAccess;

namespace AndroCloudDataAccess
{
    public interface IDataAccessFactory
    {
        ISiteDataAccess SiteDataAccess { get; set; }
        IOrdersDataAccess OrderDataAccess { get; set; }
        IAuditDataAccess AuditDataAccess { get; set; }
        IPartnersDataAccess PartnerDataAccess { get; set; }
        IGroupsDataAccess GroupDataAccess { get; set; }
        ISiteMenuDataAccess SiteMenuDataAccess { get; set; }
        ISiteDetailsDataAccess SiteDetailsDataAccess { get; set; }
        IOrderStatusDataAccess OrderStatusDataAccess { get; set; }
        IMyAndromedaUserDataAccess MyAndromedaUserDataAccess { get; set; }
    }
}
