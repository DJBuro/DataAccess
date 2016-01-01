using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using DashboardDataAccess.Domain;

namespace DashboardDataAccess.nHibernate.Mappings
{
    public class tbl_Site : ClassMap<Site>
    {
        public tbl_Site()
        {
            Table("tbl_Site");
            Id(x => x.Id);
            Map(x => x.SiteId);
            Map(x => x.Name);
            Map(x => x.HeadOfficeID);
            Map(x => x.IPAddress);
            Map(x => x.SiteTypeId);
            Map(x => x.Enabled);
            Map(x => x.SiteKey);
 //           Map(x => x.RegionId);
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
            References(x => x.Region).ForeignKey("FK_tbl_Site_tbl_Region").Column("RegionId").Not.LazyLoad();
        }

        internal static Site FindBySiteId(int? ramesesId)
        {
            Site site = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                const string hql = "select s from Site as s where s.SiteId = :RAMESESID";

                var query = session.CreateQuery(hql);

                query.SetInt32("RAMESESID", ramesesId.Value);

                IList<Site> sites = query.List<Site>();

                if (sites != null && sites.Count == 1)
                {
                    site = sites[0];
                }
            }

            return site;
        }
    }
}