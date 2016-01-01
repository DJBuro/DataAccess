﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DashboardDataAccess.Domain;
using DashboardDataAccess.nHibernate.Mappings;

namespace DashboardDataAccess.DataAccess
{
    public class SiteDAO
    {
        public static Site FindBySiteId(int? ramesesId)
        {
            return tbl_Site.FindBySiteId(ramesesId);
        }
    }
}