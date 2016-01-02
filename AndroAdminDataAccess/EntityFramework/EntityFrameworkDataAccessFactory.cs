using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.DataAccess;
using AndroAdminDataAccess.EntityFramework.DataAccess;

namespace AndroAdminDataAccess.EntityFramework
{
    public class EntityFrameworkDataAccessFactory
    {
        public static string DBServer
        {
            get 
            { 
                string dbServer = "";

                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    dbServer = ((SqlConnection)entitiesContext.Database.Connection).DataSource;
                }

                return dbServer; 
            }
        }

        public static string DBName
        {
            get
            {
                string dbName = "";

                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    dbName = ((SqlConnection)entitiesContext.Database.Connection).Database;
                }

                return dbName;
            }
        }

        public IAMSServerDAO AMSServerDAO
        {
            get { return new AMSServerDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IFTPSiteDAO FTPSiteDAO
        {
            get { return new FtpSiteDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IFTPSiteTypeDAO FTPSiteTypeDAO
        {
            get { return new FtpSiteTypeDAO(); }
            set { throw new NotImplementedException(); }
        }

        public ILogDAO LogDAO
        {
            get { return new LogDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreAMSServerDAO StoreAMSServerDAO
        {
            get { return new StoreAMSServerDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreAMSServerFTPSiteDAO StoreAMSServerFTPSiteDAO
        {
            get { return new StoreAMSServerFtpSiteDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreDAO StoreDAO
        {
            get { return new StoreDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreDriverDAO StoreDriverDAO
        {
            get { return new StoreDriverDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IStoreGPSSettingsDAO StoreBringgSettingsDAO
        {
            get { return new StoreGPSSettingsDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IPartnerDAO PartnerDAO
        {
            get { return new PartnerDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IACSApplicationDAO ACSApplicationDAO
        {
            get { return new ACSApplicationDAO(); }
            set { throw new NotImplementedException(); }
        }

        public IHostDAO HostDAO
        {
            get { return new HostDAO(); }
            set { throw new NotImplementedException(); }
        }

        public ICountryDAO CountryDAO
        {
            get { return new CountryDAO(); }
            set { throw new NotImplementedException(); }
        }

        public ISettingsDAO SettingsDAO
        {
            get { return new SettingsDAO(); }
            set { throw new NotImplementedException(); }
        }
    }
}
