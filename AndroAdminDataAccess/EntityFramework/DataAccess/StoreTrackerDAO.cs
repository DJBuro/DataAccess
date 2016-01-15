using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreTrackerDAO : IStoreTrackerDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.StoreTracker> GetAll()
        {
            List<Domain.StoreTracker> models = new List<Domain.StoreTracker>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreTrackers
                            select s;

                foreach (var entity in query)
                {
                    Domain.StoreTracker model = new Domain.StoreTracker()
                    {
                        Id = entity.StoreId,
                        IMEI = entity.IMEI
                    };

                    models.Add(model);
                }
            }

            return models;
        }
    }
}