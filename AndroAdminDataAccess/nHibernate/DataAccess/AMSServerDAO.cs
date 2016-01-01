using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using AndroAdminDataAccess.nHibernate.Mappings;
using FluentNHibernate.Mapping;
using NHibernate;

namespace AndroAdminDataAccess.nHibernate.DataAccess
{
    public class AMSServerDAO : IAMSServerDAO
    {
        public IList<AndroAdminDataAccess.Domain.AMSServer> GetAll()
        {
            IList<AndroAdminDataAccess.Domain.AMSServer> amsServers = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                amsServers = session.CreateQuery("from " + typeof(AndroAdminDataAccess.Domain.AMSServer)).List<AndroAdminDataAccess.Domain.AMSServer>();
            }

            return amsServers;
        }

        public void Add(AMSServer amsServer)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Save(amsServer);
                transaction.Commit();
            }
        }

        public void Update(AMSServer amsServer)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Update(amsServer);
                transaction.Commit();
            }
        }

        public AMSServer GetById(int id)
        {
            AndroAdminDataAccess.Domain.AMSServer amsServer = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                amsServer = session.Get<AndroAdminDataAccess.Domain.AMSServer>(id);
            }

            return amsServer;
        }

        public AMSServer GetByName(string name)
        {
            IList<AMSServer> amsServers = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                amsServers = session.CreateQuery("from " + typeof(AndroAdminDataAccess.Domain.AMSServer) + " where name=:name")
                    .SetParameter("name", name)
                    .List<AMSServer>();
            }

            AMSServer amsServer = null;

            if (amsServers.Count == 1)
            {
                amsServer = amsServers[0];
            }

            return amsServer;
        }
    }
}
