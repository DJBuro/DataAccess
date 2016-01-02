using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class StoreBringgSettings
    {
        public virtual int Id { get; set; }
        public virtual bool IsBringgEnabled { get; set; }
        public virtual int MaxDrivers { get; set; }
        public virtual int BringCustomerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual double? Lat { get; set; }
        public virtual double? Lon { get; set; }
        public virtual string Phone { get; set; }
    }
}