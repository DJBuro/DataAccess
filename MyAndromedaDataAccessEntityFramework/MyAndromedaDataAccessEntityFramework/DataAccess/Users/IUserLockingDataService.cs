﻿using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserLockingDataService : IDependency
    {

        /// <summary>
        /// Creates or update restrictions.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="ipRanges">The ip ranges.</param>
        void CreateOrUpdateRestrictionsByUserId(int id, string ipRanges);

        /// <summary>
        /// Gets the lock by user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        UserIpLock GetLockByUserName(string userName);

        /// <summary>
        /// Gets the lock by user id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        UserIpLock GetLockByUserId(int id);

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<UserIpLock> List(Expression<Func<UserIpLock, bool>> query);
    }

    public class UserLockingDataService : IUserLockingDataService 
    {
        public UserLockingDataService() 
        {
        
        }

        public void CreateOrUpdateRestrictionsByUserId(int id, string ipRanges)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var user = dbContext.UserRecords
                    .Include(e => e.UserIpLock)
                    .Single(e=> e.Id == id);

                if (user.UserIpLock == null) { user.UserIpLock = new UserIpLock(); }
                
                user.UserIpLock.Enabled = true;
                user.UserIpLock.ValidIpV4Ranges = ipRanges;

                dbContext.SaveChanges();
            }
        }

        public UserIpLock GetLockByUserName(string userName)
        {
            return this.List(e => e.UserRecord.Username == userName).SingleOrDefault();
        }

        public UserIpLock GetLockByUserId(int id)
        {
            var result = this.List(e => e.UserId == id).SingleOrDefault();

            if (result != null)
                return result;

            this.CreateOrUpdateRestrictionsByUserId(id, string.Empty);

            return new UserIpLock() 
            { 
                UserId = id,
                Enabled = true,
                ValidIpV4Ranges = string.Empty
            };
        }

        public IEnumerable<UserIpLock> List(Expression<Func<UserIpLock, bool>> query)
        {
            IEnumerable<UserIpLock> results = Enumerable.Empty<UserIpLock>();

            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.UserIpLocks;
                var tableQuery = table.Where(query);

                results = tableQuery.ToArray();
            }

            return results;
        }
    }
}
