//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroAdminDataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreMenu
    {
        public int Id { get; set; }
        public Nullable<int> StoreId { get; set; }
        public Nullable<int> Version { get; set; }
        public string MenuType { get; set; }
        public string MenuData { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public int DataVersion { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
