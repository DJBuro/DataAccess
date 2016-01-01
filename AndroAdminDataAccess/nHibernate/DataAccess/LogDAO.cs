using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using AndroAdminDataAccess.nHibernate.Mappings;
using NHibernate;

namespace AndroAdminDataAccess.nHibernate.DataAccess
{
    public class LogDAO : ILogDAO
    {
        public IEnumerable<AndroAdminDataAccess.Domain.Log> GetAll()
        {
            IEnumerable<AndroAdminDataAccess.Domain.Log> logs = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                logs = session.CreateQuery("from " + typeof(AndroAdminDataAccess.Domain.Log)).List<AndroAdminDataAccess.Domain.Log>();
            }

            return logs;
        }

        public void Add(AndroAdminDataAccess.Domain.Log log)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Save(log);
                transaction.Commit();
            }
        }
    }
}
