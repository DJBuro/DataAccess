using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IHostDataAccess
    {
        string GetAll(out List<Host> hosts);
    }
}
