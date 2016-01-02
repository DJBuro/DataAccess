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
    public class OrderDiscount
    {
        public string Type { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; }
    }
}
