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
    public class StoreGPSSettingsDAO : IStoreGPSSettingsDAO
    {
        public string ConnectionStringOverride { get; set; }

        public Domain.StoreGPSSettings GetById(int id)
        {
            Domain.StoreGPSSettings model = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreGPSSettings
                            where s.StoreId == id
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new StoreGPSSettings()
                    {
                        Id = entity.StoreId,
                        MaxDrivers = entity.MaxDrivers,
                        PartnerConfig = entity.PartnerConfig
                    };
                }
            }

            return model;
        }

        public bool Add(Domain.StoreGPSSettings model)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var entity = new StoreGPSSetting()
                {
                    PartnerConfig = model.PartnerConfig,
                    MaxDrivers = model.MaxDrivers,
                    StoreId = model.Id
                };

                entitiesContext.StoreGPSSettings.Add(entity);
                entitiesContext.SaveChanges();
            }

            return true;
        }

        public bool Update(Domain.StoreGPSSettings model)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreGPSSettings
                            where s.StoreId == model.Id
                            select s;
                var entity = query.FirstOrDefault();

                if (entity == null)
                {
                    return false;
                }
                else
                {
                    entity.MaxDrivers = model.MaxDrivers;
                    entity.PartnerConfig = model.PartnerConfig;
                    entity.StoreId = model.Id;

                    entitiesContext.SaveChanges();
                }
            }

            return true;
        }
    }
}