using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using NHibernate;

namespace AndroAdminDataAccess
{
    internal class nHibernateDataAccess
    {
        //internal static void UpdateDashboardData(Site site, HistoricalData historicalData, bool isHistoricalDataNew)
        //{
        //    using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
        //    {
        //        // We need to update multiple objects
        //        ITransaction transaction = session.BeginTransaction();
        //        transaction.Begin();

        //        // Update the site
        //        session.Update(site);
                
        //        // Is the historical data new?
        //        if (isHistoricalDataNew)
        //        {
        //            // Insert the new historical data
        //            session.Save(historicalData);
        //        }
        //        else
        //        {
        //            // Update the existing historical data
        //            session.Update(historicalData);
        //        }

        //        transaction.Commit();
        //    }
        //}
    }
}
