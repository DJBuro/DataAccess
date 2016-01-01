using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace AndroCloudDataAccess.Domain
{
    public class OrderStatus
    {
        public Guid Id { get; set;}
        public int RamesesStatusId { get; set; }
        public string Description { get; set; }
    }
}
