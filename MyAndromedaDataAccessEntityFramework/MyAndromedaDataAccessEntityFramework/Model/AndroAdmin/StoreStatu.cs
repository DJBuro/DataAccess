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
    
    public partial class StoreStatu
    {
        public StoreStatu()
        {
            this.Stores = new HashSet<Store>();
        }
    
        public int Id { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Store> Stores { get; set; }
    }
}
