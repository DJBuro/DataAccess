//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAndromedaDataAccessEntityFramework.Model.AndroAdmin
{
    using System;
    using System.Collections.Generic;
    
    public partial class Partner
    {
        public Partner()
        {
            this.ACSApplications = new HashSet<ACSApplication>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExternalId { get; set; }
        public int DataVersion { get; set; }
    
        public virtual ICollection<ACSApplication> ACSApplications { get; set; }
    }
}
