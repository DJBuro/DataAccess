//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAndromedaDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmailCampaignSite
    {
        public int EmailCampaignId { get; set; }
        public int SiteId { get; set; }
        public bool Editable { get; set; }
    
        public virtual EmailCampaign EmailCampaign { get; set; }
    }
}
