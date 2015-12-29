using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class DeliveryZoneRoadsDataAccess : IDeliveryAreaRoadDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetByApplicationIdPostcode(int applicationId, string postcode, out List<DeliveryZoneRoad> deliveryZoneRoads)
        {
            deliveryZoneRoads = new List<DeliveryZoneRoad>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var deliveryZonesQuery = from dz in acsEntities.DeliveryAreaRoads
                                         join s in acsEntities.Sites
                                            on dz.SiteId equals s.ID
                                         where dz.ApplicationId == applicationId
                                            && dz.Postcode == postcode
                                         select new
                                         {
                                             DeliversToWholeRoad = dz.DeliversToWholeRoad,
                                             ExternalSiteId = s.ExternalId,
                                             HouseNumberEnd = dz.HouseNumberEnd,
                                             HouseNumberStart = dz.HouseNumberStart,
                                             Postcode = dz.Postcode,
                                             RoadName = dz.RoadName,
                                             RuleId = dz.RuleId
                                         };

                foreach (var deliveryZoneTown in deliveryZonesQuery)
                {
                    deliveryZoneRoads.Add
                    (
                        new DeliveryZoneRoad()
                        {
                            DeliversToWholeRoad = deliveryZoneTown.DeliversToWholeRoad,
                            ExternalSiteId = deliveryZoneTown.ExternalSiteId,
                            HouseNumberEnd = deliveryZoneTown.HouseNumberEnd == null ? null : deliveryZoneTown.HouseNumberEnd.ToString(),
                            HouseNumberStart = deliveryZoneTown.HouseNumberStart == null ? null : deliveryZoneTown.HouseNumberStart.ToString(),
                            Postcode = deliveryZoneTown.Postcode,
                            RoadName = deliveryZoneTown.RoadName,
                            RuleId = deliveryZoneTown.RuleId
                        }
                    );
                }
            }

            return "";
        }
    }
}
