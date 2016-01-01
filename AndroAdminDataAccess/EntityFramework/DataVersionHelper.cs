using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AndroAdminDataAccess.EntityFramework
{
    public static class DataVersionHelper
    {
        public static int GetNextDataVersionForEntity(this AndroAdminEntities entitiesContext)
        {
            return GetNextDataVersion(entitiesContext);
        }

        public static int GetNextDataVersion(AndroAdminEntities entitiesContext)
        {
            // We need to do something a little unusual here.  All database changes are versioned and to do this we need a new version
            // number.  The problem is when two people make changes at exactly the same time.  We need to be careful to make sure
            // that they don't interfere with one another.  
            // User 1 needs to get a new version number and needs to tag all db changes with that version number.
            // If at any point while User 1 is doing this, User 2 could also make changes.  So, all changes must be done in a transaction.
            // But more than that - we have to make sure that if User 1 and User 2 both request a new version number one wins and
            // the other one waits.
            // To get a new version number we need to increment the current version number. But we can't get the version number
            // and then increment it because this isn't an atomic operation and if user 1 and user 2 do this at the same time
            // it can fail.  User 1 gets current version 5. User 2 gets current version 5.  User 1 increments version to 6, user 2 increments 
            // the version to 6 but it should be incremented to 7.  Both user 1 and 2 tag all their changes with version 6.
            // There doesn't seem to be a way to increment a number atomically in Entity Framework so I've had to fall back to SQL...

            // Note that the current database version is stored in the settings table.  The setting values are strings.

            // Get a SQL connection from EF
            SqlConnection sqlConnection = (SqlConnection)entitiesContext.Database.Connection;

            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();

            // We're gonna do this in a SQL command
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "UPDATE [Settings] SET [Value] = cast([Value] as int) + 1 output inserted.[Value] where [name] = 'dataversion'";

            // We're using an output clause in the SQL so we can do the update and get the result back all in one go
            int newVersion = -1;
            using (SqlDataReader sqlDataReader = command.ExecuteReader())
            {
                string newVersionText = "";

                if (sqlDataReader.Read())
                {
                    newVersionText = sqlDataReader.GetString(0);

                    Int32.TryParse(newVersionText, out newVersion);
                }
            }

            return newVersion;
        }

        public static int GetDataVersion(this AndroAdminEntities entitiesContext)
        {
            int newVersion = -1;
            var result = entitiesContext.Settings.Where(c => c.Name.Equals("DataVersion", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (result != null)
            {
                newVersion = Convert.ToInt32(result.Value);
            }
            return newVersion;
        }
    }
}
