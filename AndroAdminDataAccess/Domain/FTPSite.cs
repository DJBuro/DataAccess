﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AndroAdminDataAccess.Domain
{
    public class FTPSite
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Port { get; set; }
        public string ServerType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}