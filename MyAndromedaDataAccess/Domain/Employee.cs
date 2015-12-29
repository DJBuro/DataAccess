using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccess.Domain
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set;}
        public string Role { get; set;}
        public string Surname { get; set; }
        public string Phone { get; set; }
    }
}
