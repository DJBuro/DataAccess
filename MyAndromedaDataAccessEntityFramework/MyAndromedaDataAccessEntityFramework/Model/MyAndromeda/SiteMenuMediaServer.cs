//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyAndromedaDataAccessEntityFramework.Model.MyAndromeda
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiteMenuMediaServer
    {
        public SiteMenuMediaServer()
        {
            this.SiteMenus = new HashSet<SiteMenu>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Port { get; set; }
    
        public virtual ICollection<SiteMenu> SiteMenus { get; set; }
    }
}
