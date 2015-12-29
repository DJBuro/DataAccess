using System;
using System.Data;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class DeliveryZoneDataAccess : IDeliveryAreaDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetBySiteId(Guid siteId, out List<string> deliveryZones)
        {
            deliveryZones = new List<string>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var deliveryZonesQuery = from dz in acsEntities.DeliveryAreas
                                         where dz.SiteId == siteId
                                         select dz;

                foreach (var deliveryZone in deliveryZonesQuery)
                {
                    deliveryZones.Add(deliveryZone.DeliveryArea1);
                }
            }

            return "";
        }
    }
}
