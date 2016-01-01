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
                        Permissions = null
                    };
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
                        Permissions = null
                    };

                    //var permissionQuery = from s in entitiesContext.SecurityGroupPermissions
                    //                      .Include("Permission")
                    //                      where s.SecurityGroupId == securityGroup.Id
                    //                      select s;

                    //foreach (SecurityGroupPermission securityGroupPermission in permissionQuery.Perm)
                    //{
                    //  //  securityGroupPermission.
                    //}

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
                    Name = securityGroup.Name
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
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = securityGroup.Name;
                };

                androUsersEntities.SaveChanges();

                return "";
            }
        }
    }
}
