using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Model;
using System.Collections.Generic;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        public string GetBySiteId(int siteId, out List<MyAndromedaDataAccess.Domain.Employee> employees)
        {
            employees = new List<MyAndromedaDataAccess.Domain.Employee>();
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            // Check that the myAndromeda user is allowed to access this site
            var myAndromedaQuery = from e in androAdminEntities.Employees
                                   where e.SiteId == siteId
                                   select e;

            List<MyAndromedaDataAccessEntityFramework.Model.Employee> myAndromedaEntity = myAndromedaQuery.ToList();

            if (myAndromedaEntity != null)
            {
                foreach (MyAndromedaDataAccessEntityFramework.Model.Employee employeeEntity in myAndromedaEntity)
                {
                    MyAndromedaDataAccess.Domain.Employee employee = new MyAndromedaDataAccess.Domain.Employee();

                    employee.Id = employeeEntity.Id;
                    employee.Firstname = employeeEntity.Firstname;
                    employee.Role = employeeEntity.Role;
                    employee.Surname = employeeEntity.Surname;
                    employee.Phone = employeeEntity.Phone;

                    employees.Add(employee);
                }
            }

            return "";
        }

        public string DeleteById(int siteId, int employeeId)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            // Get the employee to be deleted
            var myAndromedaQuery = from e in androAdminEntities.Employees
                                   join s in androAdminEntities.Stores
                                     on e.SiteId equals s.Id
                                   where e.Id == employeeId
                                     && s.Id == siteId
                                   select e;

            MyAndromedaDataAccessEntityFramework.Model.Employee myAndromedaEntity = myAndromedaQuery.FirstOrDefault();

            if (myAndromedaEntity != null)
            {
                androAdminEntities.Employees.Remove(myAndromedaEntity);
                androAdminEntities.SaveChanges();
            }

            return "";
        }

        public string Add(int siteId, MyAndromedaDataAccess.Domain.Employee employee)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            // Create an object we can add
            MyAndromedaDataAccessEntityFramework.Model.Employee employeeEntity = new MyAndromedaDataAccessEntityFramework.Model.Employee();
            employeeEntity.Firstname = employee.Firstname;
            employeeEntity.Surname = employee.Surname;
            employeeEntity.Role = employee.Role;
            employeeEntity.SiteId = siteId;
            employeeEntity.Phone = employee.Phone;

            androAdminEntities.Employees.Add(employeeEntity);
            androAdminEntities.SaveChanges();

            return "";
        }
    }
}
