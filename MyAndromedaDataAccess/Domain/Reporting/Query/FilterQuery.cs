using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccess.Domain.Reporting.Query
{
    public class FilterQuery
    {
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public DateTime? FilterFrom { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public DateTime? FilterTo { get; set; }

        public double TotalDays 
        {
            get 
            {
                if (!FilterTo.HasValue && !FilterFrom.HasValue)
                    return 0;

                TimeSpan block = FilterTo.Value.Subtract(FilterFrom.Value);

                return block.TotalDays;
            } 
        }
    }
}
