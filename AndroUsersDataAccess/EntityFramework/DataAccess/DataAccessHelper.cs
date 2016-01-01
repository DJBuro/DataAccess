using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroUsersDataAccess.EntityFramework.DataAccess
{
    public class DataAccessHelper
    {
        public static void FixConnectionString(AndroUsersEntities entitiesContext, string connectionString)
        {
            if (connectionString != null)
            {
                entitiesContext.Database.Connection.ConnectionString = connectionString;
            }
        }
    }
}
