using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreMenuThumbnailsDataService 
    {
        /// <summary>
        /// Gets the store menu changes after data version.
        /// </summary>
        /// <param name="fromVersion">From version.</param>
        /// <returns></returns>
        IEnumerable<Domain.StoreMenu> GetStoreMenuChangesAfterDataVersion(int fromVersion);

        /// <summary>
        /// Gets the store menu thumbnail changes after data version.
        /// </summary>
        /// <param name="fromVersion">From version.</param>
        /// <returns></returns>
        IEnumerable<Domain.StoreMenuThumbnails> GetStoreMenuThumbnailChangesAfterDataVersion(int fromVersion);
    }
}
