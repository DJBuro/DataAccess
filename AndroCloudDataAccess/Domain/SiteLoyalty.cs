using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{
    public class SiteLoyalty
    {
        public System.Guid Id { get; set; }
        public System.Guid SiteId { get; set; }
        public string Configuration { get; set; }
        public string ProviderName { get; set; }
        public LoyaltyConfiguration ConfigurationTypes { get; set; }
    }

    public class LoyaltyConfiguration
    {
        public bool isEnabled { get; set; }
        public double PointsForEachpoundSpent { get; set; }
        public double MinimumTotalPointsToRedeem { get; set; }
        public double PointsReferringToEachPound { get; set; }
        public double PointsForNewCustomer { get; set; }
        public double MinimumPointsToRedeemAtATime { get; set; }
        public double MaximumPercentageToRedeem { get; set; }
    }
}
