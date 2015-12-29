using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.DataAccess
{
    public interface IAndroUserDAO
    {
        AndroUser GetByEmailAddress(string emailAddress);
    }
}
