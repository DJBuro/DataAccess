using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class OrderLineDAO
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> OrderHeaderID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string Description { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> Price { get; set; }
    }
}
