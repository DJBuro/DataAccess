using System;
using System.Linq;
using AndroCloudDataAccessEntityFramework.Model;
using System.Data.SqlClient;

namespace AndroCloudDataAccessEntityFramework
{
    public class DataVersionHelper
    {
        public static bool SetVersion(int fromVersion, int toVersion, ACSEntities entitiesContext)
        {
            // We need to do something a little unusual here.  This ACS server has a specific version of the ACS database (data not schema).
            // When there are changes to the master ACS database, the sync works out what data has changed between this ACS database version and the master db version.
            // However,  it's possible for multiple people to change the master db at the same time which could result in multiple simultanous data syncs.
            // To prevent data loss these syncs are done in transactions.  However, we still need to check that the sync is being applied to the correct version of the database.
            // We need to check that the database version matches the database version that the upgrade is for.
            // To do this we need to do something that doesn't appear to be possible in EF - "update where"

            // Note that the current database version is stored in the settings table.  The setting values are strings.

            // Get a SQL connection from EF
            SqlConnection sqlConnection = (SqlConnection)entitiesContext.Database.Connection;

            // We're gonna do this in a SQL command
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandText = "UPDATE [Settings] SET [Value] = '" + toVersion + "' where [name] = 'dataversion' and [Value] = '" + fromVersion + "'";

            // We need to check that the version was actually updated.  If someone else has sneaked in and done a sync then rowsAffected will be zero
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected != 1)
            {
                return false;
            }

            return true;
        }
    }
}
