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
    public class OrderHeader
    {
        public string Id { get; set;}
        public DateTime ForDateTime { get; set;}
        public int Status { get; set; }
    }
}
