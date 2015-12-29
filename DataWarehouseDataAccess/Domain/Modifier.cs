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
    public class Modifier
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public bool? Removed { get; set; }
        public int? Price { get; set; }
    }
}
