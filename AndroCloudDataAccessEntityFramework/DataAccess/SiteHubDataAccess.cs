using System;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using CloudSyncModel.Hubs;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SiteHubDataAccess : ISiteHubDataAccess
    {
        public string ConnectionStringOverride { get; set; }
          
        public void ClearByHub(HubHostModel withHub)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void ResetSiteHardwareKey(SiteHubReset reset)
        {
            // TODO: Implement this method
            // -> update Site.HardwareKey => null 
            throw new NotImplementedException();
        }

        public void AddLink(SiteHubs model)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void ClearByHub(HubHost withHub)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}