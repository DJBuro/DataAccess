using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace AndroCloudDataAccess.Domain
{
    public class TimeSpanBlock
    {
        [JsonIgnore]
        [XmlIgnore]
        public Guid ID { get; set; }

        [JsonProperty(PropertyName = "day")]
        public string Day { get; set; }

        [JsonProperty(PropertyName = "startTime")]
        public string StartTime { get; set; }

        [JsonProperty(PropertyName = "endTime")]
        public string EndTime { get; set; }

        [JsonProperty(PropertyName = "openAllDay")]
        public bool OpenAllDay { get; set; }
    }
}
