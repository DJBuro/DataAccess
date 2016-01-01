﻿using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class ACSApplicationDataAccess : IACSApplicationDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string Get(string externalApplicationId, out AndroCloudDataAccess.Domain.ACSApplication acsApplication)
        {
            acsApplication = null;

            using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
            {
                var query = from p in acsEntities.ACSApplications
                            where p.ExternalApplicationId == externalApplicationId
                            select p;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    acsApplication = new AndroCloudDataAccess.Domain.ACSApplication();
                    acsApplication.Id = entity.Id;
                    acsApplication.Name = entity.Name;
                    acsApplication.ExternalApplicationId = entity.ExternalApplicationId;
                }
            }

            return "";
        }

        public bool StoreExists(Guid existingSiteId, int acsApplicationId)
        {
            using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
            {
                var query = from acsa in acsEntities.ACSApplications
                            join acss in acsEntities.ACSApplicationSites
                            on acsa.Id equals acss.ACSApplicationId
                            where acsa.Id == acsApplicationId
                            && acss.SiteId == existingSiteId
                            select acss;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
