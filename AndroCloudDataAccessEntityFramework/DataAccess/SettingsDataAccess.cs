using System;
using System.Data;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SettingsDataAccess : ISettingsDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetByName(string name, out string value)
        {
            value = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var query = from p in acsEntities.Settings
                            where p.Name == name
                            select p;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    value = entity.Value;
                }
            }

            return "";
        }

        public string Update(string name, string value)
        {
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var query = from p in acsEntities.Settings
                            where p.Name == name
                            select p;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Value = value;
                    acsEntities.SaveChanges();
                }
            }

            return "";
        }
    }
}
