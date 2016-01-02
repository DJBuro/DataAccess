using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface ICustomerGPSDataAccess
    {
        string ConnectionStringOverride { get; set; }

        string GetById(Guid customerId, out CustomerGPS customerGPS);
        string Add(DataWarehouseDataAccess.Domain.CustomerGPS newCustomerGPS);
        string Delete(DataWarehouseDataAccess.Domain.CustomerGPS deleteCustomerGPS);
    }
}
