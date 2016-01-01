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
            this.Table = this.dbContext.Set<Device>();
            this.TableQuery = this.Table.Include(e => e.ExternalApi);
        }

        public DbSet<Device> Table { get; set; }
        public IQueryable<Device> TableQuery { get; set; }

        public void ChangeIncludeScope<TPropertyModel>(Expression<Func<Device, TPropertyModel>> predicate)
        {
            this.TableQuery = TableQuery.Include(predicate);
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
            var tableQuery = this.TableQuery.Where(query);

            var device = tableQuery.SingleOrDefault();

            return device;
        }

        public IQueryable<Device> List()
        {
            return this.TableQuery;
        }

        public IQueryable<Device> List(Expression<Func<Device, bool>> query)
        {
            return this.TableQuery.Where(query);
        }
    }
}