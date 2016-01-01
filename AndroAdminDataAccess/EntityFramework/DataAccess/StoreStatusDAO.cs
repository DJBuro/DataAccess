using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreStatusDAO : IStoreStatusDAO
    {
        public IList<Domain.StoreStatus> GetAll()
        {
            List<Domain.StoreStatus> models = new List<Domain.StoreStatus>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.StoreStatus1
                            select s;

                foreach (var entity in query)
                {
                    Domain.StoreStatus model = new Domain.StoreStatus()
                    {
                        Id = entity.Id,
                        Status = entity.Status,
                        Description = entity.Description
                    };

                    models.Add(model);
                }
            }

            return models;
        }
    }
}