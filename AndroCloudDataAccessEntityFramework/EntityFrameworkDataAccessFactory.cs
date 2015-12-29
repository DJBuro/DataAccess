using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess;
using AndroCloudDataAccessEntityFramework.DataAccess;

namespace AndroCloudDataAccessEntityFramework
{
    public class EntityFrameworkDataAccessFactory : IDataAccessFactory
    {
        public string ConnectionStringOverride { get; set; }

        public AndroCloudDataAccess.DataAccess.IHostDataAccess HostDataAccess
        {
            get { return new HostDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.ISiteMenuDataAccess SiteMenuDataAccess
        {
            get { return new SiteMenuDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.ISiteDataAccess SiteDataAccess
        {
            get { return new SitesDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.ISiteDetailsDataAccess SiteDetailsDataAccess
        {
            get { return new SiteDetailsDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        //public AndroCloudDataAccess.DataAccess.IOrdersDataAccess OrderDataAccess
        //{
        //    get { return new OrderDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
        //    set { throw new NotImplementedException(); }
        //}

        public AndroCloudDataAccess.DataAccess.IAuditDataAccess AuditDataAccess
        {
            get { return new AuditDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        //public AndroCloudDataAccess.DataAccess.IOrderStatusDataAccess OrderStatusDataAccess
        //{
        //    get { return new OrderStatusDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
        //    set { throw new NotImplementedException(); }
        //}

        public AndroCloudDataAccess.DataAccess.IAddressDataAccess AddressDataAccess
        {
            get { return new AddressDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IACSApplicationDataAccess AcsApplicationDataAccess
        {
            get { return new ACSApplicationDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.ISettingsDataAccess SettingsDataAccess
        {
            get { return new SettingsDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IDeliveryAreaDataAccess DeliveryZoneDataAccess
        {
            get { return new DeliveryZoneDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }


        public AndroCloudDataAccess.DataAccess.IDeliveryAreaTownDataAccess DeliveryAreaTownDataAccess
        {
            get { return new DeliveryZoneTownsDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IDeliveryAreaRoadDataAccess DeliveryAreaRoadDataAccess
        {
            get { return new DeliveryZoneRoadsDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }
    }
}
