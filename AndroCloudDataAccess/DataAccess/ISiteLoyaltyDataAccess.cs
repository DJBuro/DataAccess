using System.Collections.Generic;

namespace AndroCloudDataAccess.DataAccess
{
    public interface ISiteLoyaltyDataAccess 
    {
        string ConnectionStringOverride { get; set; }

        string GetAllByExternalApplicationId(string externalApplicationId, string externalSiteId, out IEnumerable<AndroCloudDataAccess.Domain.SiteLoyalty> configurations);
    }
}