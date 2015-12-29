using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.DataAccess;

namespace AndroCloudDataAccess
{
    public interface IDataAccessFactory
    {
        IHostDataAccess HostDataAccess { get; set; }
        ISiteDataAccess SiteDataAccess { get; set; }
        IOrdersDataAccess OrderDataAccess { get; set; }
        IAuditDataAccess AuditDataAccess { get; set; }
        ISiteMenuDataAccess SiteMenuDataAccess { get; set; }
        ISiteDetailsDataAccess SiteDetailsDataAccess { get; set; }
        IOrderStatusDataAccess OrderStatusDataAccess { get; set; }
        IAddressDataAccess AddressDataAccess { get; set; }
        IACSApplicationDataAccess AcsApplicationDataAccess { get; set; }
        ISettingsDataAccess SettingsDataAccess { get; set; }
        IDeliveryAreaDataAccess DeliveryZoneDataAccess { get; set; }
        IDeliveryAreaTownDataAccess DeliveryAreaTownDataAccess { get; set; }
        IDeliveryAreaRoadDataAccess DeliveryAreaRoadDataAccess { get; set; }
    }
}
