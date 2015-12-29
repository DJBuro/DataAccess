using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    public class Address
    {
        [JsonProperty(PropertyName = "long")]
        public string Long { get; set;}

        [JsonProperty(PropertyName = "lat")]
        public string Lat { get; set;}

        [JsonProperty(PropertyName = "prem1")]
        public string Prem1 { get; set;}

        [JsonProperty(PropertyName = "prem2")]
        public string Prem2 { get; set;}

        [JsonProperty(PropertyName = "prem3")]
        public string Prem3 { get; set; }

        [JsonProperty(PropertyName = "prem4")]
        public string Prem4 { get; set; }

        [JsonProperty(PropertyName = "prem5")]
        public string Prem5 { get; set; }

        [JsonProperty(PropertyName = "prem6")]
        public string Prem6 { get; set; }

        [JsonProperty(PropertyName = "org1")]
        public string Org1 { get; set;}

        [JsonProperty(PropertyName = "org2")]
        public string Org2 { get; set;}

        [JsonProperty(PropertyName = "org3")]
        public string Org3 { get; set; }

        [JsonProperty(PropertyName = "roadNum")]
        public string RoadNum { get; set;}

        [JsonProperty(PropertyName = "roadName")]
        public string RoadName { get; set;}

        [JsonProperty(PropertyName = "town")]
        public string Town { get; set;}

        [JsonProperty(PropertyName = "postcode")]
        public string Postcode { get; set;}

        [JsonProperty(PropertyName = "dps")]
        public string Dps { get; set;}

        [JsonProperty(PropertyName = "county")]
        public string County { get; set;}

        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set;}

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}
