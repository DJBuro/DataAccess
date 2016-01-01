using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AndroCloudDataAccess.Domain
{
    public class Contact
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set;}

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set;}

        [JsonProperty(PropertyName = "marketingLevel")]
        public string MarketingLevel { get; set; }
    }
}
