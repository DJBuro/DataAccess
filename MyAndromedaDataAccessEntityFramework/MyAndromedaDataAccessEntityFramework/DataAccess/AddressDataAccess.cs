using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccessEntityFramework.Model;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccessEntityFramework.DataAccess
{
    public class AddressDataAccess : IAddressDataAccess
    {
        public string UpsertBySiteId(int siteId, MyAndromedaDataAccess.Domain.Address address)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var androAdminQuery = from s in androAdminEntities.Stores
                                  join a in androAdminEntities.Addresses
                                    on s.AddressId equals a.Id
                                  where s.Id == siteId
                                  select a;

            Model.Address androAdminEntity = androAdminQuery.FirstOrDefault();

            // Insert or update?
            if (androAdminEntity == null)
            {
                androAdminEntity = new Model.Address();
            }

            // Make the changes
            androAdminEntity.Country = address.Country;
            androAdminEntity.County = address.County;
            androAdminEntity.Locality = address.Locality;
            androAdminEntity.Org1 = address.Org1;
            androAdminEntity.Org2 = address.Org2;
            androAdminEntity.Org3 = address.Org3;
            androAdminEntity.PostCode = address.Postcode;
            androAdminEntity.Prem1 = address.Prem1;
            androAdminEntity.Prem2 = address.Prem2;
            androAdminEntity.Prem3 = address.Prem3;
            androAdminEntity.Prem4 = address.Prem4;
            androAdminEntity.Prem5 = address.Prem5;
            androAdminEntity.Prem6 = address.Prem6;
            androAdminEntity.RoadName = address.RoadName;
            androAdminEntity.RoadNum = address.RoadNum;
            androAdminEntity.State = address.State;
            androAdminEntity.Town = address.Town;
            androAdminEntity.DPS = address.Dps;

            // Add a new address
            if (androAdminEntity == null)
            {
                androAdminEntities.Addresses.Add(androAdminEntity);
            }

            androAdminEntities.SaveChanges();

            return "";
        }
    }
}
