using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class DataAccessHelper
    {
        public static void FixConnectionString(ACSEntities entitiesContext, string connectionString)
        {
            if (connectionString != null)
            {
                entitiesContext.Database.Connection.ConnectionString = connectionString;
            }
        }
    }
}
