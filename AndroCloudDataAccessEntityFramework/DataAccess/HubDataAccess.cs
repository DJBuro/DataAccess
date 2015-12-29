﻿using System;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using CloudSyncModel.Hubs;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class HubDataAccess : IHubDataAccess 
    {
        public Model.ACSEntities AcsEntities { get; set; }
        public string ConnectionStringOverride { get; set; }
        
        public void AddOrUpdate(HubHostModel model)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public void AddOrUpdate(HubHost model)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public bool TryToRemoveHub(Guid id)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        
    }
}
