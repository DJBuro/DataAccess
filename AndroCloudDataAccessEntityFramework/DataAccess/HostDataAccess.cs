using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class HostDataAccess : IHostDataAccess
    {
        public string GetAll(out List<AndroCloudDataAccess.Domain.Host> hosts)
        {
            hosts = new List<AndroCloudDataAccess.Domain.Host>();
            var acsEntities = new ACSEntities();

            var acsQuery = from h in acsEntities.Hosts
                           select h;

            foreach (Host hostEntity in acsQuery)
            {
                AndroCloudDataAccess.Domain.Host host = new AndroCloudDataAccess.Domain.Host();
                host.HostName = hostEntity.HostName;
                host.Order = hostEntity.Order;
                host.Port = hostEntity.Port;

                hosts.Add(host);
            }

            return "";
        }
    }
}
