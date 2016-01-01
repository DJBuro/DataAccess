using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DashboardDataAccess.Domain;
using DashboardDataAccess.nHibernate.Mappings;

namespace DashboardDataAccess.DataAccess
{
    public class HistoricalDataDAO
    {
        public static HistoricalData FindBySiteIdAndTradingDay(int siteId, DateTime tradingDay)
        {
            return tbl_HistoricalData.FindBySiteIdAndTradingDay(siteId, tradingDay);
        }
    }
}
