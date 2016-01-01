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

        /// <summary>
        /// Gets the specified role by id.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns></returns>
        IUserRole Get(int roleId);

        /// <summary>
        /// Gets the specified role by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IUserRole Get(string name);

        /// <summary>
        /// Lists all available user roles.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IUserRole> List();

        /// <summary>
        /// Lists the roles for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IEnumerable<IUserRole> ListRolesForUser(int userId);

        /// <summary>
        /// Adds the roles to user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="userRoles">The user roles.</param>
        void AddRolesToUser(int userId, IEnumerable<IUserRole> userRoles);
    }
}
