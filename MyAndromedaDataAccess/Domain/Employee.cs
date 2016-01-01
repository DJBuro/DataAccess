using System;
using System.Linq;

namespace MyAndromedaDataAccess.Domain
{
    public class Employee
    {
        public int Id { get; set; }

        public string Firstname { get; set;}
        
        public string Surname { get; set; }

        public string FullName { get; set; }

        public string Role { get; set; }

        public string NationalInsuranceNumber { get; set; }

        public string DrivingLicenceNumber { get; set; }

        public string Phone { get; set; }

        public string PayrollNumber { get; set; }
    }
}
