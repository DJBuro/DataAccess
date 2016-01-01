using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StorePaymentProviderDAO : IStorePaymentProviderDAO
    {
        public string ConnectionStringOverride { get; set; }

        IList<Domain.StorePaymentProvider> IStorePaymentProviderDAO.GetAll()
        {
            List<Domain.StorePaymentProvider> models = new List<Domain.StorePaymentProvider>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StorePaymentProviders
                            select s;

                foreach (var entity in query)
                {
                    Domain.StorePaymentProvider model = new Domain.StorePaymentProvider()
                    {
                        Id = entity.Id,
                        DisplayText = entity.DisplayText,
                        ProviderName = entity.ProviderName,
                        ClientId = entity.ClientId,
                        ClientPassword = entity.ClientPassword
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public void Add(Domain.StorePaymentProvider storePaymentProvider)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                StorePaymentProvider entity = new StorePaymentProvider()
                {
                    DisplayText = storePaymentProvider.DisplayText,
                    ProviderName = storePaymentProvider.ProviderName,
                    ClientId = storePaymentProvider.ClientId,
                    ClientPassword = storePaymentProvider.ClientPassword
                };

                entitiesContext.StorePaymentProviders.Add(entity);
                entitiesContext.SaveChanges();
            }
        }

        public void Update(Domain.StorePaymentProvider store)
        {
            throw new NotImplementedException();
        }

        Domain.StorePaymentProvider IStorePaymentProviderDAO.GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Domain.StorePaymentProvider> GetAfterDataVersion(int dataVersion)
        {
            throw new NotImplementedException();
        }
    }
}