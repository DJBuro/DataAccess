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
    
    public partial class OrderStatu
    {
        public OrderStatu()
        {
            this.OrderHeaders = new HashSet<OrderHeader>();
            this.OrderStatusHistories = new HashSet<OrderStatusHistory>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<OrderHeader> OrderHeaders { get; set; }
        public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; }
    }
}
