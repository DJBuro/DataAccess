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
    public class AMSServerFTPServerPairDAO : IAMSServerFTPServerPairDAO
    {
        public void Add(AMSServerFTPServerPair amsServerFTPServerPair)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Save(amsServerFTPServerPair);
                transaction.Commit();
            }
        }

        public void Delete(int id)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();

                session.CreateQuery("delete " + typeof(AndroAdminDataAccess.Domain.AMSServerFTPServerPair) + " where id = :id")
                    .SetParameter("id", id)
                    .ExecuteUpdate();

                transaction.Commit();
            }
        }

        public void Update(AMSServerFTPServerPair amsServerFTPServerPair)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Update(amsServerFTPServerPair);
                transaction.Commit();
            }
        }
    }
}
