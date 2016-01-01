using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class StorePaymentProviderDataAccess : IStorePaymentProviderDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetById(int id, out AndroCloudDataAccess.Domain.StorePaymentProvider storePaymentProvider)
        {
            storePaymentProvider = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from os in acsEntities.StorePaymentProviders
                               where os.Id == id
                               select os;

                var acsQueryEntity = acsQuery.FirstOrDefault();

                if (acsQueryEntity != null)
                {
                    storePaymentProvider = new AndroCloudDataAccess.Domain.StorePaymentProvider()
                    {
                        ClientId = acsQueryEntity.ClientId,
                        ClientPassword = acsQueryEntity.ClientPassword,
                        Id = acsQueryEntity.Id,
                        ProviderName = acsQueryEntity.ProviderName
                    };
                }
            }

            return "";
        }
    }
}
