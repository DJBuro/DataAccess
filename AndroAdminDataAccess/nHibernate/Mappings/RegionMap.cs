using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using DashboardDataAccess.Domain;

namespace DashboardDataAccess.nHibernate.Mappings
{
    public class RegionMap : ClassMap<Region>
    {
        public RegionMap()
        {
            Table("tbl_Region");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Url);
            Map(x => x.Port);

            Map(x => x.Username);
            Map(x => x.Password);
            Map(x => x.IsPrimary);
        }
    }
}