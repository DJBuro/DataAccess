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
    
    public partial class EmailCampaignSetting
    {
        public EmailCampaignSetting()
        {
            this.EmailCampaignTasks = new HashSet<EmailCampaignTask>();
        }
    
        public int Id { get; set; }
        public string Host { get; set; }
        public Nullable<int> Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FromEmail { get; set; }
        public bool SSL { get; set; }
        public Nullable<int> ChainId { get; set; }
        public Nullable<int> SiteId { get; set; }
        public string DropFolder { get; set; }
    
        public virtual ICollection<EmailCampaignTask> EmailCampaignTasks { get; set; }
    }
}
