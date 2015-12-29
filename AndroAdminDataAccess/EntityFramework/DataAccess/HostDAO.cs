using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Data.Common;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class HostDAO : IHostDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.Host> GetAll()
        {
            List<Domain.Host> models = new List<Domain.Host>();

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from p in entitiesContext.Hosts
                            select p;

                foreach (var entity in query)
                {
                    Domain.Host model = new Domain.Host()
                    {
                        Id = entity.ID,
                        HostName = entity.HostName,
                        Order = entity.Order,
                        PrivateHostName = entity.PrivateHostName
                    };

                    models.Add(model);
                }
            }

            return models;
        }
    }
}