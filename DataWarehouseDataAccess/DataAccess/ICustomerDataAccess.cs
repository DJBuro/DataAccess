﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface ICustomerDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string GetByUsernamePassword(string username, string password, int applicationId, out DataWarehouseDataAccess.Domain.Customer customer);
        string AddCustomer(string username, string password, int applicationId, DataWarehouseDataAccess.Domain.Customer customer);
        string UpdateCustomer(string username, string password, int applicationId, DataWarehouseDataAccess.Domain.Customer customer);
    }
}
