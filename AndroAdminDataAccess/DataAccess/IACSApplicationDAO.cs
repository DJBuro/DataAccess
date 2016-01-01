using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IACSApplicationDAO
    {
        string ConnectionStringOverride { get; set; }

        ACSApplication GetById(int acsApplicationId);
        ACSApplication GetByName(string name);
        ACSApplication GetByExternalId(string externalId);


        IList<ACSApplication> GetByStoreId(int storeId);
        IList<ACSApplication> GetByPartnerId(int partnerId);
        
        void Add(Domain.ACSApplication acsApplication);
        void Update(ACSApplication acsApplication);
        void AddStore(int storeId, int acsApplicatiopnId);
        void RemoveStore(int storeId, int acsApplicatiopnId);
        
        IList<Domain.ACSApplication> GetByPartnerAfterDataVersion(int partnerId, int dataVersion);
    }
}