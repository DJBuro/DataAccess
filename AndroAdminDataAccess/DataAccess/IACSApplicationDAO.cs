using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IACSApplicationDAO
    {
        ACSApplication GetById(int acsApplicationId);
        IList<ACSApplication> GetByPartnerId(int partnerId);
        ACSApplication GetByName(string name);
        ACSApplication GetByExternalId(string externalId);
        void Add(Domain.ACSApplication acsApplication);
        void Update(ACSApplication acsApplication);
        void UpdateDataVersion(int acsApplicationId, int newVersion);
        void AddStore(int storeId, int acsApplicatiopnId);
        void RemoveStore(int storeId, int acsApplicatiopnId);
        IList<Domain.ACSApplication> GetByPartnerAfterDataVersion(int partnerId, int dataVersion);
    }
}