using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class OrderStatusDataAccess : IOrderStatusDataAccess
    {
        public string GetByRamesesStatusId(int ramesesStatusId, out AndroCloudDataAccess.Domain.OrderStatus orderStatus)
        {
            orderStatus = null;
            var acsEntities = new ACSEntities();

            var acsQuery = from os in acsEntities.OrderStatus1
                           where os.RamesesStatusId == ramesesStatusId
                           select os;

            var acsQueryEntity = acsQuery.FirstOrDefault();

            if (acsQueryEntity != null)
            {
                orderStatus = new AndroCloudDataAccess.Domain.OrderStatus();
                orderStatus.Id = acsQueryEntity.ID;
                orderStatus.RamesesStatusId = acsQueryEntity.RamesesStatusId;
                orderStatus.Description = acsQueryEntity.Description;
            }

            return "";
        }
    }
}
