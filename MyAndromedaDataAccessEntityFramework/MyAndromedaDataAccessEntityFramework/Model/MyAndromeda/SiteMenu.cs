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
    
    public partial class SiteMenu
    {
        public SiteMenu()
        {
            this.MenuItems = new HashSet<MenuItem>();
        }
    
        public System.Guid Id { get; set; }
        public string AndromediaId { get; set; }
        public int DataVersion { get; set; }
        public System.DateTime LastUpdated { get; set; }
    
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}
