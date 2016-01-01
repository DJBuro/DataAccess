using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace DashboardDataAccess
{
    public class Site
    {
        public virtual int Id { get; set; }
        public virtual int SiteId { get; set; }
        public virtual string Name { get; set; }
        public virtual int HeadOfficeID { get; set; }
        public virtual string IPAddress { get; set; }
        public virtual int SiteTypeId { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual int SiteKey { get; set; }
        public virtual int RegionId { get; set; }
        public virtual DateTime? LastUpdated { get; set; }
        public virtual int Column_1 { get; set; }
        public virtual int Column_2 { get; set; }
        public virtual int Column_3 { get; set; }
        public virtual int Column_4 { get; set; }
        public virtual int Column_5 { get; set; }
        public virtual int Column_6 { get; set; }
        public virtual int Column_7 { get; set; }
        public virtual int Column_8 { get; set; }
        public virtual int Column_9 { get; set; }
        public virtual int Column_10 { get; set; }
        public virtual int Column_11 { get; set; }
        public virtual int Column_12 { get; set; }
        public virtual int Column_13 { get; set; }
        public virtual int Column_14 { get; set; }
        public virtual int Column_15 { get; set; }
        public virtual int Column_16 { get; set; }
        public virtual int Column_17 { get; set; }
        public virtual int Column_18 { get; set; }
        public virtual int Column_19 { get; set; }
        public virtual int Column_20 { get; set; }
        public virtual bool? Comp { get; set; }
        public virtual int? Column_21 { get; set; }

        public static Site FindBySiteId(int? ramesesId)
        {
            Site site = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                const string hql = "select s from Site as s where s.SiteId = :RAMESESID";

                var query = session.CreateQuery(hql);

                query.SetInt32("RAMESESID", ramesesId.Value);

                query.SetCacheable(true);

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