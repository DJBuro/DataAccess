using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IHostDataAccess
    {
        string GetAllPublic(out List<Host> hosts);
        string GetAllPrivate(out List<AndroCloudDataAccess.Domain.Host> hosts);
    }
}
