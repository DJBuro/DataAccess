//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroAdminDataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class AndroWebOrderingWebsite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ChainId { get; set; }
        public bool Enabled { get; set; }
        public string DisabledReason { get; set; }
        public int SubscriptionTypeId { get; set; }
        public string URL { get; set; }
        public int ACSApplicationId { get; set; }
        public int DataVersion { get; set; }
        public string Settings { get; set; }
        public string PreviewDomainName { get; set; }
        public string PreviewSettings { get; set; }
        public Nullable<int> ThemeId { get; set; }
    
        public virtual ACSApplication ACSApplication { get; set; }
        public virtual AndroWebOrderingSubscriptionType AndroWebOrderingSubscriptionType { get; set; }
        public virtual Chain Chain { get; set; }
        public virtual AndroWebOrderingTheme AndroWebOrderingTheme { get; set; }
    }
}
