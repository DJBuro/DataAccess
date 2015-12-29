using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class AddressDAO : IAddressDAO
    {
        public string ConnectionStringOverride { get; set; }

        public int Add(Domain.Address address)
        {
            int addressId = -1;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                // Get the country
                var query = from s in entitiesContext.Countries
                            where s.Id == address.Country.Id
                            select s;

                var countryEntity = query.FirstOrDefault();

                // Create the address
                Address entityAddress = new Address()
                {
                    Org1 = address.Org1,
                    Org2  = address.Org2,
                    Org3 = address.Org3,
                    Prem1 = address.Prem1,
                    Prem2 = address.Prem2,
                    Prem3 = address.Prem3,
                    Prem4 = address.Prem4,
                    Prem5 = address.Prem5,
                    Prem6 = address.Prem6,
                    RoadNum = address.RoadNum,
                    RoadName = address.RoadName,
                    Locality = address.Locality,
                    Town = address.Town,
                    County = address.County,
                    State = address.State,
                    PostCode = address.PostCode,
                    DPS = address.DPS,
                    Lat = address.Lat,
                    Long = address.Long,
                    Country = countryEntity
                };

                // Add the address
                entitiesContext.Addresses.Add(entityAddress);

                // Commit the change
                entitiesContext.SaveChanges();

                addressId = entityAddress.Id;
            }

            return addressId;
        }
    }
}
