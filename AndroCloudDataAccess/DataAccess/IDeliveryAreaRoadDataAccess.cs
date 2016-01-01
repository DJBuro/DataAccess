using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IDeliveryAreaRoadDataAccess
    {
        string ConnectionStringOverride { get; set; }

        string GetByApplicationIdPostcode(int applicationId, string postcode, out List<DeliveryZoneRoad> deliveryZoneRoads);
    }
}
