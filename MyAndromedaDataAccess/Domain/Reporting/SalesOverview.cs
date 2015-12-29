using System;
using System.Linq;

namespace MyAndromedaDataAccess.Domain.Reporting
{
    public class OrderOverview 
    {
    
    }

    public class SalesOverview
    {
        /// <summary>
        /// Gets or sets the today.
        /// </summary>
        /// <value>The today.</value>
        public decimal TodaysTotal { get; set; }

        /// <summary>
        /// Gets or sets the today's average.
        /// </summary>
        /// <value>The today's average.</value>
        public decimal TodaysAverage { get; set; }

        /// <summary>
        /// Gets or sets the week data.
        /// </summary>
        /// <value>The week data.</value>
        public decimal[] WeekData { get; set; }

        /// <summary>
        /// Gets or sets the week averages.
        /// </summary>
        /// <value>The week averages.</value>
        public decimal[] WeekAverages { get; set; }

        /// <summary>
        /// Gets or sets the last week.
        /// </summary>
        /// <value>The last week.</value>
        public decimal? LastWeek { get; set; }

        /// <summary>
        /// Gets or sets the last year.
        /// </summary>
        /// <value>The last year.</value>
        public decimal? LastYear { get; set; }
    }
}