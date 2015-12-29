using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{
    public class Partner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ExternalId { get; set; }
    }
}
