using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;

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
            // We will use transactionscope to implicitly enrole both EF and direct SQL in the same transaction
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    entitiesContext.Database.Connection.Open();

                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                    StorePaymentProvider entity = new StorePaymentProvider()
                    {
                        DisplayText = storePaymentProvider.DisplayText,
                        ProviderName = storePaymentProvider.ProviderName,
                        ClientId = storePaymentProvider.ClientId,
                        ClientPassword = storePaymentProvider.ClientPassword,
                        DataVersion = newVersion
                    };

                    entitiesContext.StorePaymentProviders.Add(entity);
                    entitiesContext.SaveChanges();

                    // Commit the transacton
                    transactionScope.Complete();
                }
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
            List<Domain.StorePaymentProvider> models = new List<Domain.StorePaymentProvider>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StorePaymentProviders
                            where s.DataVersion > dataVersion
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
    }
}