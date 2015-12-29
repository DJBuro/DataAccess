using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IPartnerDAO
    {
        IEnumerable<Partner> GetAll();
        Partner GetById(int partnerId);
        Partner GetByName(string name);
        Partner GetByExternalId(string externalId);
        void Add(Partner partner);
        void Update(Partner partner);
        IList<Domain.Partner> GetAfterDataVersion(int dataVersion);
    }
}