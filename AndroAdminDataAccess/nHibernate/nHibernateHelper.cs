using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AndroAdminDataAccess.nHibernate.Mappings;

namespace AndroAdminDataAccess
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
                MsSqlConfiguration.MsSql2008.ConnectionString("Server=.;initial catalog=androadmin;password=r2Is!islMM$;user=AndroAdminUser;")
              )
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AMSServerFTPServerPairMap>())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AMSServerMap>())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<FTPSiteMap>())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StoreMap>())
              .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LogMap>())
              .BuildSessionFactory();
        }
    }
}
