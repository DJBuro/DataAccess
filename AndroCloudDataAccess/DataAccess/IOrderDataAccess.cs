using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IOrderDataAccess
    {
        string Get(Guid securityGuid, Guid orderGuid, out Order order);
    }
}
