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
    public class Address
    {
        [JsonProperty(PropertyName = "long")]
        public string Long { get; set;}

        [JsonProperty(PropertyName = "lat")]
        public string Lat { get; set;}

        [StringLength(64, ErrorMessage = "Prem1 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "prem1")]
        public string Prem1 { get; set;}

        [StringLength(64, ErrorMessage = "Prem2 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "prem2")]
        public string Prem2 { get; set;}

        [StringLength(64, ErrorMessage = "Prem3 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "prem3")]
        public string Prem3 { get; set; }

        [StringLength(64, ErrorMessage = "Prem4 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "prem4")]
        public string Prem4 { get; set; }

        [StringLength(64, ErrorMessage = "Prem5 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "prem5")]
        public string Prem5 { get; set; }

        [StringLength(64, ErrorMessage = "Prem6 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "prem6")]
        public string Prem6 { get; set; }

        [StringLength(64, ErrorMessage = "Org1 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "org1")]
        public string Org1 { get; set;}

        [StringLength(64, ErrorMessage = "Org2 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "org2")]
        public string Org2 { get; set;}

        [StringLength(64, ErrorMessage = "Org3 cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "org3")]
        public string Org3 { get; set; }

        [StringLength(16, ErrorMessage = "RoadNum cannot be longer than 16 characters.")]
        [JsonProperty(PropertyName = "roadNum")]
        public string RoadNum { get; set;}

        [StringLength(64, ErrorMessage = "RoadName cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "roadName")]
        public string RoadName { get; set;}

        [StringLength(64, ErrorMessage = "Town cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "town")]
        public string Town { get; set;}

        [StringLength(16, ErrorMessage = "Postcode cannot be longer than 16 characters.")]
        [JsonProperty(PropertyName = "postcode")]
        public string Postcode { get; set;}

        [StringLength(4, ErrorMessage = "Dps cannot be longer than 4 characters.")]
        [JsonProperty(PropertyName = "dps")]
        public string Dps { get; set;}

        [StringLength(64, ErrorMessage = "County cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "county")]
        public string County { get; set;}

        [StringLength(64, ErrorMessage = "State cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "state")]
        public string State { get; set; }

        [StringLength(64, ErrorMessage = "Locality cannot be longer than 64 characters.")]
        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set;}

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}
