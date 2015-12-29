using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IHubDataService
    {
        IEnumerable<Domain.HubItem> GetAfterDataVersion(int fromVersion);

        /// <summary>
        /// Adds the specified db model.
        /// </summary>
        /// <param name="dbModel">The db model.</param>
        void Add(Domain.HubItem dbModel);

        /// <summary>
        /// Updates the specified db model.
        /// </summary>
        /// <param name="dbModel">The db model.</param>
        void Update(Domain.HubItem dbModel);

        /// <summary>
        /// Removes the specified db model.
        /// </summary>
        /// <param name="dbModel">The db model.</param>
        void Remove(Domain.HubItem dbModel);

        /// <summary>
        /// Gets the hub.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        Domain.HubItem GetHub(Guid id);

        /// <summary>
        /// Gets the hubs.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.HubItem> GetHubs();
    }

    public interface IStoreHubDataService
    {
        /// <summary>
        /// Adds to.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="hub">The hub.</param>
        void AddTo(Store store, HubItem hub);

        /// <summary>
        /// Removes from.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="removeItem">The remove item.</param>
        void RemoveFrom(Store store, HubItem removeItem);

        /// <summary>
        /// Gets the hub items.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Domain.HubItem> GetHubItems();

        /// <summary>
        /// Gets the sites using hub.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        IEnumerable<Domain.StoreHub> GetSitesUsingHub(Guid id);

        /// <summary>
        /// Gets the selected hubs.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <returns></returns>
        IEnumerable<Domain.StoreHub> GetSelectedHubs(Store store);
    }

    public interface IHubResetDataService 
    {
        IEnumerable<Domain.Store> GetStoresToResetAfterDataVersion(int fromVersion);

        void ResetStore(int storeId);
    }
}
