using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Lists the by chain.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        IEnumerable<Customer> ListByChain(int chainId);

        /// <summary>
        /// Lists the by site.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        IEnumerable<Customer> ListBySite(int siteId);

        /// <summary>
        /// Lists the by application id.
        /// </summary>
        /// <param name="acsApplicationId">The acs application id.</param>
        /// <returns></returns>
        IEnumerable<Customer> ListByAcsApplicationId(int acsApplicationId);
    }
}
