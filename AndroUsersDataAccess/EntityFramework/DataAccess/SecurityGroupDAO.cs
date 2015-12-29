using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.DataAccess;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.EntityFramework.DataAccess
{
    public class SecurityGroupDAO : ISecurityGroupDAO
    {
        public string ConnectionStringOverride { get; set; }

        public Domain.SecurityGroup GetById(int id)
        {
            Domain.SecurityGroup securityGroup = null;

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                // Get the security group
                var query = from s in entitiesContext.SecurityGroups
                            where s.Id == id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    securityGroup = new Domain.SecurityGroup()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        Permissions = new List<Domain.Permission>()
                    };
                }
            }

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                // Is this the admin security group?  If so, we need to return ALL permissions
                if (securityGroup.Name == Domain.SecurityGroup.AdministratorSecurityGroup)
                {
                    // Get all permissions
                    var permissionsQuery = from s in entitiesContext.Permissions
                                           select s;

                    foreach (var permissionEntity in permissionsQuery)
                    {
                        securityGroup.Permissions.Add
                        (
                            new Domain.Permission()
                            {
                                Id = permissionEntity.Id,
                                Name = permissionEntity.Name,
                                Description = permissionEntity.Description
                            }
                        );
                    }
                }
                else 
                {
                    // Get the permissions for the security group
                    var permissionsQuery = from s in entitiesContext.SecurityGroupPermissions.Include("Permission")
                                           where s.SecurityGroupId == id
                                           select s;

                    foreach (var permissionEntity in permissionsQuery)
                    {
                        securityGroup.Permissions.Add
                        (
                            new Domain.Permission()
                            {
                                Id = permissionEntity.Permission.Id,
                                Name = permissionEntity.Permission.Name,
                                Description = permissionEntity.Permission.Description
                            }
                        );
                    }
                }
            }

            return securityGroup;
        }

        public Domain.SecurityGroup GetByName(string name)
        {
            Domain.SecurityGroup securityGroup = null;

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.SecurityGroups
                            where s.Name == name
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    securityGroup = new Domain.SecurityGroup()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        Permissions = null
                    };
                }
            }

            return securityGroup;
        }

        public List<Domain.SecurityGroup> GetAll()
        {
            List<Domain.SecurityGroup> securityGroups = new List<Domain.SecurityGroup>();

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.SecurityGroups
                            select s;

                foreach (var entity in query)
                {
                    Domain.SecurityGroup securityGroup = new Domain.SecurityGroup()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        Permissions = null
                    };

                    securityGroups.Add(securityGroup);
                }
            }

            return securityGroups;
        }

        public string Add(Domain.SecurityGroup securityGroup)
        {
            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

                SecurityGroup securityGroupEntity = new SecurityGroup()
                {
                    Name = securityGroup.Name,
                    Description = securityGroup.Description
                };

                androUsersEntities.SecurityGroups.Add(securityGroupEntity);
                androUsersEntities.SaveChanges();

                return "";
            }
        }

        public string Update(Domain.SecurityGroup securityGroup)
        {
            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

                var query = from s in androUsersEntities.SecurityGroups
                            where s.Id == securityGroup.Id
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = securityGroup.Name;
                    entity.Description = securityGroup.Description;
                };

                androUsersEntities.SaveChanges();

                return "";
            }
        }

        public string AddPermission(int securityGroupId, int permissionId)
        {
            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

                SecurityGroupPermission securityGroupPermission = new SecurityGroupPermission()
                {
                    PermissionId = permissionId,
                    SecurityGroupId = securityGroupId
                };

                androUsersEntities.SecurityGroupPermissions.Add(securityGroupPermission);

                androUsersEntities.SaveChanges();

                return "";
            }
        }

        public string RemovePermission(int securityGroupId, int permissionId)
        {
            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

                var query = from s in androUsersEntities.SecurityGroupPermissions
                            where s.SecurityGroupId == securityGroupId
                            && s.PermissionId == permissionId
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    androUsersEntities.SecurityGroupPermissions.Remove(entity);
                };

                androUsersEntities.SaveChanges();

                return "";
            }
        }

        public string AddUser(int securityGroupId, int userId)
        {
            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

                SecurityGroupUser securityGroupUser = new SecurityGroupUser()
                {
                    UserId = userId,
                    SecurityGroupId = securityGroupId
                };

                androUsersEntities.SecurityGroupUsers.Add(securityGroupUser);

                androUsersEntities.SaveChanges();

                return "";
            }
        }

        public string RemoveUser(int securityGroupId, int userId)
        {
            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

                var query = from s in androUsersEntities.SecurityGroupUsers
                            where s.SecurityGroupId == securityGroupId
                            && s.UserId == userId
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    androUsersEntities.SecurityGroupUsers.Remove(entity);
                };

                androUsersEntities.SaveChanges();

                return "";
            }
        }
    }
}
