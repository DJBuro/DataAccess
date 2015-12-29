using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DashboardDataAccess.Domain;
using DashboardDataAccess.nHibernate;

namespace DashboardDataAccess.DataAccess
{
    public class DataAccess
    {
        public static void UpdateDashboardData(Site site, HistoricalData historicalData, bool isHistoricalDataNew)
        {
            nHibernateDataAccess.UpdateDashboardData(site, historicalData, isHistoricalDataNew);
        }
    }
}
