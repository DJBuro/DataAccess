using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class VoucherCode
    {
        public System.Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public List<string> Occasions { get; set; }
        public decimal? MinimumOrderAmount { get; set; }
        public int? MaxRepetitions { get; set; }
        public bool Combinable { get; set; }
        public System.DateTime? StartDateTime { get; set; }
        public System.DateTime? EndDataTime { get; set; }
        public List<string> AvailableOnDays { get; set; }
        public System.TimeSpan? StartTimeOfDayAvailable { get; set; }
        public System.TimeSpan? EndTimeOfDayAvailable { get; set; }
        public bool IsActive { get; set; }
        public bool IsRemoved { get; set; }
        public string DiscountType { get; set; }
        public decimal DiscountValue { get; set; }

        public string stringOccasions { set; get; }
        public string stringAvailableDays { set; get; }
    }
}
