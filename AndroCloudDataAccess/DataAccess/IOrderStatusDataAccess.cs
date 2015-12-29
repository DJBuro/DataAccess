using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IOrderStatusDataAccess
    {
        string GetByRamesesStatusId(int ramesesStatusId, out OrderStatus orderStatus);
    }
}
