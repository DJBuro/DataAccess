using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndroAdminDataAccess.Domain
{
    public class FTPSite
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
        public virtual int Port { get; set; }
        public virtual FTPSiteType FTPSiteType { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
    }
}