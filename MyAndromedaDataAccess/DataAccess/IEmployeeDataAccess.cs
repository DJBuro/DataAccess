using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IEmployeeDataAccess
    {
        string GetBySiteId(int siteId, out List<Employee> employees);
        string DeleteById(int siteId, int employeeId);
        string Add(int siteId, MyAndromedaDataAccess.Domain.Employee employee);
    }
}
