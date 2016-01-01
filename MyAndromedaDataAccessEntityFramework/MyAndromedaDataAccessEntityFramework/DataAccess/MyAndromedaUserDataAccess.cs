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
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var myAndromedaQuery = from u in androAdminEntities.MyAndromedaUsers
                                   where u.Username == username
                                     && u.Password == password
                                     && u.IsEnabled == true
                                   select u;

            var myAndromedaEntity = myAndromedaQuery.FirstOrDefault();

            if (myAndromedaEntity != null)
            {
                return true;
            }

            return false;
        }

        public bool CanAccessStoreByExternalStoreId(string userName, string externalStoreId, out MyAndromedaDataAccess.Domain.MyAndromedaUser myAndromedaUser, out int siteId)
        {
            siteId = -1;
            myAndromedaUser = null;
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            // Is the store in any of the groups that the user is associated with?
            var myAndromedaQuery = from u in androAdminEntities.MyAndromedaUsers
                                   join mug in androAdminEntities.MyAndromedaUserGroups
                                     on u.Id equals mug.MyAndromedaUserId
                                   join g in androAdminEntities.Groups
                                     on mug.GroupId equals g.Id
                                   join sg in androAdminEntities.StoreGroups
                                     on g.Id equals sg.GroupId
                                   join s in androAdminEntities.Stores
                                     on sg.StoreId equals s.Id
                                   where s.ExternalId == externalStoreId
                                   && u.Username == userName
                                   select u;

            MyAndromedaUser myAndromedaEnitity = myAndromedaQuery.FirstOrDefault();

            if (myAndromedaEnitity != null)
            {
                // User is allowed to access this store
                myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser();
                myAndromedaUser.Firstname = myAndromedaEnitity.FirstName;
                myAndromedaUser.Surname = myAndromedaEnitity.LastName;

                return true;
            }

            // Is the store assocaited with the user
            var myAndromedaQuery2 = from u in androAdminEntities.MyAndromedaUsers
                                    join mus in androAdminEntities.MyAndromedaUserStores
                                      on u.Id equals mus.MyAndromedaUserId
                                    join s in androAdminEntities.Stores
                                      on mus.StoreId equals s.Id
                                    where s.ExternalId == externalStoreId
                                    && u.Username == userName
                                    select new { u.FirstName, u.LastName, s.Id };

            var myAndromedaEnitity2 = myAndromedaQuery2.FirstOrDefault();

            if (myAndromedaEnitity2 != null)
            {
                // User is allowed to access this store
                myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser();
                myAndromedaUser.Firstname = myAndromedaEnitity2.FirstName;
                myAndromedaUser.Surname = myAndromedaEnitity2.LastName;

                // Return the store row id in case the caller needs to do db lookups 
                // (faster than using the external store id)
                siteId = myAndromedaEnitity2.Id;

                return true;
            }

            // User is NOT allowed to access this store
            return false;
        }

        public string GetByUsername(string username, out MyAndromedaDataAccess.Domain.MyAndromedaUser myAndromedaUser)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            myAndromedaUser = null;

            var myAndromedaQuery = from u in androAdminEntities.MyAndromedaUsers
                                   where u.Username == username
                                     && u.IsEnabled == true
                                   select u;

            var myAndromedaEntity = myAndromedaQuery.FirstOrDefault();

            if (myAndromedaEntity != null)
            {
                // Get the users sites
                System.Collections.Generic.List<MyAndromedaDataAccess.Domain.Site> sites = null;
                SitesDataAccess sitesDataAccess = new SitesDataAccess();
                sitesDataAccess.GetByMyAndromedaUserId(myAndromedaEntity.Id, out sites);

                // Build an object that we can return to the caller
                myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser();

                myAndromedaUser.Username = myAndromedaEntity.Username;
                myAndromedaUser.Firstname = myAndromedaEntity.FirstName;
                myAndromedaUser.Surname = myAndromedaEntity.LastName;
                myAndromedaUser.Sites = sites;
            }

            return "";
        }
    }
}
