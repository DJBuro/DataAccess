using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using MyAndromedaDataAccessEntityFramework.Model.CustomerDataWarehouse;

namespace MyAndromedaDataAccessEntityFramework.QueryExtensions
{
    public static class AndroAdminExtensions
    {
        public static IQueryable<Model.AndroAdmin.ACSApplication> GetAcsApplications(this DbSet<Store> Store, int storeId) 
        {
            return Store.Where(e => e.Id == storeId).SelectMany(e => e.ACSApplicationSites.Select(appplicationSite => appplicationSite.ACSApplication));
        }
    }

    public static class DataWarehouseExtensions
    {
        /// <summary>
        /// Filters the customer for contact details that allow marketing.
        /// </summary>
        /// <param name="customerQuery">The customer query.</param>
        /// <returns></returns>
        public static IQueryable<Model.CustomerDataWarehouse.CustomerRecord> FilterForMarketingByEmailType(this IQueryable<CustomerRecord> customerQuery) 
        {
            return customerQuery.Where(e => e.Contacts.Any(
                contact =>
                          contact.ContactTypeId == MyAndromedaDataAccess.Domain.DataWarehouse.ContactType.Email &&
                          contact.MarketingLevelId == MyAndromedaDataAccess.Domain.DataWarehouse.MarketingLevelType.ThirdParty));
        }
    }
}