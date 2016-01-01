using System;
using System.Linq;
using MyAndromedaDataAccess;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccess.DataAccess.MyAndromeda.Email;
using MyAndromedaDataAccessEntityFramework.DataAccess;

namespace MyAndromedaDataAccessEntityFramework
{
    public class EntityFrameworkDataAccessFactory : IDataAccessFactory
    {
        public MyAndromedaDataAccess.DataAccess.ISiteDataAccess SiteDataAccess
        {
            get
            {
                return new SitesDataAccess();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //public MyAndromedaDataAccess.DataAccess.IMyAndromedaUserDataAccess MyAndromedaUserDataAccess
        //{
        //    get
        //    {
        //        return new MyAndromedaUserDataAccess();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public MyAndromedaDataAccess.DataAccess.IAddressDataAccess AddressDataAccess
        {
            get
            {
                return new AddressDataAccess();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        //public MyAndromedaDataAccess.DataAccess.IEmployeeDataAccess EmployeeDataAccess
        //{
        //    get
        //    {
        //        return new EmployeeDataAccess();
        //    }
        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        public MyAndromedaDataAccess.DataAccess.IOpeningHoursDataAccess OpeningHoursDataAccess
        {
            get
            {
                return new OpeningHoursDataAccess();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MyAndromedaDataAccess.DataAccess.ISiteDetailsDataAccess SiteDetailsDataAccess
        {
            get
            {
                return new SiteDetailsDataAccess();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MyAndromedaDataAccess.DataAccess.ICountryDataAccess CountryDataAccess
        {
            get
            {
                return new CountryDataAccess();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public MyAndromedaDataAccess.DataAccess.ICustomerDataAccess CustomerDataAccess
        {
            get 
            {
                return new DataAccess.Marketing.CustomerDataAccess();
            }
        }

        public MyAndromedaDataAccess.DataAccess.MyAndromeda.Email.IEmailCampaignDataAccess EmailCampaignDataAccess
        { 
            get 
            {
                return new DataAccess.Marketing.EmailCampaignDataAccess();
            }
        }

        public IEmailCampaignTasksDataAccess EmailCampaignTasksDataAccess
        {
            get
            {
                return new DataAccess.Marketing.EmailCampaignTasksDataAccess();
            }
        }

        public IOrderReportingService OrderReportingService
        {
            get
            {
                // TODO: Implement this property getter
                throw new NotImplementedException();
            }
        }
    }
}