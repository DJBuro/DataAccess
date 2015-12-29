using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AndroAdminDataAccess.Domain
{
    public class AndroWebOrderingSubscriptionType
    {
        public int Id { set; get; }
        public string Subscription { set; get; }
        public string Description { set; get; }
        public int DisplayOrder { set; get; }
    }
     
    public class AndroWebOrderingWebsite
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int? ChainId { set; get; }
        public bool Enabled { set; get; }
        public string Status { set; get; }
        public string DisabledReason { set; get; }
        public int SubscriptionTypeId { set; get; }
        public string SubscriptionName { set; get; }
        public string LiveDomainName { set; get; } // to be changed to LiveDomainName
        public int ACSApplicationId { set; get; }
        public int DataVersion { set; get; }
        public ACSApplication ACSApplication { set; get; }
        public IList<int> MappedSiteIds { set; get; }
        public IList<Store> AllStores { set; get; }
        public IList<Chain> Chains { set; get; }
        public string StoresCount { set; get; }
        public IList<AndroWebOrderingSubscriptionType> SubscriptionsList { set; get; }
        public string UpdatedMappedStoreIds { set; get; }

        public string Settings { get; set; }
        public string PreviewSettings { get; set; }
        public string PreviewDomainName { get; set; }
        public int? ThemeId { get; set; }
    }
}
