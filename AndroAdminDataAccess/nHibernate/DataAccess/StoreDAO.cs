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
    public class StoreDAO : IStoreDAO
    {
        public IEnumerable<AndroAdminDataAccess.Domain.Store> GetAll()
        {
            IEnumerable<AndroAdminDataAccess.Domain.Store> stores = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                stores = session.CreateQuery("from " + typeof(AndroAdminDataAccess.Domain.Store)).List<AndroAdminDataAccess.Domain.Store>();
            }

            return stores;
        }

        public void Add(AndroAdminDataAccess.Domain.Store store)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Save(store);
                transaction.Commit();
            }
        }

        public void Update(AndroAdminDataAccess.Domain.Store store)
        {
            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                ITransaction transaction = session.BeginTransaction();
                transaction.Begin();
                session.Update(store);
                transaction.Commit();
            }
        }

        public AndroAdminDataAccess.Domain.Store GetById(int id)
        {
            AndroAdminDataAccess.Domain.Store store = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                store = session.Get<AndroAdminDataAccess.Domain.Store>(id);
            }

            return store;
        }

        public AndroAdminDataAccess.Domain.Store GetByAndromedaId(int andromedaStoreId)
        {
            IList<Store> stores = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                stores = session.CreateQuery("from " + typeof(AndroAdminDataAccess.Domain.Store) + " where AndromedaSiteId=:andromedaSiteId")
                    .SetParameter("andromedaSiteId", andromedaStoreId)
                    .List<Store>();
            }

            Store store = null;

            if (stores.Count == 1)
            {
                store = stores[0];
            }

            return store;
        }
    }
}