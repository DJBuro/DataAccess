﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyAndromedaDataAccess.Domain.Reporting
{
    public class OrdersSummary
    {
        private readonly SummaryByDay<decimal> today;

        public OrdersSummary(IEnumerable<SummaryByDay<decimal>> orderData)
        {
            this.OrderData = orderData;
            this.today = orderData.FirstOrDefault();
        }

        public IEnumerable<SummaryByDay<decimal>> OrderData { get; set; }

        public decimal TotalRangeTurnover
        {
            get { return OrderData.Sum(e => e.Total); }
        }

        public decimal TodaysTurnover
        {
            get { return today == null ? 0 : today.Total; }
        }

        /// <summary>
        /// Gets the average over the range of the data.
        /// </summary>
        /// <value>The range average.</value>
        public decimal RangeAverage
        {
            get
            {
                if (this.OrderData.Count() == 0)
                    return 0;

                return this.OrderData.Average(e => e.Total);
            }
        }

        public decimal RangeCount
        {
            get { return this.OrderData.Sum(e => e.Count); }
        }

        /// <summary>
        /// Gets or sets the today's online orders.
        /// </summary>
        /// <value>The today's online orders.</value>
        //public int TodaysOnlineOrders { get; set; }


    }

    public class SalesSummmary 
    {
        private readonly SummaryByDay<decimal> latest;

        public SalesSummmary(IEnumerable<SummaryByDay<decimal>> data)
        {
            this.Data = data;
            this.latest = data.FirstOrDefault();
        }

        public IEnumerable<SummaryByDay<decimal>> Data { get; private set; }

        public decimal Total
        {
            get
            {
                return latest == null ? 0 : latest.Total;
            }
        }

        public decimal Largest 
        {
            get { return latest == null ? 0 : latest.Max; } 
        }

        public decimal Lowest
        {
            get { return Lowest == null ? 0 : latest.Min; }
        }

        public decimal TotalAverage
        {
            get
            {
                return Data.Average(e => e.Total);
            }
        }

        
    }

    public class SummaryByDay : SummaryByDay<float> { }
    public class SummaryByDay<T>
    {
        public DateTime Day { get; set; }

        public T Total { get; set; }
        public T Count { get; set; }
        public T Average { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }
    }

    public class SummaryOfLineItem
    {
        /// <summary>
        /// Gets or sets the count if sold.
        /// </summary>
        /// <value>The count.</value>
        public int OrderCount { get; set; }
        
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the items sold count.
        /// </summary>
        /// <value>The items sold count.</value>
        public int? ItemsQuantitySold { get; set; }

        /// <summary>
        /// Gets or sets the sum price.
        /// </summary>
        /// <value>The sum price.</value>
        public double SumPrice { get; set; }

        public int? ItemPrice { get; set; }
    }

    public class SummaryOfProducts
    {
        //public ICollection<SummaryOfItem> Items { get; set; }

        public int Max { get; set; }
        //public double Avg { get { return this.Items.Average(e => e.Count); } }
    }

    //public class SummaryByDayCollection<T> : List<SummaryByDay<T>> 
    //{
    //    public SummaryByDayCollection(IEnumerable<SummaryByDay<T>> items)
    //    {
    //        this.AddRange(items);
    //    }
    //}

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