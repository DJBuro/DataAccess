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

        public string GetPublic(string externalApplicationId, out List<AndroCloudDataAccess.Domain.Host> applicationHostList)
        {
            applicationHostList = new List<AndroCloudDataAccess.Domain.Host>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = acsEntities.Hosts
                    .Where(e=> e.ACSApplications.Any(application => application.ExternalApplicationId == externalApplicationId))
                    .OrderBy(e=> e.Order)
                    .ToArray();

                foreach (Host hostEntity in acsQuery)
                {
                    AndroCloudDataAccess.Domain.Host host = new AndroCloudDataAccess.Domain.Host();
                    host.Url = hostEntity.HostName;
                    host.Order = hostEntity.Order;

                    applicationHostList.Add(host);
                }
            }

            return "";
        }

        public string GetAllPublic(out List<AndroCloudDataAccess.Domain.Host> hosts)
        {
            hosts = new List<AndroCloudDataAccess.Domain.Host>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from h in acsEntities.Hosts orderby h.Order
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

        public string GetAllPublicV2(out List<AndroCloudDataAccess.Domain.HostV2> hosts)
        {
            hosts = new List<AndroCloudDataAccess.Domain.HostV2>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from h in acsEntities.HostsV2
                               join ht in acsEntities.HostTypes
                                   on h.HostTypeId equals ht.Id
                               where h.Public == true
                               select h;

                foreach (HostsV2 hostEntity in acsQuery)
                {
                    AndroCloudDataAccess.Domain.HostV2 host = new AndroCloudDataAccess.Domain.HostV2()
                    {
                        Id = hostEntity.Id,
                        Order = hostEntity.Order,
                        Type = hostEntity.HostType.Name,
                        Url = hostEntity.Url,
                        Version = hostEntity.Version
                    };

                    hosts.Add(host);
                }
            }

            return "";
        }

        public void GetPublicV2(string externalApplicationId, out List<AndroCloudDataAccess.Domain.HostV2> applicationHostList)
        {
            applicationHostList = new List<AndroCloudDataAccess.Domain.HostV2>();
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = acsEntities.HostsV2
                    .Where(e => e.Public)
                    .Where(e=> e.ACSApplications.Any(application => application.ExternalApplicationId == externalApplicationId))
                    .Select(e=> new {
                        e.Id,
                        e.Order,
                        TypeName = e.HostType.Name,
                        e.Url,
                        e.Version
                    }).ToArray();

                foreach (var hostEntity in acsQuery)
                {
                    AndroCloudDataAccess.Domain.HostV2 host = new AndroCloudDataAccess.Domain.HostV2()
                    {
                        Id = hostEntity.Id,
                        Order = hostEntity.Order,
                        Type = hostEntity.TypeName,
                        Url = hostEntity.Url,
                        Version = hostEntity.Version
                    };

                    applicationHostList.Add(host);
                }
            }

            
        }

        public string GetAllPrivate(out List<AndroCloudDataAccess.Domain.PrivateHost> hosts)
        {
            hosts = new List<AndroCloudDataAccess.Domain.PrivateHost>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from h in acsEntities.Hosts
                               select h;

                foreach (Host hostEntity in acsQuery)
                {
                    AndroCloudDataAccess.Domain.PrivateHost host = new AndroCloudDataAccess.Domain.PrivateHost();
                    host.Url = hostEntity.PrivateHostName;
                    host.Order = hostEntity.Order;
                    host.SignalRUrl = hostEntity.SignalRHostName;

                    hosts.Add(host);
                }
            }

            return "";
        }

        public string GetAllPrivateV2(out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            hosts = new List<AndroCloudDataAccess.Domain.PrivateHostV2>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsQuery = from h in acsEntities.HostsV2
                               join ht in acsEntities.HostTypes
                                   on h.HostTypeId equals ht.Id
                               select h;

                foreach (HostsV2 hostEntity in acsQuery)
                {
                    AndroCloudDataAccess.Domain.PrivateHostV2 host = new AndroCloudDataAccess.Domain.PrivateHostV2()
                    {
                        Id = hostEntity.Id,
                        Order = hostEntity.Order,
                        Type = hostEntity.HostType.Name,
                        Url = hostEntity.Url,
                        Version = hostEntity.Version
                    };

                    hosts.Add(host);
                }
            }

            return "";
        }

        
    }
}
