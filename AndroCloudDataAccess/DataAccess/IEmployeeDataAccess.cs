using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IEmployeeDataAccess
    {
        string GetBySiteId(Guid siteId, out List<Employee> employees);
        string DeleteByIdMyAndromedaUserId(Guid openingHoursId, string myAndromedaUserId);
        string AddByMyAndromedaUserId(Employee employee, string externalSiteId, string myAndromedaUserId);
    }
}
