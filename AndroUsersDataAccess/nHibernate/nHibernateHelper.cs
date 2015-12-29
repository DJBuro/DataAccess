using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using AndroUsersDataAccess.nHibernate.Mappings;
using System.Configuration;

namespace AndroUsersDataAccess.nHibernate
{
    internal class nHibernateHelper
    {
        internal static ISessionFactory SessionFactory { get; set; }

        /// <summary>
        /// Static constructor
        /// </summary>
        static nHibernateHelper()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["AndroUsers"].ConnectionString;

            nHibernateHelper.SessionFactory = Fluently.Configure()
                .Database
                (
                    MsSqlConfiguration.MsSql2008.ConnectionString(connectionString)        
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<AndroUserMap>())
                .BuildSessionFactory();
        }
    }
}
