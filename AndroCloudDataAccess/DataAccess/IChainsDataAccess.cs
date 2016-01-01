using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IChainsDataAccess
    {
        string Get(Guid partnerId, string externalChainId, out Chain chain);
    }
}
