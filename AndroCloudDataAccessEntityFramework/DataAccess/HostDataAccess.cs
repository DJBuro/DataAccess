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
        public string ConnectionStringOverride { get; set; }

        public string GetAllPublic(out List<AndroCloudDataAccess.Domain.Host> hosts)
        {
            hosts = new List<AndroCloudDataAccess.Domain.Host>();

            //using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from h in acsEntities.Hosts
                               select h;

                foreach (Host hostEntity in acsQuery)
                {
                    AndroCloudDataAccess.Domain.Host host = new AndroCloudDataAccess.Domain.Host();
                    host.Url = hostEntity.HostName;
                    host.Order = hostEntity.Order;

                    hosts.Add(host);
                }
            }

            return "";
        }

        public string GetAllPrivate(out List<AndroCloudDataAccess.Domain.Host> hosts)
        {
            hosts = new List<AndroCloudDataAccess.Domain.Host>();

            //using (ACSEntities acsEntities = ConnectionStringOverride == null ? new ACSEntities() : new ACSEntities(this.ConnectionStringOverride))
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from h in acsEntities.Hosts
                               select h;

                foreach (Host hostEntity in acsQuery)
                {
                    AndroCloudDataAccess.Domain.Host host = new AndroCloudDataAccess.Domain.Host();
                    host.Url = hostEntity.PrivateHostName;
                    host.Order = hostEntity.Order;

                    hosts.Add(host);
                }
            }

            return "";
        }
    }
}
