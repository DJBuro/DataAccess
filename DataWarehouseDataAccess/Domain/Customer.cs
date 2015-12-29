using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace DataWarehouseDataAccess.Domain
{
    public class Customer
    {
        [JsonIgnore]
        [XmlIgnore]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set;}

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set;}

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "contacts")]
        public List<Contact> Contacts { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }
    }
}
