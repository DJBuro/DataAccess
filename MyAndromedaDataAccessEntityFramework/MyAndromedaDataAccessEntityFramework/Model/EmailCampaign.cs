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
    
    public partial class EmailCampaign
    {
        public EmailCampaign()
        {
            this.EmailCampaignSentEmails = new HashSet<EmailCampaignSentEmail>();
            this.EmailCampaignSites = new HashSet<EmailCampaignSite>();
            this.EmailCampaignTasks = new HashSet<EmailCampaignTask>();
        }
    
        public int Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string EmailTemplate { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Modified { get; set; }
        public bool Removed { get; set; }
        public int ChainId { get; set; }
        public bool ChainOnly { get; set; }
    
        public virtual EmailCampaignAudit EmailCampaignAudit { get; set; }
        public virtual ICollection<EmailCampaignSentEmail> EmailCampaignSentEmails { get; set; }
        public virtual ICollection<EmailCampaignSite> EmailCampaignSites { get; set; }
        public virtual ICollection<EmailCampaignTask> EmailCampaignTasks { get; set; }
    }
}
