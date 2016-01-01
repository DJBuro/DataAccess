using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccessEntityFramework.Domain
{
    public class CustomerWrapper
    {
        public DataWarehouseDataAccess.Domain.Customer Customer { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
