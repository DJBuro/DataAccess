using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class CustomerDAO
    {
        public System.Guid ID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> AddressId { get; set; }
        public int ACSAplicationId { get; set; }
        public System.DateTime RegisteredDateTime { get; set; }
        public Nullable<System.Guid> CustomerAccountId { get; set; }
    }
}
