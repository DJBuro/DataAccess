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
        public AndroCloudDataAccess.DataAccess.ISiteMenuDataAccess SiteMenuDataAccess
        {
            get { return new SiteMenuDataAccess(); }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.ISiteDataAccess SiteDataAccess
        {
            get { return new SitesDataAccess(); }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.ISiteDetailsDataAccess SiteDetailsDataAccess
        {
            get { return new SiteDetailsDataAccess(); }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IOrdersDataAccess OrderDataAccess
        {
            get { return new OrderDataAccess(); }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IAuditDataAccess AuditDataAccess
        {
            get { return new AuditDataAccess(); }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IPartnersDataAccess PartnerDataAccess
        {
            get { return new PartnersDataAccess(); }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IGroupsDataAccess GroupDataAccess
        {
            get { return new GroupsDataAccess(); }
            set { throw new NotImplementedException(); }
        }

        public AndroCloudDataAccess.DataAccess.IOrderStatusDataAccess OrderStatusDataAccess
        {
            get { return new OrderStatusDataAccess(); }
            set { throw new NotImplementedException(); }
        }


        public AndroCloudDataAccess.DataAccess.IMyAndromedaUserDataAccess MyAndromedaUserDataAccess
        {
            get { return new MyAndromedaUserDataAccess(); }
            set { throw new NotImplementedException(); }
        }


        public AndroCloudDataAccess.DataAccess.IAddressDataAccess AddressDataAccess
        {
            get { return new AddressDataAccess(); }
            set { throw new NotImplementedException(); }
        }
    }
}
