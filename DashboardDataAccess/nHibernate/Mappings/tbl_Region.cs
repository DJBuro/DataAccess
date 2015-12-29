using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using DashboardDataAccess.Domain;

namespace DashboardDataAccess.nHibernate.Mappings
{
    public class tbl_Region : ClassMap<Region>
    {
        public tbl_Region()
        {
            Table("tbl_Region");
            Id(x => x.Id);
            Map(x => x.RegionName);
            Map(x => x.HeadOfficeID);
            Map(x => x.TimeZone);
        }
    }
}