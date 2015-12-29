using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccess.Domain.Marketing;
using MyAndromedaDataAccess.Domain.Reporting;
using MyAndromedaDataAccess.Domain.Reporting.Query;
using MyAndromedaDataAccessEntityFramework.QueryExtensions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class CustomerDataAccess : ICustomerDataAccess 
    {
        public CustomerDataAccess() { }

        public IEnumerable<Customer> ListByChain(int chainId)
        {
            IList<int> acsApplicationIds;
            using (var androAdminContext = new Model.AndroAdmin.AndroAdminDbContext())
            {
                acsApplicationIds = androAdminContext.Stores
                    .Where(e=> e.ChainId == chainId)
                    .SelectMany(e=> e.ACSApplicationSites)
                    .Select(e=> e.ACSApplicationId).Distinct().ToList();
            }

            var customers = ListByApplicaitonIds(acsApplicationIds);
            return customers;
        }

        public IEnumerable<Customer> ListBySite(int siteId)
        {
            IList<int> acsApplicationIds; 
            using (var androAdminContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                acsApplicationIds = androAdminContext.Stores.GetAcsApplications(siteId).Select(e=> e.Id).ToList();
            }

            var customers = ListByApplicaitonIds(acsApplicationIds);
            return customers;
        }

        private IList<Customer> ListByApplicaitonIds(IList<int> acsApplicationIds) 
        {
            IList<Customer> customers;
            using (var dataWareHouseContext = new Model.CustomerDataWarehouse.CustomerWarehouseDbContext())
            {
                var result = dataWareHouseContext.CustomerRecords
                    .Where(customer => acsApplicationIds.Any(id => id == customer.ACSAplicationId))
                    .FilterForMarketingByEmailType()
                    .ToArray();

                customers = result.Select(e => e.ToDomainModel()).ToList();
            }

            return customers;
        }

        public IEnumerable<Customer> ListByAcsApplicationId(int acsApplicationId)
        {
            IList<Customer> customers;
            using (var dataWareHouseContext = new Model.CustomerDataWarehouse.CustomerWarehouseDbContext())
            {
                var result = dataWareHouseContext.CustomerRecords.Where(customer => customer.ACSAplicationId == acsApplicationId).ToArray();

                customers = result.Select(e => e.ToDomainModel()).ToList();
            }

            return customers;
        }

        public CustomersOverview GetOverview(int siteId, FilterQuery filter)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        //public IEnumerable<Customer> ListByChain(int chainId)
        //{
        //    using (Model.MyAndro.Entities dbContext = new Model.MyAndro.Entities()) 
        //    {
        //        var customers = dbContext.CustomerRecords;

        //        return customers.ToList().Select(e => e.ToDomainModel()).ToList();
        //    }
        //}

        //public IEnumerable<Customer> ListBySite(int chainId, int siteId)
        //{
        //    using (Model.MyAndro.Entities dbContext = new Model.MyAndro.Entities())
        //    {
        //        var customers = dbContext.CustomerRecords;

        //        return customers.ToList().Select(e => e.ToDomainModel()).ToList();
        //    }
        //}
    }
}
