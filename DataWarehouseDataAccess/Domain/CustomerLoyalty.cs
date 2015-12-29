using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class CustomerLoyalty
    {
        public System.Guid Id { get; set; }
        public System.Guid CustomerId { get; set; }
        public string ProviderName { get; set; }
        public Nullable<decimal> Points { get; set; }
    }
}
