using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using DashboardDataAccess.nHibernate.Mappings;

namespace DashboardDataAccess
{
    internal class nHibernateHelper
    {
        internal static ISessionFactory SessionFactory { get; set; }

        /// <summary>
        /// Static constructor
        /// </summary>
        static nHibernateHelper()
        {
            nHibernateHelper.SessionFactory = Fluently.Configure()
              .Database
              (
                MsSqlConfiguration.MsSql2008.ConnectionString("Server=.;initial catalog=dashboard;password=D45hb0ardPa55;user=DashboardUser;")
              )
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<tbl_Log>())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<tbl_Site>())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<tbl_HistoricalData>())
              .BuildSessionFactory();
        }
    }
}
