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
    
    public partial class OrderLine
    {
        public OrderLine()
        {
            this.modifiers = new HashSet<modifier>();
            this.modifiers1 = new HashSet<modifier>();
        }
    
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> OrderHeaderID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> Price { get; set; }
    
        public virtual ICollection<modifier> modifiers { get; set; }
        public virtual ICollection<modifier> modifiers1 { get; set; }
        public virtual OrderHeader OrderHeader { get; set; }
        public virtual OrderHeader OrderHeader1 { get; set; }
    }
}
