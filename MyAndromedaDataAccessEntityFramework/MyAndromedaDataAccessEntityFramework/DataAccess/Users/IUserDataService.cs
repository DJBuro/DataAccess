using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserDataService : IDependency
    {
        Model.AndroAdmin.MyAndromedaUser New();

        void Add(Model.AndroAdmin.MyAndromedaUser user);
        void Update(Model.AndroAdmin.MyAndromedaUser user);

        MyAndromedaDataAccess.Domain.MyAndromedaUser GetByUserName(string userName);

        IEnumerable<Model.AndroAdmin.MyAndromedaUser> Query(Expression<Func<Model.AndroAdmin.MyAndromedaUser, bool>> query);
    }

    public class UserDataService : IUserDataService
    {
        public UserDataService() 
        {
        }

        

        public IEnumerable<MyAndromedaUser> Query(Expression<Func<MyAndromedaUser, bool>> query)
        {
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.MyAndromedaUsers;
                var tableQuery = table.Where(query);

                return tableQuery.ToArray();
            }
        }

        private AndroAdminDbContext NewContext() 
        {
            return new AndroAdminDbContext();
        }

        public MyAndromedaUser New()
        {
            return new MyAndromedaUser();
        }

        public MyAndromedaDataAccess.Domain.MyAndromedaUser GetByUserName(string userName)
        {
            MyAndromedaDataAccess.Domain.MyAndromedaUser model = null;
            using (var dbContext = NewContext()) 
            {
                var result = dbContext.MyAndromedaUsers.Where(e => e.Username.Equals(userName)).SingleOrDefault();

                model = result.ToDomain();
            }

            return model;
        }

        public void Add(MyAndromedaUser user)
        {
            using (var dbContext = NewContext())
            {
                var table = dbContext.MyAndromedaUsers;
                table.Add(user);

                dbContext.SaveChanges();
            }
        }

        public void Update(MyAndromedaUser user)
        {
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.MyAndromedaUsers;
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
        public static MyAndromedaDataAccess.Domain.MyAndromedaUser ToDomain(this Model.AndroAdmin.MyAndromedaUser user) 
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
