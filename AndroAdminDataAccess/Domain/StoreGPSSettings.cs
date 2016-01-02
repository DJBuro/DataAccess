using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class StoreGPSSettings
    {
        public virtual int Id { get; set; }
        public virtual int MaxDrivers { get; set; }
        public virtual string PartnerConfig { get; set; }
    }
}