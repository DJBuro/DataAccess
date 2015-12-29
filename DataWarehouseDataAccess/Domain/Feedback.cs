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
    public class Feedback
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid ID { get; set; }

        [JsonProperty(PropertyName = "name")]
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "feedbackCategory")]
        [XmlElement(ElementName = "FeedbackCategory")]
        public int? FeedbackCategory { get; set; }

        [JsonProperty(PropertyName = "feedback")]
        [XmlElement(ElementName = "Feedback")]
        public string FeedbackText { get; set; }
    }
}
