using System;
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
        public string Get(string externalApplicationId, out AndroCloudDataAccess.Domain.ACSApplication acsApplication)
        {
            acsApplication = null;
            var acsEntities = new ACSEntities();

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

            return "";
        }

        public bool StoreExists(Guid siteId, Guid applicationId)
        {
            var acsEntities = new ACSEntities();

            var query = from p in acsEntities.ACSApplicationSites
                        where p.ACSApplicationId == applicationId
                        && s.AndroID == androStoreId
                        select p;

            var entity = query.FirstOrDefault();

            if (entity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
