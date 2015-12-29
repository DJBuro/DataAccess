using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
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

                entity.DataVersion = dbContext.GetNextDataVersionForEntity();
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
                var tableQuery = table
                    .Where(e=> !e.Removed)
                    .Where(query);
                
                result = tableQuery.ToArray();
            }

            return result;
        }

        public IEnumerable<Device> ListRemoved(Expression<Func<Device, bool>> query)
        {
            IEnumerable<Device> result = Enumerable.Empty<Device>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.Devices
                    .Include(e => e.ExternalApi)
                    .Where(e=> e.Removed);

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


                var tableQuery = table
                    .Where(e=> !e.Removed)
                    .Where(query);  //table.Where).SelectMany(e => e.Stores);
                
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

                model.DataVersion = dbContext.GetNextDataVersionForEntity();
                table.Add(model);

                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// News this instance.
        /// </summary>
        /// <returns></returns>
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
                entity.DataVersion = dbContext.GetNextDataVersionForEntity();

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

                if (entity == null) 
                {
                    return;    
                }

                entity.DataVersion = dbContext.GetNextDataVersionForEntity();
                entity.Removed = true;

                dbContext.SaveChanges();
            }
        }
    }
}
