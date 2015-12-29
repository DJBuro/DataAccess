using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;

namespace DashboardDataAccess
{
    public class tbl_Log : ClassMap<Log>
    {
        public tbl_Log()
        {
            Table("tbl_Log");
            Id(x => x.Id);
            Map(x => x.StoreId);
            Map(x => x.Severity);
            Map(x => x.Message);
            Map(x => x.Method);
            Map(x => x.Source);
            Map(x => x.Created);
        }

        internal static void Create(Log log)
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
