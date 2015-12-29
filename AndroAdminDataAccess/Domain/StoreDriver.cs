using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class StoreDriver
    {
        public virtual int Id { get; set; }     
        public virtual int StoreId { get; set; }
        public virtual string Name { get; set; }
        public virtual string PartnerId { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
    }
}