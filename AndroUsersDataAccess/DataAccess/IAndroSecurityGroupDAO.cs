using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.DataAccess
{
    public interface IAndroSecurityGroupDAO
    {
        string ConnectionStringOverride { get; set; }

        List<SecurityGroup> GetAll();
        string Add(Domain.SecurityGroup securityGroup);
    }
}
