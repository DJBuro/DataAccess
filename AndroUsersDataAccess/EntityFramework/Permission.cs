//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndroUsersDataAccess.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permission
    {
        public Permission()
        {
            this.SecurityGroupPermissions = new HashSet<SecurityGroupPermission>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
    
        public virtual ICollection<SecurityGroupPermission> SecurityGroupPermissions { get; set; }
    }
}
