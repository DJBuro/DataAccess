using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Devices
{
    public class DevicesDataService : IDevicesDataService 
    {
        private readonly AndroAdminDbContext dbContext;

        public DevicesDataService() 
        {
            this.dbContext = new Model.AndroAdmin.AndroAdminDbContext();
        }

        public Device New()
        {
            return new Device() 
            { 
                Id = Guid.NewGuid()
            };
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
            var table = this.dbContext.Devices.Include(e => e.ExternalApi);

            return table;
        }

        public IQueryable<Device> List(Expression<Func<Device, bool>> query)
        {
            var table = this.dbContext.Devices.Include(e => e.ExternalApi);

            return table;
        }
    }
}