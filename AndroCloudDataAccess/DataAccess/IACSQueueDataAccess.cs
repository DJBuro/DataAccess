using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IACSQueueDataAccess
    {
        string GetById(Guid externalOrderId, out ACSQueue acsQueue);
    }
}
