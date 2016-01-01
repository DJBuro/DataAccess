using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class StoreMenuThumbnails
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string MenuType { get; set; }
        public string Data { get; set; }
        public DateTime LastUpdate { get; set; }

        public int AndromediaSiteId { get; set; }
    }
}
