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
    
    public partial class OpeningHour
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public System.TimeSpan TimeStart { get; set; }
        public System.TimeSpan TimeEnd { get; set; }
        public bool OpenAllDay { get; set; }
        public int DayId { get; set; }
        public int DataVersion { get; set; }
    
        public virtual Day Day { get; set; }
        public virtual Store Store { get; set; }
    }
}
