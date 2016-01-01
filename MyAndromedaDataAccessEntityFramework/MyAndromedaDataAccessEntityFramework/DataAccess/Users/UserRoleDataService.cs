using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromeda.Core.Authorization;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public class UserRoleDataService : IUserRoleDataService 
    {
        public UserRoleDataService() 
        {
        }
          
        public void CreateORUpdate(IUserRole userRole)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = dbContext.Roles;
                Role entity;
                entity = table.FirstOrDefault(e => e.Name == userRole.Name) ?? table.Create();
                  
                entity.Name = userRole.Name;

                table.Add(entity);
                userRole.Id = entity.Id;
            }
        }

        public IUserRole Get(int roleId)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = dbContext.Roles;
                Role entity = table.Single(e => e.Id == roleId);

                return entity;
            }
        }

        public IUserRole Get(string name)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = dbContext.Roles;
                Role entity = table.Single(e => e.Name == name);

                return entity;
            }
        }

        public IEnumerable<IUserRole> List()
        {
            IEnumerable<IUserRole> items;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.Roles;

                items = table.ToList();
            }

            return items;
        }

        public IEnumerable<IUserRole> ListRolesForUser(int userId)
        {
            IEnumerable<IUserRole> results;

            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.Roles;
                var query = table.Where(e => e.UserRecords.Any(user => user.Id == userId));
                results = query.ToArray();
            }

            return results;
        }

        public void AddRolesToUser(int userId, IEnumerable<IUserRole> userRoles)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.Roles;
                var allRoles = table.ToArray();
                var user = dbContext.UserRecords.Single(e=> e.Id == userId);

                foreach (var userRole in userRoles) 
                {
                    var role = allRoles.Single(e => e.Id == userRole.Id);
                    role.UserRecords.Add(user);
                }

                dbContext.SaveChanges();
            }
        }
    }
}