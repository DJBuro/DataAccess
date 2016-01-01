using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromeda.Core;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using MyAndromeda.Core.User;
using System.Data.Entity;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserDataService : IDependency
    {
        /// <summary>
        /// returns a new instance of a database model
        /// </summary>
        /// <returns></returns>
        Model.MyAndromeda.UserRecord New();

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        void Add(UserRecord user);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        void Update(UserRecord user);

        /// <summary>
        /// Gets the name of the by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        MyAndromedaUser GetByUserName(string userName);
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserRecord GetByUserId(int userId);
        /// <summary>
        /// Queries for users.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<MyAndromedaUser> QueryForUsers(Expression<Func<UserRecord, bool>> query);

        /// <summary>
        /// Queries the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<UserRecord> Query(Expression<Func<UserRecord, bool>> query);
    }

    public class UserDataService : IUserDataService
    {
        public UserDataService() 
        {
        }

        public IEnumerable<UserRecord> Query(Expression<Func<UserRecord, bool>> query)
        {
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.UserRecords;
                var tableQuery = table.Where(query);

                return tableQuery.ToArray();
            }
        }

        private MyAndromedaDbContext NewContext() 
        {
            return new Model.MyAndromeda.MyAndromedaDbContext();
        }

        public UserRecord New()
        {
            return new UserRecord();
        }

        public MyAndromedaUser GetByUserName(string userName)
        {
            MyAndromedaUser model = null;
            using (var dbContext = NewContext()) 
            {
                var result = dbContext.UserRecords
                    //.Include(e=> e.UserIpLocking)
                    .Where(e => e.Username.Equals(userName))
                    .SingleOrDefault();

                if (result == null)
                    return null;

                model = result.ToDomain();
            }

            return model;
        }

        public UserRecord GetByUserId(int userId)
        {
            UserRecord model = null;
            using (var dbContext = NewContext())
            {
                var result = dbContext.UserRecords
                    //.Include(e=> e.UserIpLocking)
                    .Where(e => e.Id.Equals(userId))
                    .SingleOrDefault();

                if (result == null)
                    return null;

                model = result;
            }

            return model;
        }

        public IEnumerable<MyAndromedaUser> QueryForUsers(Expression<Func<UserRecord, bool>> query)
        {
            IEnumerable<MyAndromedaUser> results;
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.UserRecords;
                var userQuery = table.Where(query);
                var result = userQuery.ToArray();

                results = result.Select(e => e.ToDomain());
            }

            return results;
        }

        public void Add(UserRecord user)
        {
            using (var dbContext = NewContext())
            {
                var table = dbContext.UserRecords;
                table.Add(user);

                dbContext.SaveChanges();
            }
        }

        public void Update(UserRecord user)
        {
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.UserRecords;
                var entity = table.Where(e => e.Id == user.Id).Single();

                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.IsEnabled = user.IsEnabled;
                entity.Password = user.Password;
                entity.PasswordSalt = user.PasswordSalt;
                entity.PasswordFormat = user.PasswordFormat;
                entity.HashAlgorithm = user.HashAlgorithm;

                dbContext.SaveChanges();
            }
        }
    }

    public static class UserDataServiceExtensions 
    {
        public static MyAndromedaUser ToDomain(this UserRecord user) 
        {
            var domainModel = new MyAndromedaUser() { 
                Id = user.Id,
                Firstname = user.FirstName,
                Surname = user.LastName,
                Username = user.Username
            };

            return domainModel;
        }
    }
}
