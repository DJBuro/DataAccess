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

    public class SummaryByDay : SummaryByDay<float> { }
    public class SummaryByDay<T>
    {
        public DateTime Day { get; set; }

        public T Total { get; set; }
        public T Average { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public class SummaryByDayCollection<T> : List<SummaryByDay<T>> 
    {
        public SummaryByDayCollection(IEnumerable<SummaryByDay<T>> items)
        {
            this.AddRange(items);
        }
    }

    //public class SummaryOverview<T> : SummaryByDay<float>
    //{
    //    private readonly IEnumerable<SummaryByDay<float>> items;

    //    public SummaryOverview(IEnumerable<SummaryByDay<float>> items) 
    //    {
    //        this.items = items;
    //    }

    //    private int total; 
    //    public int Total
    //    {
    //        get 
    //        {
    //            if (total.Equals(default(T)))
    //            {
    //                total = items.Sum(item => item.Total);
    //            }

    //            return total;
    //        }
    //    }

    //    public float count;
    //    public float Count 
    //    {
    //        get 
    //        {
    //            if (count == 0) 
    //            {
    //                count = items.Count();
    //            }
    //            return count;
    //        }
    //    }

    //    private float average;
    //    public float Average
    //    {
    //        get 
    //        {
    //            if( average == 0) 
    //            {
    //                average = items.Average(e => (float)e.Total);
    //            }

    //            return average;
    //        }
    //    }

    //    private float min;
    //    public float Min 
    //    {
    //        get 
    //        {
    //            if (min == 0) 
    //            {
    //                min = items.Min(e => (float)e.Total);   
    //            }

    //            return min;
    //        }
    //    }

    //    private float max;
    //    public float Max 
    //    {
    //        get 
    //        {
    //            if (max == 0) 
    //            {
    //                max = items.Min(e => (float)e.Total);
    //            }

    //            return max;
    //        }
    //    }

    //}
}