using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Data.Common;
using System.Globalization;
using System.Transactions;
using System.Data.Entity;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreDriverDAO : IStoreDriverDAO
    {
        public string ConnectionStringOverride { get; set; }

        public List<Domain.StoreDriver> GetAllStoreDrivers(int storeId)
        {
            List<Domain.StoreDriver> models = new List<Domain.StoreDriver>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from sd in entitiesContext.StoreDrivers
                            where sd.StoreId == storeId
                            select sd;

                if (query != null)
                {
                    foreach (var storeDriverEntity in query)
                    {
                        Domain.StoreDriver storeDriver = new Domain.StoreDriver()
                        {
                            Id = storeDriverEntity.Id,
                            Name = storeDriverEntity.Name,
                            PartnerId = storeDriverEntity.PartnerId,
                            StoreId = storeDriverEntity.StoreId,
                            Phone = storeDriverEntity.Phone
                        };

                        models.Add(storeDriver);
                    }
                }
            }

            return models;
        }

        public bool Add(Domain.StoreDriver model)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var entity = new StoreDriver()
                {
                    Name = model.Name,
                    PartnerId = model.PartnerId,
                    StoreId = model.StoreId,
                    Phone = model.Phone
                };

                entitiesContext.StoreDrivers.Add(entity);
                entitiesContext.SaveChanges();
            }

            return true;
        }

        public bool Update(Domain.StoreDriver model)
        {
            bool success = false;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from sd in entitiesContext.StoreDrivers
                            where sd.StoreId == model.StoreId
                            && sd.PartnerId == model.PartnerId
                            select sd;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = model.Name;
                    entity.PartnerId = model.PartnerId;
                    entity.StoreId = model.StoreId;
                    entity.Phone = model.Phone;

                    entitiesContext.SaveChanges();
                    success = true;
                }
            }

            return success;
        }

        public bool Delete(Domain.StoreDriver model)
        {
            bool success = false;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from sd in entitiesContext.StoreDrivers
                            where sd.StoreId == model.StoreId
                            && sd.PartnerId == model.PartnerId
                            select sd;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entitiesContext.StoreDrivers.Remove(entity);
                    entitiesContext.SaveChanges();
                    success = true;
                }
            }

            return success;
        }
    }
}