using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreDevicesDataService : IStoreDevicesDataService 
    {
        public StoreDevice New()
        {
            return new StoreDevice()
            {
                Id = Guid.NewGuid()
            };
        }

        public IEnumerable<StoreDevice> List()
        {
            var results = Enumerable.Empty<StoreDevice>();
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreDevices
                                     .Include(e => e.Device)
                                     .Where(e=> !e.Removed);

                results = table.ToArray();
            }

            return results;
        }

        public IEnumerable<StoreDevice> List(Expression<Func<StoreDevice, bool>> query)
        {
            var results = Enumerable.Empty<StoreDevice>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.StoreDevices
                                     .Include(e => e.Device)
                                     .Where(e=> !e.Removed)
                                     .Where(query);

                results = table.ToArray();
            }

            return results;
        }

        public IEnumerable<StoreDevice> ListRemoved(Expression<Func<StoreDevice, bool>> query)
        {
            var results = Enumerable.Empty<StoreDevice>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.StoreDevices
                                     .Include(e => e.Device)
                                     .Where(e=> e.Removed)
                                     .Where(query);
                results = table.ToArray();
            }

            return results;
        }

        public StoreDevice Get(Guid id)
        {
            StoreDevice result;
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.StoreDevices
                                     .Include(e => e.Device)
                                     .Include(e => e.Device.ExternalApi)
                                     .Include(e => e.Store);

                var entity = table.FirstOrDefault(e => e.Id == id);

                result = entity;
            }

            return result;
        }

        public void Update(StoreDevice model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreDevices.Include(e => e.Device);
                var entity = table.FirstOrDefault(e => e.Id == model.Id);

                entity.DataVersion = dbContext.GetNextDataVersionForEntity();
                entity.DeviceId = model.DeviceId;
                entity.Parameters = model.Parameters;

                dbContext.SaveChanges();
            }
        }

        public void Create(StoreDevice model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var devicesTable = dbContext.Devices.Include(e => e.ExternalApi);
                var table = dbContext.StoreDevices;

                if (model.Device == null) 
                {
                    model.Device = devicesTable.SingleOrDefault(e => model.DeviceId == e.Id);
                    model.DataVersion = dbContext.GetNextDataVersionForEntity();
                }

                table.Add(model);

                dbContext.SaveChanges();
            }
        }

        public void Delete(StoreDevice model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.StoreDevices;
                var entity = table.FirstOrDefault(e => e.Id == model.Id);

                if (entity == null)
                {
                    return;
                }

                entity.DataVersion = dbContext.GetNextDataVersionForEntity();
                entity.Removed = true;
                //table.Remove(entity);

                dbContext.SaveChanges();
            }
        }


        
    }
}