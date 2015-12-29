using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using Newtonsoft.Json;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class StoreLoyaltyDataAccess : ISiteLoyaltyDataAccess
    {
        public string ConnectionStringOverride { get; set; }



        public string GetAllByExternalApplicationId(string externalApplicationId, out IEnumerable<AndroCloudDataAccess.Domain.SiteLoyalty> configurations)
        {
            configurations = Enumerable.Empty<AndroCloudDataAccess.Domain.SiteLoyalty>();

            try
            {
                using (ACSEntities acsEntities = new ACSEntities())
                {
                    DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                    var table = acsEntities.SiteLoyalties;

                    var query = table
                        .Where(e => e.Site.ACSApplicationSites.Any(application => application.ACSApplication.ExternalApplicationId == externalApplicationId))
                        //.Where(e=> e.ProviderName == "andromeda")
                        .Select(e => new { e.Id, e.ProviderName, e.Configuration });

                    var result = query.ToArray();
                    configurations = result.Select(e => new AndroCloudDataAccess.Domain.SiteLoyalty
                    {
                        Id = e.Id,
                        ProviderName = e.ProviderName,
                        Configuration = JsonConvert.DeserializeObject<AndromedaLoyaltyConfiguration>(string.IsNullOrWhiteSpace(e.Configuration) ? "{}" : e.Configuration)
                    }).ToArray();
                }
            }
            catch (Exception)
            {
                return "Failed to create loyalty provider list for external acs application id: " + externalApplicationId;
            }


            return string.Empty;
        }
    }

    
}
