using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AndroCloudDataAccess.Domain
{
    [DataContract]
    public class Order
    {
        [DataMember(Name = "orderId")]
        public string OrderId { get; set;}

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}
