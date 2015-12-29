using System.Linq.Expressions;
using MyAndromeda.Core;
using MyAndromeda.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System.Data.Entity;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Devices
{
    public interface IDevicesDataService : IDataProvider<Model.AndroAdmin.Device>, IDependency
    {
        
    }
}
