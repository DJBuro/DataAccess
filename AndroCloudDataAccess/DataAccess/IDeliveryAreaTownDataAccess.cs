using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IDeliveryAreaTownDataAccess
    {
        string ConnectionStringOverride { get; set; }

        string GetByApplicationId(int applicationId, out List<DeliveryZoneTown> deliveryZoneTowns);
    }
}
