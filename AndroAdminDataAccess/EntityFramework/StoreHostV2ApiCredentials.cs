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
    
    public partial class StoreHostV2ApiCredentials
    {
        public int StoreId { get; set; }
        public System.Guid HostV2Id { get; set; }
        public System.Guid ApiCredentialsId { get; set; }
        public string Parameters { get; set; }
    
        public virtual HostV2 HostV2 { get; set; }
        public virtual Store Store { get; set; }
    }
}
