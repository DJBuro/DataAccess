using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStorePaymentProviderDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<Domain.StorePaymentProvider> GetAll();
        void Add(Domain.StorePaymentProvider store);
        void Update(Domain.StorePaymentProvider store);
        Domain.StorePaymentProvider GetById(int id);
        IList<Domain.StorePaymentProvider> GetAfterDataVersion(int dataVersion);
    }
}