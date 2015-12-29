using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace DashboardDataAccess
{
    public class SiteMap : ClassMap<Site>
    {
        public SiteMap()
        {
            Id(x => x.Id);
            Map(x => x.SiteId);
            Map(x => x.Name);
            Map(x => x.HeadOfficeID);
            Map(x => x.IPAddress);
            Map(x => x.SiteTypeId);
            Map(x => x.Enabled);
            Map(x => x.SiteKey);
            Map(x => x.RegionId);
            Map(x => x.LastUpdated);
            Map(x => x.Column_1);
            Map(x => x.Column_2);
            Map(x => x.Column_3);
            Map(x => x.Column_4);
            Map(x => x.Column_5);
            Map(x => x.Column_6);
            Map(x => x.Column_7);
            Map(x => x.Column_8);
            Map(x => x.Column_9);
            Map(x => x.Column_10);
            Map(x => x.Column_11);
            Map(x => x.Column_12);
            Map(x => x.Column_13);
            Map(x => x.Column_14);
            Map(x => x.Column_15);
            Map(x => x.Column_16);
            Map(x => x.Column_17);
            Map(x => x.Column_18);
            Map(x => x.Column_19);
            Map(x => x.Column_20);
            Map(x => x.Comp);
            Map(x => x.Column_21);
        }
    }
}