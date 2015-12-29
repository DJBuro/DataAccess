using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudWCFHelper;
using AndroCloudHelper;
using CloudSyncModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Reflection;
namespace AndroCloudDataAccessEntityFramework
{
    public class DataVersionHelper
    {
        public static bool SetVersion(int fromVersion, int toVersion, ACSEntities entitiesContext, DbTransaction transaction)
        {
            // We need to do something a little unusual here.  This ACS server has a specific version of the ACS database (data not schema).
            // When there are changes to the master ACS database, the sync works out what data has changed between this ACS database version and the master db version.
            // However,  it's possible for multiple people to change the master db at the same time which could result in multiple simultanous data syncs.
            // To prevent data loss these syncs are done in transactions.  However, we still need to check that the sync is being applied to the correct version of the database.
            // We need to check that the database version matches the database version that the upgrade is for.
            // To do this we need to do something that doesn't appear to be possible in EF - "update where"

            // Note that the current database version is stored in the settings table.  The setting values are strings.

            // Get a SQL connection from EF
            SqlConnection sqlConnection = (SqlConnection)((EntityConnection)entitiesContext.Connection).StoreConnection;

            // Get a SQL transaction from EF
            SqlTransaction sqlTransaction = (SqlTransaction)transaction.GetType().InvokeMember(
                "StoreTransaction",
                BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.NonPublic,
                null,
                transaction,
                new object[0]);

            // We're gonna do this in a SQL command
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.Transaction = sqlTransaction;
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
