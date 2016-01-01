using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IOrdersDataAccess
    {
        string GetById(Guid externalOrderId, out Order order);
        string Update(Guid orderId, Guid orderStatusId);
        string GetByInternetOrderNumber(int internetOrderNumber, out Order order);
    }
}
