using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAndromedaDataAccess.Domain.Reporting
{
    public class OrdersSummary
    {
        /// <summary>
        /// Gets or sets the today's total orders.
        /// </summary>
        /// <value>The today's total orders.</value>
        public int TodaysTotalOrders { get; set; }

        /// <summary>
        /// Gets or sets the today's online orders.
        /// </summary>
        /// <value>The today's online orders.</value>
        public int TodaysOnlineOrders { get; set; }

    }
}