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
    public class OrderDetails
    {
        public Guid? Id { get; set; }
        public string ExternalOrderRef { get; set; }
        public DateTime ForDateTime { get; set; }
        public int OrderStatus { get; set; }
        public decimal OrderTotal { get; set; }
        public decimal DeliveryCharge { get; set; }

        public List<OrderLine> OrderLines { get; set; }
        public List<OrderLine> Deals { get; set; }
        public List<OrderDiscount> Discounts { get; set; }
    }
}
