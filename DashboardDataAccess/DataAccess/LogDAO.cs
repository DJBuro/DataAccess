using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DashboardDataAccess.Domain;
using DashboardDataAccess.nHibernate.Mappings;

namespace DashboardDataAccess.DataAccess
{
    public class LogDAO
    {
        public static void Create(Log log)
        {
            tbl_Log.Create(log);
        }
    }
}
