using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccess.Domain;
using AndroCloudDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class PartnersDataAccess : IPartnersDataAccess
    {
        public string Get(string externalPartnerId, out AndroCloudDataAccess.Domain.Partner partner)
        {
            partner = null;
            var acsEntities = new ACSEntities();

            var partnerQuery = from p in acsEntities.Partners
                               where p.ExternalId == externalPartnerId
                               select p;

            var partnerEntity = partnerQuery.FirstOrDefault();

            if (partnerEntity != null)
            {
                partner = new AndroCloudDataAccess.Domain.Partner();
                partner.Id = partnerEntity.ID;
                partner.Name = partnerEntity.Name;
                partner.ExternalId = partnerEntity.ExternalId;
            }

            return "";
        }
    }
}
