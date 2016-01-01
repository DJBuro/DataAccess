using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AndroAdminDataAccess.EntityFramework;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreDevicesDataService 
    {
        StoreDevice New();
        IEnumerable<StoreDevice> List();
        IEnumerable<StoreDevice> List(Expression<Func<StoreDevice, bool>> query);

        StoreDevice Get(Guid id);
        void Update(StoreDevice model);
        void Create(StoreDevice model);
        void Delete(StoreDevice model);
    }
    public interface IDevicesDataService 
    {
        /// <summary>
        /// Adds the devices to store.
        /// </summary>
        /// <param name="storeId">The store id.</param>
        /// <param name="storeDevices">The store devices.</param>
        void AddDevicesToStore(int storeId, IEnumerable<StoreDevice> storeDevices);

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Device> List();

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<Device> List(Expression<Func<Device, bool>> query);

        /// <summary>
        /// Lists the stores.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<Store> ListStores(Expression<Func<Store, bool>> query);

        /// <summary>
        /// Lists the store devices.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<StoreDevice> ListStoreDevice(Expression<Func<StoreDevice, bool>> query);

        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Device Get(Guid id);

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Create(Device model);

        Device New();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(Device model);

        /// <summary>
        /// Destroys the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Destroy(Device model);
    }
}