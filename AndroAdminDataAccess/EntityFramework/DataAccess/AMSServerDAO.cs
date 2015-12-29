using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class AMSServerDAO : IAMSServerDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.AMSServer> GetAll()
        {
            List<Domain.AMSServer> amsServers = new List<Domain.AMSServer>();

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.AMSServers
                            select s;

                foreach (var entity in query)
                {
                    Domain.AMSServer model = new Domain.AMSServer()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description
                    };

                    amsServers.Add(model);
                }
            }

            return amsServers;
        }

        public void Add(Domain.AMSServer amsServer)
        {
            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                AMSServer entity = new AMSServer()
                {
                    Name = amsServer.Name,
                    Description = amsServer.Description
                };

                entitiesContext.AddToAMSServers(entity);
                entitiesContext.SaveChanges();
            }
        }

        public void Update(Domain.AMSServer amsServer)
        {
            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.AMSServers
                            where amsServer.Id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = amsServer.Name;
                    entity.Description = amsServer.Description;

                    entitiesContext.SaveChanges();
                }
            }
        }

        public Domain.AMSServer GetById(int id)
        {
            Domain.AMSServer model = null;

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.AMSServers
                            where id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.AMSServer()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description
                    };
                }
            }

            return model;
        }

        public Domain.AMSServer GetByName(string name)
        {
            Domain.AMSServer model = null;

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.AMSServers
                            where name == s.Name
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.AMSServer()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description
                    };
                }
            }

            return model;
        }


        public void Delete(int amsServerId)
        {
            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                // Delete the StoreAMSServerFTPSite objects that reference the AMS server
                var storeAMSServerFTPSiteQuery = from s in entitiesContext.StoreAMSServerFtpSites
                                                 where s.StoreAMSServer.AMSServerId == amsServerId
                                                 select s;

                foreach (var entity in storeAMSServerFTPSiteQuery)
                {
                    entitiesContext.StoreAMSServerFtpSites.DeleteObject(entity);
                }

                // Delete the StoreAMSServer objects that reference the AMS server
                var storeAMSServerQuery = from s in entitiesContext.StoreAMSServers
                                          where s.AMSServerId == amsServerId
                                          select s;

                foreach (var entity in storeAMSServerQuery)
                {
                    entitiesContext.StoreAMSServers.DeleteObject(entity);
                }

                // Delete the AMS Server
                var amsServerQuery = from s in entitiesContext.AMSServers
                                     where amsServerId == s.Id
                                     select s;

                AMSServer amsServerEntity = amsServerQuery.FirstOrDefault();

                if (amsServerEntity != null)
                {
                    entitiesContext.AMSServers.DeleteObject(amsServerEntity);

                    entitiesContext.SaveChanges();
                }
            }
        }
    }
}
