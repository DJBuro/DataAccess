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
    
    public partial class StoreEmployee
    {
        public int SiteId { get; set; }
        public string EmployeeId { get; set; }
        public int PersonId { get; set; }
        public string JobTitle { get; set; }
    
        public virtual Person Person { get; set; }
    }
}
