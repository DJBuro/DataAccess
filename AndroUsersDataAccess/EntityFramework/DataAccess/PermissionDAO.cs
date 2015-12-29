﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.DataAccess;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.EntityFramework.DataAccess
{
    public class PermissionDAO : IPermissionDAO
    {
        public string ConnectionStringOverride { get; set; }

        /// <summary>
        /// Gets a list of ALL permissions
        /// </summary>
        /// <returns></returns>
        public List<Domain.Permission> GetAll()
        {
            List<Domain.Permission> permissions = new List<Domain.Permission>();

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Permissions
                            orderby s.DisplayOrder
                            select s;

                foreach (var entity in query)
                {
                    Domain.Permission permission = new Domain.Permission()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description
                    };

                    permissions.Add(permission);
                }
            }

            return permissions;
        }

        /// <summary>
        /// Gets a list of ALL permissions that the specfied user has.  
        /// This is a union of permissions in all security groups that the user belongs to.
        /// Return ONLY the permission names.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<string> GetNamesByUserName(string username)
        {
            List<string> permissions = new List<string>();

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Permissions
                            join sgp in entitiesContext.SecurityGroupPermissions
                            on s.Id equals sgp.PermissionId
                            join sgu in entitiesContext.SecurityGroupUsers
                            on sgp.SecurityGroupId equals sgu.SecurityGroupId
                            join u in entitiesContext.tbl_AndroUser
                            on sgu.UserId equals u.id
                            where u.EmailAddress == username
                            select s;

                foreach (var entity in query)
                {
                    permissions.Add(entity.Name);
                }
            }

            return permissions;
        }

        /// <summary>
        /// Returns true if the user has the specified permission.
        /// The permission can be in one or more of the security groups that the user belongs to.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        public bool UserHasPermission(string username, string permissionName)
        {
            List<string> permissions = new List<string>();

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Permissions
                            join sgp in entitiesContext.SecurityGroupPermissions
                            on s.Id equals sgp.PermissionId
                            join sgu in entitiesContext.SecurityGroupUsers
                            on sgp.SecurityGroupId equals sgu.SecurityGroupId
                            join u in entitiesContext.tbl_AndroUser
                            on sgu.UserId equals u.id
                            where u.EmailAddress == username
                            && s.Name == permissionName
                            select s;

                if (query.Count() >= 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
