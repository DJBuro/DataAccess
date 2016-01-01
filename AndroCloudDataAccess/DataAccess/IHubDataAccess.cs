using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudSyncModel.Hubs;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IHubDataAccess 
    {
        void AddOrUpdate(HubHostModel model);

        bool TryToRemoveHub(Guid id);
    }

    public interface ISiteHubDataAccess
    { 
        void AddLink(SiteHubs model);

        void ClearByHub(HubHostModel withHub);

        /// <summary>
        /// Resets the Site.HardwareKey. Setting the value to null
        /// </summary>
        /// <param name="reset">The reset.</param>
        void ResetSiteHardwareKey(SiteHubReset reset);
    }
}
