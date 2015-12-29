using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AndroAdminDataAccess.nHibernate.Mappings;
using System.Configuration;

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
            string connectionString = ConfigurationManager.ConnectionStrings["AndroAdmin"].ConnectionString;
            nHibernateHelper.SessionFactory = Fluently.Configure()
                .Database
                (
                    MsSqlConfiguration.MsSql2008.ConnectionString(connectionString)
                )
 //               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AMSServerFTPServerPairMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AMSServerMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<FTPSiteMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StoreMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LogMap>())
                .BuildSessionFactory();
        }
    }
}
