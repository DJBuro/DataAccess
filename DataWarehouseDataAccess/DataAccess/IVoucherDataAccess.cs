using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface IVoucherDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string GetVouchersByAndromedaSiteId(int andromedaSiteId, out List<DataWarehouseDataAccess.Domain.VoucherCode> voucherList);
        string GetSingleVoucherByAndromedaSiteId(int andromedaSiteId, string voucherCode, out DataWarehouseDataAccess.Domain.VoucherCode voucherCodeObj);
        string GetUsedVoucherCount(string voucherId, string customerId, out int usedVoucherCount);
    }
}
