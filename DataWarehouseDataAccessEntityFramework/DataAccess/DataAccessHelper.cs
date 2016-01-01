using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccessEntityFramework.Model;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class DataAccessHelper
    {
        public static void FixConnectionString(DataWarehouseEntities dataWarehouseEntities, string connectionString)
        {
            if (connectionString != null)
            {
                dataWarehouseEntities.Database.Connection.ConnectionString = connectionString;
            }
        }
    }
}
