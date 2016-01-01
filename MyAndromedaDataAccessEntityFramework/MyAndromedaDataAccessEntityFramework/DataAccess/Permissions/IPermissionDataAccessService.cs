using MyAndromeda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromeda.Core.Authorizatin;
using MyAndromeda.Core.Site;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Permissions
{
    public interface IPermissionDataAccessService : IDependency
    {
        

        void UpdateRole(IUserRole role, IEnumerable<IPermission> permissions);
        
        void UpdateEnrolementPermissions(MyAndromeda.Core.Site.IEnrolmentLevel enrolementLevel, IEnumerable<IPermission> permissions);

        IEnumerable<IPermission> GetEffectivePermissions(int userId);
        IEnumerable<IPermission> GetEffectivePermissions(IUserRole role);
        IEnumerable<IPermission> GetEffectivePermissions(IEnrolmentLevel role);
        IEnumerable<IPermission> GetEffectivePermissions(ISite site);
    }

    public class PermissionDataAccessService : IPermissionDataAccessService 
    {
        public PermissionDataAccessService() 
        {
        }

        public void UpdateRole(IUserRole role, IEnumerable<IPermission> permissions)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var roleTable = dbContext.Roles;
                var roleEntity = roleTable.SingleOrDefault(e => e.Id == role.Id);
                roleEntity.Permissions.Clear();

                this.AddPermissionsToCollection(dbContext, roleEntity.Permissions, permissions);
                
                dbContext.SaveChanges();
            }
        }

        public void UpdateEnrolementPermissions(IEnrolmentLevel enrolementLevel, IEnumerable<IPermission> permissions)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var enrolementTable = dbContext.EnrolmentLevels;
                var enrolmentEntity = 
                    enrolementLevel.Id == 0 ? 
                        enrolementTable.SingleOrDefault(e=> e.Name == enrolementLevel.Name) : 
                        enrolementTable.SingleOrDefault(e=> e.Id == enrolementLevel.Id);

                this.AddPermissionsToCollection(dbContext, enrolmentEntity.Permissions, permissions);
            }
        }

        public IEnumerable<IPermission> GetEffectivePermissions(int userId)
        {
            IEnumerable<IPermission> permissions;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var permissionsTable = dbContext.Permissions;
                var query = permissionsTable
                        .Where(e => e.Roles.Any(userRole => userRole.UserRecords.Any(userRecord => userRecord.Id == userId)));

                var result = query.ToArray();
                permissions = result.Select(e => e as IPermission).ToArray();
            }

            return permissions;
        }

        public IEnumerable<IPermission> GetEffectivePermissions(IUserRole userRole)
        {
            IEnumerable<IPermission> permissions;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var permissionsTable = dbContext.Permissions;
                var query = userRole.Id == 0 ?
                    permissionsTable.Where(e => e.Roles.Any(role => role.Name == userRole.Name)) :
                    permissionsTable.Where(e => e.Roles.Any(role => role.Id == userRole.Id));

                var result = query.ToArray();
                permissions = result.Select(e => e as IPermission).ToArray();
            }

            return permissions;
        }

        public IEnumerable<IPermission> GetEffectivePermissions(IEnrolmentLevel role)
        {
            IEnumerable<IPermission> permissions;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var permissionsTable = dbContext.Permissions;
                var query = role.Id == 0 ?
                    permissionsTable.Where(e => e.EnrolmentLevels.Any(level => level.Name == role.Name)) :
                    permissionsTable.Where(e => e.EnrolmentLevels.Any(level => level.Id == role.Id));

                var results = query.ToArray();
                permissions = results.Select(e => e as IPermission).ToArray();
            }

            return permissions;
        }

        public IEnumerable<IPermission> GetEffectivePermissions(ISite site)
        {
            IEnumerable<IPermission> permissions;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var permissionsTable = dbContext.Permissions;
                var query = permissionsTable.Where(e => e.EnrolmentLevels.Any(level => level.StoreEnrolments.Any(enrolement => enrolement.StoreId == site.Id)));
                var results = query.ToArray();
                
                permissions = results.Select(e => e as IPermission).ToArray();
            }

            return permissions;
        }

        private void AddPermissionsToCollection(
            MyAndromedaDataAccessEntityFramework.Model.MyAndromeda.MyAndromedaDbContext dbContext,
            ICollection<MyAndromedaDataAccessEntityFramework.Model.MyAndromeda.Permission> collection,
            IEnumerable<IPermission> permissions)
        {
            var permissionTable = dbContext.Permissions;

            foreach (var permission in permissions)
            {
                var permissionEntity = permissionTable.SingleOrDefault(e => e.Name == permission.Name && e.Category == permission.Category);
                if (permissionEntity == null)
                {
                    permissionEntity = new Model.MyAndromeda.Permission()
                    {
                        Name = permission.Name,
                        Description = permission.Description,
                        Category = permission.Category
                    };
                }

                collection.Add(permissionEntity);
            }
        }

        
    }
}
