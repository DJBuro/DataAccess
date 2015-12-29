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

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                // Check that the myAndromeda user is allowed to access this site
                var query = from e in entitiesContext.Employees
                                       where e.SiteId == siteId
                                       select e;

                List<MyAndromedaDataAccessEntityFramework.Model.Employee> entity = query.ToList();

                if (entity != null)
                {
                    foreach (MyAndromedaDataAccessEntityFramework.Model.Employee employeeEntity in entity)
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
            }

            return "";
        }

        public string DeleteById(int siteId, int employeeId)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                // Get the employee to be deleted
                var query = from e in entitiesContext.Employees
                                       join s in entitiesContext.Stores
                                         on e.SiteId equals s.Id
                                       where e.Id == employeeId
                                         && s.Id == siteId
                                       select e;

                MyAndromedaDataAccessEntityFramework.Model.Employee entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entitiesContext.Employees.Remove(entity);
                    entitiesContext.SaveChanges();
                }
            }

            return "";
        }

        public string Add(int siteId, MyAndromedaDataAccess.Domain.Employee employee)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                // Create an object we can add
                MyAndromedaDataAccessEntityFramework.Model.Employee entity = new MyAndromedaDataAccessEntityFramework.Model.Employee();

                entity.Firstname = employee.Firstname;
                entity.Surname = employee.Surname;
                entity.Role = employee.Role;
                entity.SiteId = siteId;
                entity.Phone = employee.Phone;

                entitiesContext.Employees.Add(entity);
                entitiesContext.SaveChanges();
            }

            return "";
        }
    }
}
