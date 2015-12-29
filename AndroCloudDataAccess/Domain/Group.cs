using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{
    public class Group
    {
        public Guid Id { get; set;}
        public Guid? PartnerID { get; set; }
        public string ChainName { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
