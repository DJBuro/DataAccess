using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccess.Domain.Reporting
{
    public class VoucherSummary
    {
        public IEnumerable<VoucherSummaryByCode<decimal>> voucherData { get; set; }

        public VoucherSummary(IEnumerable<VoucherSummaryByCode<decimal>> voucherData)
        {
            this.voucherData = voucherData;
        }

        public decimal TotalTurnover
        {
            get { return voucherData.Count() > 0 ? voucherData.Sum(e => e.Total) : 0; }
        }
        public decimal TotalAverage
        {
            get { return voucherData.Count() > 0 ? voucherData.Average(e => e.Average) : 0; }
        }
        public int TotalNumberofUses
        {
            get { return voucherData.Count() > 0 ? Convert.ToInt32(voucherData.Sum(e => e.NumberOfUses)) : 0; }
        }
    }

    public class VoucherSummaryByCode : VoucherSummaryByCode<decimal> { }
    public class VoucherSummaryByCode<T>
    {
        public DateTime Day { get; set; }
        public T Total { get; set; }
        public T NumberOfUses { get; set; }
        public T Average { get; set; }
        public T Min { get; set; }
        public T Max { get; set; }
    }
}
