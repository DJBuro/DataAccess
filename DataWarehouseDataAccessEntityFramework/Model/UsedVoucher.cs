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
    
    public partial class UsedVoucher
    {
        public System.Guid VoucherId { get; set; }
        public System.Guid CustomerId { get; set; }
        public System.Guid OrderId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual OrderHeader OrderHeader { get; set; }
        public virtual Voucher Voucher { get; set; }
    }
}
