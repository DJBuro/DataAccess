using MyAndromedaDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public CustomerDataAccess() { }

        public IEnumerable<Customer> ListByChain(int chainId)
        {
            using (Model.MyAndromedaEntities dbContext = new Model.MyAndromedaEntities()) 
            {
                var customers = dbContext.CustomerRecords;

                return customers.Select(e => e.ToDomainModel()).ToList();
            }
        }

        public IEnumerable<Customer> ListBySite(int chainId, int siteId)
        {
            using (Model.MyAndromedaEntities dbContext = new Model.MyAndromedaEntities())
            {
                var customers = dbContext.CustomerRecords;

                return customers.Select(e => e.ToDomainModel()).ToList();
            }
        }
    }
}
