//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataWarehouseDataAccessEntityFramework.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PasswordResetRequest
    {
        public int Id { get; set; }
        public System.DateTime RequestedDateTime { get; set; }
        public string Token { get; set; }
        public System.Guid CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
