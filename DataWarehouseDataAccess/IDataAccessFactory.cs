﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.DataAccess;

namespace DataWarehouseDataAccess
{
    public interface IDataAccessFactory
    {
        ICustomerDataAccess CustomerDataAccess { get; set; }
    }
}
