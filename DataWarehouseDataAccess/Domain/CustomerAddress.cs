using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class CustomerAddress
    {
        public System.Guid ID { get; set; }
        public Nullable<System.Guid> CustomerKey { get; set; }
        public string RoadNum { get; set; }
        public string RoadName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
