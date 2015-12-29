using System;
using System.Linq;
using System.Collections.Generic;
using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserSitesDataService : IDependency 
    {
        /// <summary>
        /// Gets the sites for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IEnumerable<Site> GetSitesForUser(int userId);

        /// <summary>
        /// Gets the sites for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        IEnumerable<Site> GetSitesForUser(int userId, int chainId);
    }

    public class UserSitesDataService : IUserSitesDataService
    {
        public IEnumerable<Site> GetSitesForUser(int userId)
        {
            IEnumerable<Site> sites;
            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContext.Stores;
                var query = table.Where(e => e.MyAndromedaUsers.Any(user => user.Id == userId));

                var results = query.ToArray();
                sites = results.Select(e => e.ToDomain()).ToArray();
            }

            return sites;
        }

        public IEnumerable<Site> GetSitesForUser(int userId, int chainId)
        {
            IEnumerable<Site> sites;
            using (var dbContect = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var table = dbContect.Stores;
                var query = table.Where(e => e.Chain.MyAndromedaUsers.Any(user => user.Id == userId));
                
                var results = query.ToArray();
                sites = results.Select(e => e.ToDomain()).ToArray();
            }

            return sites;
        }
    }

}