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
    
    public partial class ACSApplicationSite
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int ACSApplicationId { get; set; }
        public int DataVersion { get; set; }
    
        public virtual ACSApplication ACSApplication { get; set; }
        public virtual Store Store { get; set; }
    }
}
