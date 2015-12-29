using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.DataAccess;

namespace MyAndromedaDataAccess
{
    public interface IDataAccessFactory
    {
        ISiteDataAccess SiteDataAccess { get; set; }
        IMyAndromedaUserDataAccess MyAndromedaUserDataAccess { get; set; }
        IAddressDataAccess AddressDataAccess { get; set; }
        IEmployeeDataAccess EmployeeDataAccess { get; set; }
        IOpeningHoursDataAccess OpeningHoursDataAccess { get; set; }
        ISiteDetailsDataAccess SiteDetailsDataAccess { get; set; }
        ICountryDataAccess CountryDataAccess { get; set; }

        /// <summary>
        /// Gets the customer data access.
        /// </summary>
        /// <value>The customer data access.</value>
        ICustomerDataAccess CustomerDataAccess { get; }

        /// <summary>
        /// Gets the email campaign data access.
        /// </summary>
        /// <value>The email campaign data access.</value>
        IEmailCampaignDataAccess EmailCampaignDataAccess { get; }
    }
}
