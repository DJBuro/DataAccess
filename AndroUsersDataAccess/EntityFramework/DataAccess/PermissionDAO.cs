using System;
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
    }
}
