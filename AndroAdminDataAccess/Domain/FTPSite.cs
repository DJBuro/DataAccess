using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class FTPSite
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage="Please enter a name for the FTP site")]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "Please enter a url for the FTP site")]
        public virtual string Url { get; set; }

        [Required(ErrorMessage = "Please enter a port for the FTP site")]
        public virtual int Port { get; set; }

        public virtual FTPSiteType FTPSiteType { get; set; }

        [Required(ErrorMessage = "Please enter a username to access the FTP site")]
        public virtual string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password to access the FTP site")]
        public virtual string Password { get; set; }
    }
}