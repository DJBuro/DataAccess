using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{
    public class MyAndromedaUser
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public List<Site> Sites { get; set; }
    }
}
