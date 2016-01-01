using MyAndromedaDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain.Marketing;
using MyAndromedaDataAccess.Domain.Reporting.Query;
using MyAndromedaDataAccess.Domain.Reporting;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public CustomerDataAccess() { }

        public CustomersOverview GetOverview(int siteId, FilterQuery filter)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> ListByChain(int chainId)
        {
            using (Model.MyAndromedaEntities dbContext = new Model.MyAndromedaEntities()) 
            {
                var customers = dbContext.CustomerRecords;

                return customers.ToList().Select(e => e.ToDomainModel()).ToList();
            }
        }

        public IEnumerable<Customer> ListBySite(int chainId, int siteId)
        {
            using (Model.MyAndromedaEntities dbContext = new Model.MyAndromedaEntities())
            {
                var customers = dbContext.CustomerRecords;

                return customers.ToList().Select(e => e.ToDomainModel()).ToList();
            }
        }
    }
}
