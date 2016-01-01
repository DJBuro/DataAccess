using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IPartnerDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<Partner> GetAll();
        Partner GetById(int partnerId);
        Partner GetByName(string name);
        Partner GetByExternalId(string externalId);
        void Add(Partner partner);
        void Update(Partner partner);
        void UpdateDataVersion(int partnerId, int newVersion);
        IList<Domain.Partner> GetAfterDataVersion(int dataVersion);
    }
}