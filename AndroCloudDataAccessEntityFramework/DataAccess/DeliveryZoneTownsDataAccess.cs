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
    public class DeliveryZoneTownsDataAccess : IDeliveryAreaTownDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetByApplicationId(int applicationId, out List<DeliveryZoneTown> deliveryZoneTowns)
        {
            deliveryZoneTowns = new List<DeliveryZoneTown>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var deliveryZonesQuery = from dz in acsEntities.DeliveryAreaTowns
                                         where dz.ApplicationId == applicationId
                                         select dz;

                foreach (var deliveryZoneTown in deliveryZonesQuery)
                {
                    deliveryZoneTowns.Add
                    (
                        new DeliveryZoneTown()
                        {
                            Name = deliveryZoneTown.Name,
                            Postcodes = deliveryZoneTown.Postcodes
                        }
                    );
                }
            }

            return "";
        }
    }
}
