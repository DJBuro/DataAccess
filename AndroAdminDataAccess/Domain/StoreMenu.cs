using System;

namespace AndroAdminDataAccess.Domain
{
    public class StoreMenu 
    {
        public int Id { get; set; }

        public int AndromedaSiteId { get; set; }

        public int Version { get; set; }

        public string MenuType { get; set; }

        public string MenuData { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}