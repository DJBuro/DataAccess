using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{


    public class SiteLoyalty
    {
        public Guid Id { get; set; }

        public string ProviderName { get; set; }
        public string RawConfiguration { get; set; }

        public AndromedaLoyaltyConfiguration Configuration { get; set; }

        public bool Enabled 
        {
            get 
            {
                return Configuration.Enabled.GetValueOrDefault();

                //if (this.Configuration = null) 
                //{
                //    return false;
                //}
                //if (this.Configuration.Enabled != null) 
                //{
                //    return this.Configuration.Enabled;
                //}

                //return false;
            }
        }
    }

    public class AndromedaLoyaltyConfiguration 
    {
        
        //Enabled?: boolean;
        public bool? Enabled { get; set; }
        ////how much points equal to a £ ie £1 = 100 points
        //PointValue?: number;
        public decimal? PointValue { get; set; }
        ////how much points to receive per £ spent
        //AwardPointsPerPoundSpent?: number;
        public int? AwardPointsPerPoundSpent { get; set; }
        ////how much points to be awarded on registration
        //AwardOnRegiration?: number;
        public int? AwardOnRegiration { get; set; }
        ////when x points are reached they must spend some to be under x - ie they cannot horde points 
        //AutoSpendPointsOverThisPeak?: number;
        public int? AutoSpendPointsOverThisPeak { get; set; }
        ////the minimum amount needed left in the basket after the loyalty has been applied. 
        //MinimumOrderTotalAfterLoyalty?: number;
        public decimal? MinimumOrderTotalAfterLoyalty { get; set; }
        ////how much points need to be claimed before they are accessible to the user
        //MinimumPointsBeforeAvailable?: number;
        public int? MinimumPointsBeforeAvailable { get; set; }
        ////how much of the order can be claimed with points
        //MaximumPercentThatCanBeClaimed?: number;
        public decimal? MaximumPercentThatCanBeClaimed { get; set; }
        ////how highest monetary value that can be spent by points. 
        //MaximumValueThatCanBeClaimed?: number;
        public decimal? MaximumValueThatCanBeClaimed { get; set; }
        ////round the points up or down to the nearest whole number 
        //RoundUp : boolean
        public bool? RoundUp { get; set; }
    }

    //public class SiteLoyalty
    //{
    //    public System.Guid Id { get; set; }
    //    public System.Guid SiteId { get; set; }
    //    public string Configuration { get; set; }
    //    public string ProviderName { get; set; }
    //    public LoyaltyConfiguration ConfigurationTypes { get; set; }
    //}

    //public class LoyaltyConfiguration
    //{
    //    public bool isEnabled { get; set; }
    //    public double PointsForEachpoundSpent { get; set; }
    //    public double MinimumTotalPointsToRedeem { get; set; }
    //    public double PointsReferringToEachPound { get; set; }
    //    public double PointsForNewCustomer { get; set; }
    //    public double MinimumPointsToRedeemAtATime { get; set; }
    //    public double MaximumPercentageToRedeem { get; set; }
    //}
}
