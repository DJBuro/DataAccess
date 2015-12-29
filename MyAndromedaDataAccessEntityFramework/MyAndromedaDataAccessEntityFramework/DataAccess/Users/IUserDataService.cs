using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromeda.Core;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserDataService : IDependency
    {
        Model.MyAndromeda.UserRecord New();

        void Add(UserRecord user);
        void Update(UserRecord user);

        MyAndromedaDataAccess.Domain.MyAndromedaUser GetByUserName(string userName);

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

        public MyAndromedaDataAccess.Domain.MyAndromedaUser GetByUserName(string userName)
        {
            MyAndromedaDataAccess.Domain.MyAndromedaUser model = null;
            using (var dbContext = NewContext()) 
            {
                var result = dbContext.UserRecords.Where(e => e.Username.Equals(userName)).SingleOrDefault();

                if (result == null)
                    return null;

                model = result.ToDomain();
            }

            return model;
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
            }
        }

    }

    public static class UserDataServiceExtensions 
    {
        public static MyAndromedaDataAccess.Domain.MyAndromedaUser ToDomain(this UserRecord user) 
        {
            var domainModel = new MyAndromedaDataAccess.Domain.MyAndromedaUser() { 
                Id = user.Id,
                Firstname = user.FirstName,
                Surname = user.LastName,
                Username = user.Username
            };

            return domainModel;
        }
    }
}
