using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using DashboardDataAccess.Domain;

namespace DashboardDataAccess.nHibernate.Mappings
{
    public class AMSServerMap : ClassMap<AMSServer>
    {
        public AMSServerMap()
        {
            Table("tbl_Log");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);
        }
    }
}
