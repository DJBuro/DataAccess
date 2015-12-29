using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.DataAccess
{
    public interface ISecurityGroupDAO
    {
        string ConnectionStringOverride { get; set; }

        SecurityGroup GetById(int id);
        SecurityGroup GetByName(string name);
        List<SecurityGroup> GetAll();
        string Add(Domain.SecurityGroup securityGroup);
        string Update(Domain.SecurityGroup securityGroup);
    }
}
