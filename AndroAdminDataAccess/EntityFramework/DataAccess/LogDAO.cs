﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class LogDAO : ILogDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IEnumerable<Domain.Log> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Domain.Log log)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                Log entity = new Log()
                {
                    Created = log.Created,
                    Message = log.Message,
                    Method = log.Method,
                    Severity = log.Severity,
                    Source = log.Source,
                    StoreId = log.StoreId
                };

                entitiesContext.Logs.Add(entity);
                entitiesContext.SaveChanges();
            }
        }
    }
}
