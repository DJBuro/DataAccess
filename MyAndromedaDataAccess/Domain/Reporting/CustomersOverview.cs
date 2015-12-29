using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAndromedaDataAccess.Domain.Reporting
{
    public class CustomersOverview
    {
        /// <summary>
        /// Gets or sets the new customers today.
        /// </summary>
        /// <value>The new customers today.</value>
        public int NewCustomersToday { get; set; }

        /// <summary>
        /// Gets or sets the new customers week.
        /// </summary>
        /// <value>The new customers week.</value>
        public int NewCustomersThisWeek { get; set; }

        /// <summary>
        /// Gets or sets the new customers year.
        /// </summary>
        /// <value>The new customers year.</value>
        public int NewCustomersThisYear { get; set; }
    }
}