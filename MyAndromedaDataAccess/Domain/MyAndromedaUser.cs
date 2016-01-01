using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccess.Domain
{
    public class MyAndromedaUser
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Firstname { get; set; }
        public string Surname { get; set; }
        
        //not this easy any more
        //public List<Site> Sites { get; set; }
    }
}
