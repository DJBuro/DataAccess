using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using System.Text;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreDevicesDataService : IStoreDevicesDataService 
    {

        public StoreDevice New()
        {
            return new StoreDevice() 
            {
                Id= Guid.NewGuid()
            };
        }

        public IEnumerable<StoreDevice> List()
        {
            var results = Enumerable.Empty<StoreDevice>();
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreDevices
                    .Include(e => e.Device);
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
                    .Include(e=> e.Device)
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
                    .Include(e=> e.Device.ExternalApi)
                    .Include(e=> e.Store);

                var entity = table.FirstOrDefault(e => e.Id ==  id);

                result = entity;
            }

            return result;
        }

        public void Update(StoreDevice model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreDevices.Include(e=> e.Device);
                var entity = table.FirstOrDefault(e => e.Id == model.Id);

                entity.DeviceId = model.DeviceId;
                entity.Parameters = model.Parameters;

                dbContext.SaveChanges();
            }
        }

        public void Create(StoreDevice model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var devicesTable = dbContext.Devices.Include(e=> e.ExternalApi);
                var table = dbContext.StoreDevices;

                if (model.Device == null) 
                {
                    model.Device = devicesTable.SingleOrDefault(e => model.DeviceId == e.Id);
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

                if (entity == null) { return; }

                table.Remove(entity);

                dbContext.SaveChanges();
            }
        }
    }
    
    public class DevicesDataService : IDevicesDataService
    {
        /// <summary>
        /// Adds the ordering devices to store.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="selectedStoreDevices">The selected store types.</param>
        public void AddDevicesToStore(int storeId, IEnumerable<StoreDevice> selectedStoreDevices)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var devices = dbContext.Devices;
                
                var table = dbContext.Stores;
                var entity = table.Single(e => e.Id == storeId);


                if (entity.StoreDevices == null) 
                {
                    entity.StoreDevices = new List<StoreDevice>();
                }

                foreach (var storeDevice in selectedStoreDevices)
                {
                    if (entity.StoreDevices.Any(e => e.StoreId == storeDevice.StoreId)) { continue; }

                    entity.StoreDevices.Add(new StoreDevice()
                    {
                        Device = storeDevice.Device,
                        Parameters = storeDevice.Parameters,
                        Store = entity
                    });
                }

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<Device> List()
        {
            return this.List(e => true);
        }

        public IEnumerable<Device> List(Expression<Func<Device, bool>> query)
        {
            IEnumerable<Device> result = Enumerable.Empty<Device>();
            
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.Devices.Include(e=> e.ExternalApi);
                var tableQuery = table.Where(query);
                result = tableQuery.ToArray();
            }

            return result;
        }

        /// <summary>
        /// Lists the stores.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IEnumerable<Store> ListStores(Expression<Func<Store, bool>> query)
        {
            IEnumerable<Store> result = Enumerable.Empty<Store>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.Stores.Include(e=> e.Chain);
                var tableQuery = table.Where(query);  //table.Where).SelectMany(e => e.Stores);
                result = tableQuery.ToArray();
            }

            return result;
        }

        public IEnumerable<StoreDevice> ListStoreDevice(Expression<Func<StoreDevice, bool>> query)
        {
            IEnumerable<StoreDevice> result = Enumerable.Empty<StoreDevice>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.StoreDevices
                    .Include(e => e.Store)
                    .Include(e => e.Store.Chain)
                    .Include(e => e.Device)
                    .Include(e=> e.Device.ExternalApi);


                var tableQuery = table.Where(query);  //table.Where).SelectMany(e => e.Stores);
                
                result = tableQuery.ToArray();
            }

            return result;
        }

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Device Get(Guid id)
        {
            Device result = null;

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.Devices.Include(e=> e.ExternalApi);
                var tableQuery = table.Where(e=> e.Id == id);
                result = tableQuery.SingleOrDefault();
            }

            return result;
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Create(Device model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.Devices;

                if (table.Any(e => e.Id == model.Id)) { return; }

                table.Add(model);

                dbContext.SaveChanges();
            }
        }

        public Device New()
        {
            return new Device()
            {
                Id = Guid.NewGuid()
            };
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Device model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.Devices;
                var externalAPiTable = dbContext.ExternalApis;

                var entity = table.SingleOrDefault(e => e.Id == model.Id);

                entity.Name = model.Name;
                entity.ExternalApiId = model.ExternalApiId;

                if (entity.ExternalApiId.HasValue) 
                {
                    model.ExternalApi = externalAPiTable.FirstOrDefault(e => e.Id == entity.ExternalApiId);
                    entity.ExternalApi = model.ExternalApi;
                }

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Destroys the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Destroy(Device model)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.Devices;
                var entity = table.SingleOrDefault(e => e.Id == model.Id);

                if (entity != null) 
                {
                    table.Remove(entity);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
