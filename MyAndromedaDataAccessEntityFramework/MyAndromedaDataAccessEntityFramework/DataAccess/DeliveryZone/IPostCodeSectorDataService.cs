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
    public interface IPostCodeSectorDataService : IDataProvider<PostCodeSector>, IDependency
    {
        IList<PostCodeSector> Get(int deliveryZoneNameId);
        void Create(PostCodeSector postCodeSector);
        void Update(PostCodeSector postCodeSector);
        bool Delete(PostCodeSector postCodeSector);
        bool Delete(int deliveryZoneNameId);
    }
}
