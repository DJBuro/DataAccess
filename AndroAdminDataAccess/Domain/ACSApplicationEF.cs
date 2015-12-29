using AndroAdminDataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class ACSApplicationEF
    {
        public int Id { get; set; }
        public string ExternalApplicationId { get; set; }
        public string Name { get; set; }
        public string ExternalDisplayName { get; set; }
        public int PartnerId { get; set; }
        public int DataVersion { get; set; }

        public virtual Partner Partner { get; set; }
        public virtual ICollection<ACSApplicationSite> ACSApplicationSites { get; set; }
        public virtual ICollection<HostV2> HostV2 { get; set; }
        public virtual ICollection<Host> Hosts { get; set; }
    }
}
