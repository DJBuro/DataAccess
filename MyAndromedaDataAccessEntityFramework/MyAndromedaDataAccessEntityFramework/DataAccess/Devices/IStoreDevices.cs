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
    public interface IDevicesDataService : IDataProvider<Model.AndroAdmin.Device>
    {
        
    }

    public class DevicesDataService : IDevicesDataService 
    {
        private readonly AndroAdminDbContext dbContext;

        public DevicesDataService() 
        {
            this.dbContext = new Model.AndroAdmin.AndroAdminDbContext();
        }

        public Device Get(Expression<Func<Device, bool>> query)
        {
            var table = this.dbContext.Devices;
            var tableQuery = table.Where(query);

            var device = tableQuery.SingleOrDefault();

            return device;
        }

        public IQueryable<Device> List()
        {
            var table = this.dbContext.Devices.Include(e=> e.ExternalApi);

            return table;
        }

        public IQueryable<Device> List(Expression<Func<Device, bool>> query)
        {
            var table = this.dbContext.Devices.Include(e => e.ExternalApi);

            return table;
        }
    }
}
