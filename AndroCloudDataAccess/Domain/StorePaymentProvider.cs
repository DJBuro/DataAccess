using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    public class StorePaymentProvider
    {
        public int Id { get; set; }
        public string ProviderName { get; set; }
        public string ClientId { get; set; }
        public string ClientPassword { get; set; }
    }
}
