﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IOrdersDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string GetById(Guid orderId, out Order order);
        string Update(Guid orderId, Guid orderStatusId);
        string GetByInternetOrderNumber(int internetOrderNumber, out Order order);
        string GetByExternalOrderNumber(string externalOrderId, out Order order);
        string GetByApplicationIdOrderId(int applicationId, Guid orderId, out Order order);
    }
}
