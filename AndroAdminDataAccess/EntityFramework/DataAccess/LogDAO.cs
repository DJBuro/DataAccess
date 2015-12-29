using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Data.Entity.Validation;

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
                    Message = log.Message == null ? "" : log.Message,
                    Method = log.Method == null ? "" : log.Method.Length > 200 ? log.Method.Substring(0, 200) : log.Method,
                    Severity = log.Severity == null ? "" : log.Severity,
                    Source = log.Source == null ? "" : log.Source,
                    StoreId = log.StoreId == null ? "" : log.StoreId
                };

                entitiesContext.Logs.Add(entity);

                try
                {
                    entitiesContext.SaveChanges();
                }
                catch(DbEntityValidationException exception)
                {
                    string exceptionMessage = "DbEntityValidationException exception > ";
                    foreach (var entityValidationErrors in exception.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            exceptionMessage += "Property:" + validationError.PropertyName + " Error:" + validationError.ErrorMessage;
                        }
                    }

                    throw new Exception(exceptionMessage);
                }
            }
        }

    }
}
