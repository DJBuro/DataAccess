using MyAndromeda.Core;
using MyAndromeda.Core.Data;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.DeliveryZone
{
    public interface IDeliveryZoneNameDataService : IDataProvider<DeliveryZoneName>, IDependency
    {
        IList<DeliveryZoneName> Get(int storeId);
        void Create(DeliveryZoneName deliveryZoneName);
        void Update(DeliveryZoneName deliveryZoneName);
        bool Delete(DeliveryZoneName deliveryZoneName);
        bool Delete(int storeId);
    }
}
