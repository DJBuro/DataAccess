using System;
using System.Linq;

namespace MyAndromedaDataAccess.Domain
{
    public class Employee
    {
        public string Key 
        {
            get { return string.Format("{0}.{1}", this.AndromedaSiteId, this.EmployeeId); }
        }

        public int AndromedaSiteId { get; set; }

        public string EmployeeId { get; set; }

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
