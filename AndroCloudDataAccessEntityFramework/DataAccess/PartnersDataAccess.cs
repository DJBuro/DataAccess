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
        public string ConnectionStringOverride { get; set; }

        public string GetById(int id, out AndroCloudDataAccess.Domain.Partner partner)
        {
            partner = null;

            using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
            {
                var partnerQuery = from p in acsEntities.Partners
                                   where p.Id == id
                                   select p;

                var partnerEntity = partnerQuery.FirstOrDefault();

                if (partnerEntity != null)
                {
                    partner = new AndroCloudDataAccess.Domain.Partner();
                    partner.Id = partnerEntity.Id;
                    partner.Name = partnerEntity.Name;
                    partner.ExternalId = partnerEntity.ExternalId;
                }
            }

            return "";
        }
    }
}
