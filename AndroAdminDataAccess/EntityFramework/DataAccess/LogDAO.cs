using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class LogDAO : ILogDAO
    {
        public IEnumerable<Domain.Log> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Domain.Log log)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            Log entity = new Log()
            {
                Created = log.Created,
                Message = log.Message,
                Method = log.Method,
                Severity = log.Severity,
                Source = log.Source,
                StoreId = log.StoreId
            };

            androAdminEntities.AddToLogs(entity);
            androAdminEntities.SaveChanges();
        }
    }
}
