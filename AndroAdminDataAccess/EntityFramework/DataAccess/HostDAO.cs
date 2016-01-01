﻿using System;
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
        public IList<Domain.Host> GetAll()
        {
            List<Domain.Host> models = new List<Domain.Host>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from p in entitiesContext.Hosts
                            select p;

                foreach (var entity in query)
                {
                    Domain.Host model = new Domain.Host()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
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