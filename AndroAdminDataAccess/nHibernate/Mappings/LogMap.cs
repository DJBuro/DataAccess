using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class LogMap : ClassMap<Log>
    {
        public LogMap()
        {
            Table("Log");
            Id(x => x.Id);
            Map(x => x.StoreId);
            Map(x => x.Severity);
            Map(x => x.Message);
            Map(x => x.Method);
            Map(x => x.Source);
            Map(x => x.Created);
        }
    }
}
