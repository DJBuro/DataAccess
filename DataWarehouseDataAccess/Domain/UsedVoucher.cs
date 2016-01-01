using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class UsedVoucher
    {
        public System.Guid VoucherId { get; set; }
        public System.Guid CustomerId { get; set; }
        public System.Guid OrderId { get; set; }
        public VoucherCode Voucher { get; set; }
    }
}
