//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroCloudDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Day
    {
        public Day()
        {
            this.OpeningHours = new HashSet<OpeningHour>();
        }
    
        public System.Guid ID { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<OpeningHour> OpeningHours { get; set; }
    }
}
