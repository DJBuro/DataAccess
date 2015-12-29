using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain;
using System.Data.Objects;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
namespace MyAndromedaDataAccessEntityFramework.DataAccess.Sites
{
    public interface ISiteAddressDataService : IDependency 
    {
        Address GetSiteAddress(int storeId);
    }

    public class SiteAddressDataService : ISiteAddressDataService 
    {
        public SiteAddressDataService() 
        {
            
        }

        public Address GetSiteAddress(int storeId) 
        {
            Address address; 
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Addresses;
                var query = table
                    .Include(e=> e.Country)
                    .Where(e => e.Stores.Any(store => store.Id == storeId));

                var result = query.Single();

                address = new Address()
                {
                    Country = new Country(){
                        CountryName = result.Country.CountryName,
                        Id = result.Country.Id,
                        ISO3166_1_alpha_2 = result.Country.ISO3166_1_alpha_2,
                        ISO3166_1_numeric = result.Country.ISO3166_1_numeric
                    },
                    County = result.County,
                    Dps = result.DPS,
                    Lat = result.Lat,
                    Locality = result.Locality,
                    Long = result.Long,
                    Org1 = result.Org1,
                    Org2 = result.Org2,
                    Org3 = result.Org3,
                    Postcode = result.PostCode,
                    Prem1 = result.Prem1,
                    Prem2 = result.Prem2,
                    Prem3 = result.Prem3,
                    Prem4 = result.Prem4,
                    Prem5 = result.Prem5,
                    Prem6 = result.Prem6,
                    RoadName = result.RoadName,
                    RoadNum = result.RoadNum,
                    State = result.State,
                    Town = result.Town
                };
            }

            return address;
        }
        
    }
}