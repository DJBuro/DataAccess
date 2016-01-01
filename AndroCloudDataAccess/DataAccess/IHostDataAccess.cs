using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IHostDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string GetAllPublic(out List<Host> hosts);
        string GetAllPrivate(out List<AndroCloudDataAccess.Domain.PrivateHost> hosts);
    }
}
