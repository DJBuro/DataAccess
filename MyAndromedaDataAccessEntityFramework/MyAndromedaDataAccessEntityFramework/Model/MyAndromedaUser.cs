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
    
    public partial class MyAndromedaUser
    {
        public MyAndromedaUser()
        {
            this.MyAndromedaUserGroups = new HashSet<MyAndromedaUserGroup>();
            this.MyAndromedaUserStores = new HashSet<MyAndromedaUserStore>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsEnabled { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    
        public virtual ICollection<MyAndromedaUserGroup> MyAndromedaUserGroups { get; set; }
        public virtual ICollection<MyAndromedaUserStore> MyAndromedaUserStores { get; set; }
    }
}
