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
    public interface IDeliveryZoneDataService : IDataProvider<DeliveryArea>, IDependency
    {
        IList<DeliveryArea> Get(int storeId);
        void Create(DeliveryArea deliveryArea);
        void Update(DeliveryArea deliveryArea);
        bool Delete(DeliveryArea deliveryArea);
        bool Delete(int storeId);
    }
}
