using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using DashboardDataAccess.Domain;

namespace DashboardDataAccess.nHibernate.Mappings
{
    public class tbl_HistoricalData : ClassMap<HistoricalData>
    {
        public tbl_HistoricalData()
        {
            Table("tbl_HistoricalData");
            Id(x => x.Id);           
            Map(x => x.SiteId);
            Map(x => x.Name);
            Map(x => x.HeadOfficeId);
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
            Map(x => x.TradingDay);
        }

        internal static void Update(ISession session, HistoricalData historicalData)
        {
            session.Update(historicalData);
        }

        internal static void Create(ISession session, HistoricalData historicalData)
        {
            session.Save(historicalData);
        }

        internal static HistoricalData FindBySiteIdAndTradingDay(int siteId, DateTime tradingDay)
        {
            HistoricalData historicalData = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                const string hql = "select hd from HistoricalData as hd where hd.SiteId =:SITEID and hd.TradingDay = :TRADINGDAY";

                var query = session.CreateQuery(hql);

                query.SetInt32("SITEID", siteId);
                query.SetDateTime("TRADINGDAY", tradingDay);

                IList<HistoricalData> historicalDatas = query.List<HistoricalData>();

                if (historicalDatas != null && historicalDatas.Count == 1)
                {
                    historicalData = historicalDatas[0];
                }

                return historicalData;
            }
        }
    }
}
