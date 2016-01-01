using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class MyAndromedaUserDataAccess : IMyAndromedaUserDataAccess
    {
        public bool ValidateUser(string username, string password)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from u in entitiesContext.MyAndromedaUsers
                                       where u.Username == username
                                         && u.Password == password
                                         && u.IsEnabled == true
                                       select u;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CanAccessStoreByExternalSiteId(string userName, string externalSiteId, out MyAndromedaDataAccess.Domain.MyAndromedaUser myAndromedaUser, out int siteId)
        {
            siteId = -1;
            myAndromedaUser = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                // Is the store in any of the groups that the user is associated with?
                var query = from u in entitiesContext.MyAndromedaUsers
                            join mug in entitiesContext.MyAndromedaUserGroups
                                on u.Id equals mug.MyAndromedaUserId
                            join g in entitiesContext.Groups
                                on mug.GroupId equals g.Id
                            join sg in entitiesContext.StoreGroups
                                on g.Id equals sg.GroupId
                            join s in entitiesContext.Stores
                                on sg.StoreId equals s.Id
                            where s.ExternalId == externalSiteId
                            && u.Username == userName
                            select u;

                MyAndromedaUser enitity = query.FirstOrDefault();

                if (enitity != null)
                {
                    // User is allowed to access this store
                    myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser()
                    {
                        Firstname = enitity.FirstName,
                        Surname = enitity.LastName
                    };

                    return true;
                }

                // Is the store associated with the user
                var query2 = from u in entitiesContext.MyAndromedaUsers
                            join mus in entitiesContext.MyAndromedaUserStores
                                on u.Id equals mus.MyAndromedaUserId
                            join s in entitiesContext.Stores
                                on mus.StoreId equals s.Id
                            where s.ExternalId == externalSiteId
                            && u.Username == userName
                            select new { u.FirstName, u.LastName, s.Id };

                var enitity2 = query2.FirstOrDefault();

                if (enitity2 != null)
                {
                    // User is allowed to access this store
                    myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser()
                    {
                        Firstname = enitity2.FirstName,
                        Surname = enitity2.LastName
                    };

                    // Return the store row id in case the caller needs to do db lookups 
                    // (faster than using the external store id)
                    siteId = enitity2.Id;

                    return true;
                }
            }

            // User is NOT allowed to access this store
            return false;
        }

        public string GetByUsername(string username, out MyAndromedaDataAccess.Domain.MyAndromedaUser myAndromedaUser)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                myAndromedaUser = null;

                var query = from u in entitiesContext.MyAndromedaUsers
                            where u.Username == username
                                && u.IsEnabled == true
                            select u;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    // Get the users sites
                    System.Collections.Generic.List<MyAndromedaDataAccess.Domain.Site> sites = null;
                    SitesDataAccess sitesDataAccess = new SitesDataAccess();
                    sitesDataAccess.GetByMyAndromedaUserId(entity.Id, out sites);

                    // Build an object that we can return to the caller
                    myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser()
                    {
                        Username = entity.Username,
                        Firstname = entity.FirstName,
                        Surname = entity.LastName,
                        Sites = sites
                    };
                }
            }

            return "";
        }
    }
}
