using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        public string GetBySiteId(Guid siteId, out List<AndroCloudDataAccess.Domain.Employee> employees)
        {
            employees = new List<AndroCloudDataAccess.Domain.Employee>();
            ACSEntities acsEntities = new ACSEntities();

            // Check that the myAndromeda user is allowed to access this site
            var acsQuery = from e in acsEntities.Employees
                           where e.SiteID == siteId
                           select e;

            List<Model.Employee> acsEntity = acsQuery.ToList();

            if (acsEntity != null)
            {
                foreach (Model.Employee employeeEntity in acsEntity)
                {
                    AndroCloudDataAccess.Domain.Employee employee = new AndroCloudDataAccess.Domain.Employee();

                    employee.Id = employeeEntity.ID;
                    employee.Firstname = employeeEntity.Firstname;
                    employee.Role = employeeEntity.Role;
                    employee.Surname = employeeEntity.Surname;

                    employees.Add(employee);
                }
            }

            return "";
        }

        public string DeleteByIdMyAndromedaUserId(Guid employeeId, string myAndromedaUserId)
        {
            ACSEntities acsEntities = new ACSEntities();

            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           join sg in acsEntities.SitesGroups
                             on g.ID equals sg.GroupID
                           join s in acsEntities.Sites
                             on sg.SiteID equals s.ID
                           join e2 in acsEntities.Employees
                             on s.ID equals e2.SiteID
                           where u.Username == myAndromedaUserId
                             && u.IsEnabled == true
                             && e2.ID == employeeId
                           select e2;

            Model.Employee acsEntity = acsQuery.FirstOrDefault();

            if (acsEntity != null)
            {
                acsEntities.Employees.DeleteObject(acsEntity);
                acsEntities.SaveChanges();
            }

            return "";
        }

        public string AddByMyAndromedaUserId(AndroCloudDataAccess.Domain.Employee employee, string externalSiteId, string myAndromedaUserId)
        {
            ACSEntities acsEntities = new ACSEntities();

            // Check that the myAndromeda user is allowed to access this site
            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           join sg in acsEntities.SitesGroups
                             on g.ID equals sg.GroupID
                           join s in acsEntities.Sites
                             on sg.SiteID equals s.ID
                           where u.Username == myAndromedaUserId
                             && u.IsEnabled == true
                             && s.ExternalId == externalSiteId
                           select s;

            Model.Site siteACSEntity = acsQuery.FirstOrDefault();

            if (siteACSEntity != null)
            {
                // Create an object we can add
                AndroCloudDataAccessEntityFramework.Model.Employee employeeEntity = new AndroCloudDataAccessEntityFramework.Model.Employee();
                employeeEntity.ID = Guid.NewGuid();
                employeeEntity.Firstname = employee.Firstname;
                employeeEntity.Surname = employee.Surname;
                employeeEntity.Role = employee.Role;
                employeeEntity.Site = siteACSEntity;

                acsEntities.Employees.AddObject(employeeEntity);
                acsEntities.SaveChanges();
            }

            return "";
        }
    }
}
