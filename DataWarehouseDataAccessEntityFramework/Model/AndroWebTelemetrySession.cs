//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataWarehouseDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AndroWebTelemetrySession
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AndroWebTelemetrySession()
        {
            this.AndroWebTelemetries = new HashSet<AndroWebTelemetry>();
        }
    
        public System.Guid Id { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
        public Nullable<System.Guid> CustomerId { get; set; }
        public string Referrer { get; set; }
        public string BrowserDetails { get; set; }
        public string ExternalSiteId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AndroWebTelemetry> AndroWebTelemetries { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
