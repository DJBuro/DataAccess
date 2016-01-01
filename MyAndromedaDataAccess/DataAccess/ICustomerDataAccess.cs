using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain.Marketing;
using MyAndromedaDataAccess.Domain.Reporting.Query;
using MyAndromedaDataAccess.Domain.Reporting;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface ICustomerDataAccess
    {
        /// <summary>
        /// Gets the overview.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        CustomersOverview GetOverview(int siteId, FilterQuery filter);

        IEnumerable<Customer> ListByChain(int p);

        IEnumerable<Customer> ListBySite(int p1, int p2);
    }
}
