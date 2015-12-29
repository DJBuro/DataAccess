using System;
using System.Linq;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace MyAndromedaDataAccessEntityFramework.DataAccess
{
    //public class MyAndromedaUserDataAccess : IMyAndromedaUserDataAccess
    //{
    //    public bool ValidateUser(string username, string password)
    //    {
    //        using (var entitiesContext = new AndroAdminDbContext())
    //        {
    //            var query = from u in entitiesContext.MyAndromedaUsers
    //                        where u.Username == username &&
    //                              u.Password == password &&
    //                              u.IsEnabled == true
    //                        select u;

    //            var entity = query.FirstOrDefault();

    //            if (entity != null)
    //            {
    //                return true;
    //            }
    //        }

    //        return false;
    //    }

    //    public bool CanAccessStoreByExternalSiteId(string userName, string externalSiteId, out MyAndromedaDataAccess.Domain.MyAndromedaUser myAndromedaUser, out int siteId)
    //    {
    //        siteId = -1;
    //        myAndromedaUser = null;

    //        using (var entitiesContext = new AndroAdminDbContext())
    //        {
    //            var query = entitiesContext.MyAndromedaUsers
    //                                       .Where(e => e.Username.Equals(userName))
    //                                       .Where(e => e.Chains.Any(chain => chain.Stores.Any(store => store.ExternalId == externalSiteId)));

    //            //.Where(user=> 
    //            //    //check if a record exists through user group 
    //            //    user.MyAndromedaUserGroups.Any(
    //            //        group => 
    //            //            //group -> store
    //            //            group.Group.Stores.Any(store => store.ExternalId == externalSiteId)
    //            //)
    //            //);
    //            var result = query.FirstOrDefault();
    //            MyAndromedaUser enitity = result;

    //            if (enitity != null)
    //            {
    //                // User is allowed to access this store
    //                myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser()
    //                {
    //                    Firstname = enitity.FirstName,
    //                    Surname = enitity.LastName
    //                };

    //                return true;
    //            }

    //            // Is the store associated with the user
    //            //var query2 = from u in entitiesContext.MyAndromedaUsers
    //            //             join mus in entitiesContext.MyAndromedaUsers on u.Id equals mus.MyAndromedaUserId
    //            //             join s in entitiesContext.Stores on mus.StoreId equals s.Id
    //            //             where s.ExternalId == externalSiteId &&
    //            //                   u.Username == userName
    //            //             select new { u.FirstName, u.LastName, s.Id };

    //            var query2 = entitiesContext.MyAndromedaUsers.Where(e => e.Stores.Any(store => store.ExternalId == externalSiteId))
    //                .Select(e => new { 
    //                    e.FirstName,
    //                    e.LastName,
    //                    e.Id
    //                });

    //            var enitity2 = query2.FirstOrDefault();

    //            if (enitity2 != null)
    //            {
    //                // User is allowed to access this store
    //                myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser()
    //                {
    //                    Firstname = enitity2.FirstName,
    //                    Surname = enitity2.LastName
    //                };

    //                // Return the store row id in case the caller needs to do db lookups 
    //                // (faster than using the external store id)
    //                siteId = enitity2.Id;

    //                return true;
    //            }
    //        }

    //        // User is NOT allowed to access this store
    //        return false;
    //    }

    //    public string GetByUsername(string username, out MyAndromedaDataAccess.Domain.MyAndromedaUser myAndromedaUser)
    //    {
    //        using (var entitiesContext = new AndroAdminDbContext())
    //        {
    //            myAndromedaUser = null;

    //            var query = from u in entitiesContext.MyAndromedaUsers
    //                        where u.Username == username &&
    //                              u.IsEnabled == true
    //                        select u;

    //            var entity = query.FirstOrDefault();

    //            if (entity != null)
    //            {
    //                // Get the users sites
    //                //List<MyAndromedaDataAccess.Domain.Site> sites = null;
    //                //MyAndromedaDataAccessEntityFramework.DataAccess.SitesDataAccess sitesDataAccess = new MyAndromedaDataAccessEntityFramework.DataAccess.SitesDataAccess();
    //                //sitesDataAccess.GetByMyAndromedaUserId(entity.Id, out sites);

    //                // Build an object that we can return to the caller
    //                myAndromedaUser = new MyAndromedaDataAccess.Domain.MyAndromedaUser()
    //                {
    //                    Id = entity.Id,
    //                    Username = entity.Username,
    //                    Firstname = entity.FirstName,
    //                    Surname = entity.LastName,
    //                    //Sites = sites
    //                };
    //            }
    //        }

    //        return "";
    //    }
    //}
}

