using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccess.Domain
{
    public class Group
    {
        public int Id { get; set;}
        public int? PartnerId { get; set; }
        public string ChainName { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
