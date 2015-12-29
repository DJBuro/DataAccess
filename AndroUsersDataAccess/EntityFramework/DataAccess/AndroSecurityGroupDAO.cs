using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.DataAccess;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.EntityFramework.DataAccess
{
    public class AndroSecurityGroupDAO : IAndroSecurityGroupDAO
    {
        public string ConnectionStringOverride { get; set; }

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
                        Name = entity.Name
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
    }
}
