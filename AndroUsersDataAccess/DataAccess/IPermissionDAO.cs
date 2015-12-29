using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.DataAccess
{
    public interface IPermissionDAO
    {
        string ConnectionStringOverride { get; set; }
        List<Permission> GetAll();
    }
}