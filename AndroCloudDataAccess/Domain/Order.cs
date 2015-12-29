using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    public class Order
    {
        [JsonProperty(PropertyName = "orderId")]
        public string OrderId { get; set;}

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}
