using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccessEntityFramework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Objects;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class VoucherDataAccess : IVoucherDataAccess
    {
        public string ConnectionStringOverride { get; set; }
        
        public string GetVouchersByAndromedaSiteId(int andromedaSiteId, out List<DataWarehouseDataAccess.Domain.VoucherCode> voucherList)
        {
            voucherList = new List<DataWarehouseDataAccess.Domain.VoucherCode>();
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);
                var query =
                    (from voucher in dataWarehouseEntities.Vouchers
                    join
                        vSite in dataWarehouseEntities.SiteVouchers on voucher.Id equals vSite.VoucherId
                    where vSite.AndromedaSiteId == andromedaSiteId
                    select voucher).ToList();
                    
                 var list = from voucher in query select(
                    new DataWarehouseDataAccess.Domain.VoucherCode()
                    {
                        Id = voucher.Id,
                        Code = voucher.VoucherCode,
                        Description = voucher.Description,
                        Occasions = voucher.Occasion != null ? new List<string>(voucher.Occasion.Split(',')) : new List<string>(),
                        MinimumOrderAmount = voucher.MinimumOrderAmount,
                        MaxRepetitions = voucher.MaxRepetitions,
                        Combinable = voucher.Combinable,
                        StartDateTime = voucher.StartDateTime,
                        EndDataTime = voucher.EndDataTime,
                        AvailableOnDays = voucher.AvailableOnDays != null ? new List<string>(voucher.AvailableOnDays.Split(',')): new List<string>(),
                        StartTimeOfDayAvailable = voucher.StartTimeOfDayAvailable,
                        EndTimeOfDayAvailable = voucher.EndTimeOfDayAvailable,
                        IsActive = !voucher.Removed,
                        DiscountType = voucher.DiscountType,
                        DiscountValue = voucher.DiscountValue,
                        stringOccasions = voucher.Occasion,
                        stringAvailableDays = voucher.AvailableOnDays
                    });

                 voucherList.AddRange(list);
            }

            return "";
        }
        public string GetSingleVoucherByAndromedaSiteId(int andromedaSiteId, string voucherCode, out DataWarehouseDataAccess.Domain.VoucherCode voucherCodeObj)
        {
            voucherCodeObj = null;
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);
                var query =
                    (from voucher in dataWarehouseEntities.Vouchers
                     join
                         vSite in dataWarehouseEntities.SiteVouchers on voucher.Id equals vSite.VoucherId
                     where vSite.AndromedaSiteId == andromedaSiteId && voucher.VoucherCode == voucherCode
                     select voucher);
                var voucherEntity = query.FirstOrDefault();

                if (voucherEntity != null)
                {
                    voucherCodeObj = new DataWarehouseDataAccess.Domain.VoucherCode();
                    voucherCodeObj.Id = voucherEntity.Id;
                    voucherCodeObj.Code = voucherEntity.VoucherCode;
                    voucherCodeObj.Description = voucherEntity.Description;
                    voucherCodeObj.Occasions = voucherEntity.Occasion != null ? new List<string>(voucherEntity.Occasion.Split(',')) : new List<string>();
                    voucherCodeObj.MinimumOrderAmount = voucherEntity.MinimumOrderAmount;
                    voucherCodeObj.MaxRepetitions = voucherEntity.MaxRepetitions;
                    voucherCodeObj.Combinable = voucherEntity.Combinable;
                    voucherCodeObj.StartDateTime = voucherEntity.StartDateTime;
                    voucherCodeObj.EndDataTime = voucherEntity.EndDataTime;
                    voucherCodeObj.AvailableOnDays = voucherEntity.AvailableOnDays != null ? new List<string>(voucherEntity.AvailableOnDays.Split(',')) : new List<string>();
                    voucherCodeObj.StartTimeOfDayAvailable = voucherEntity.StartTimeOfDayAvailable;
                    voucherCodeObj.EndTimeOfDayAvailable = voucherEntity.EndTimeOfDayAvailable;
                    voucherCodeObj.IsActive = voucherEntity.Active;
                    voucherCodeObj.DiscountType = voucherEntity.DiscountType;
                    voucherCodeObj.DiscountValue = voucherEntity.DiscountValue;
                    voucherCodeObj.stringOccasions = voucherEntity.Occasion;
                    voucherCodeObj.stringAvailableDays = voucherEntity.AvailableOnDays;
                    voucherCodeObj.IsRemoved = voucherEntity.Removed;
                }
            }

            return "";
        }

        public string GetUsedVoucherCount(string voucherId, string customerId, out int usedVoucherCount)
        {
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);
                var query = (from usedVoucher in dataWarehouseEntities.UsedVouchers
                             join oh in dataWarehouseEntities.OrderHeaders on usedVoucher.OrderId equals oh.ID
                             join customer in dataWarehouseEntities.Customers on usedVoucher.CustomerId equals customer.ID
                             where usedVoucher.VoucherId == new Guid(voucherId) && customer.ID == new Guid(customerId) && oh.Status != 6 // omit cancelled orders
                             select usedVoucher);
                usedVoucherCount = query.Count();
            }
            return "";
        }
    }
}
