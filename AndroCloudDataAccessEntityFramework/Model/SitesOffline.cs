//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroCloudDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SitesOffline
    {
        public System.Guid ID { get; set; }
        public System.Guid SiteID { get; set; }
        public System.DateTime EmailLastSent { get; set; }
        public System.DateTime EmailNextSent { get; set; }
        public int EmailSentTimes { get; set; }
        public bool SendEmailNow { get; set; }
    }
}
