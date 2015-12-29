using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromeda.Core;
using MyAndromeda.Core.Authorization;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserRoleDataService : IDependency
    {
        void CreateORUpdate(IUserRole viewModel);

        IUserRole Get(int id);
        IUserRole Get(string name);

        IEnumerable<IUserRole> List();
    }

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

        public IUserRole Get(int id)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = dbContext.Roles;
                Role entity = table.Single(e => e.Id == id);

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

        public void Create(IUserRole userRole)
        {
            
        }
    }
}
