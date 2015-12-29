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
        IGroupsDataAccess GroupDataAccess { get; set; }
        IMyAndromedaUserDataAccess MyAndromedaUserDataAccess { get; set; }
        IAddressDataAccess AddressDataAccess { get; set; }
        IEmployeeDataAccess EmployeeDataAccess { get; set; }
        IOpeningHoursDataAccess OpeningHoursDataAccess { get; set; }
        ISiteDetailsDataAccess SiteDetailsDataAccess { get; set; }
    }
}
