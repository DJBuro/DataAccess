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
    
    public partial class StoreDriver
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public string PartnerId { get; set; }
        public string Phone { get; set; }
    
        public virtual Store Store { get; set; }
    }
}
