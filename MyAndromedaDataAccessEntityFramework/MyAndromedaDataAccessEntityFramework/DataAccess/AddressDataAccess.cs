using System;
using System.Linq;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace MyAndromedaDataAccessEntityFramework.DataAccess
{
    public class AddressDataAccess : IAddressDataAccess
    {
        public string UpsertBySiteId(int siteId, MyAndromedaDataAccess.Domain.Address address)
        {
            using (var entitiesContext = new AndroAdminDbContext())
            {
                var query = from c in entitiesContext.Countries
                                   where c.Id == address.CountryId
                                   select c;

                var countryEntity = query.FirstOrDefault();

                var query2 = from s in entitiesContext.Stores
                                      join a in entitiesContext.Addresses
                                        on s.AddressId equals a.Id
                                      where s.Id == siteId
                                      select a;

                Model.AndroAdmin.Address addressEntity = query2.FirstOrDefault();

                // Insert or update?
                if (addressEntity == null)
                {
                    addressEntity = new Model.AndroAdmin.Address();
                }

                // Make the changes
                addressEntity.Country = countryEntity;
                addressEntity.County = address.County;
                addressEntity.Locality = address.Locality;
                addressEntity.Org1 = address.Org1;
                addressEntity.Org2 = address.Org2;
                addressEntity.Org3 = address.Org3;
                addressEntity.PostCode = address.Postcode;
                addressEntity.Prem1 = address.Prem1;
                addressEntity.Prem2 = address.Prem2;
                addressEntity.Prem3 = address.Prem3;
                addressEntity.Prem4 = address.Prem4;
                addressEntity.Prem5 = address.Prem5;
                addressEntity.Prem6 = address.Prem6;
                addressEntity.RoadName = address.RoadName;
                addressEntity.RoadNum = address.RoadNum;
                addressEntity.State = address.State;
                addressEntity.Town = address.Town;
                addressEntity.DPS = address.Dps;

                // Add a new address
                if (addressEntity == null)
                {
                    entitiesContext.Addresses.Add(addressEntity);
                }

                entitiesContext.SaveChanges();
            }

            return "";
        }
    }
}
