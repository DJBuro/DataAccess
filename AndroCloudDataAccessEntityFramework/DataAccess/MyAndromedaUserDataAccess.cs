using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class MyAndromedaUserDataAccess : IMyAndromedaUserDataAccess
    {
        public bool ValidateUser(string username, string password)
        {
            ACSEntities acsEntities = new ACSEntities();

            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           where u.Username == username
                             && u.Password == password
                             && u.IsEnabled == true
                           select u;

            var acsEntity = acsQuery.FirstOrDefault();

            if (acsEntity != null)
            {
                return true;
            }

            return false;
        }

        public string GetByUsername(string username, out AndroCloudDataAccess.Domain.MyAndromedaUser myAndromedaUser)
        {
            ACSEntities acsEntities = new ACSEntities();

            myAndromedaUser = null;

            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           where u.Username == username
                             && u.IsEnabled == true
                           
                           select new { u.Username, e.Firstname, e.Surname, g.SitesGroups};

            var acsEntity = acsQuery.FirstOrDefault();

            if (acsEntity != null)
            {
                myAndromedaUser = new AndroCloudDataAccess.Domain.MyAndromedaUser();

                myAndromedaUser.Username = acsEntity.Username;
                myAndromedaUser.Firstname = acsEntity.Firstname;
                myAndromedaUser.Surname = acsEntity.Surname;

                myAndromedaUser.Sites = new System.Collections.Generic.List<AndroCloudDataAccess.Domain.Site>();

                if (acsEntity.SitesGroups != null)
                {
                    foreach (SitesGroup sitesGroup in acsEntity.SitesGroups)
                    {
                        AndroCloudDataAccess.Domain.Site site = new AndroCloudDataAccess.Domain.Site();
                        site.Name = sitesGroup.Site.SiteName;
                        site.Id = sitesGroup.Site.ID;

                        myAndromedaUser.Sites.Add(site);
                    }
                }
            }

            return "";
        }
    }
}
