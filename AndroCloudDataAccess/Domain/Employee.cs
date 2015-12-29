using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccess.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Firstname { get; set;}
        public string Role { get; set;}
        public string Surname { get; set; }
    }
}
