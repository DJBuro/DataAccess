using System;
using System.Linq;
using AndroCloudDataAccess.Domain;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccessEntityFramework.Model;
using System.Collections.Generic;
using System.Transactions;
using System.Data.Entity;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class CustomerGPSDataAccess : ICustomerGPSDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetById(Guid customerId, out DataWarehouseDataAccess.Domain.CustomerGPS customerGPS)
        {
            customerGPS = null;

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var query = from c in dataWarehouseEntities.CustomerGPS
                            where c.CustomerId == customerId
                            select c;

                var entity = query.FirstOrDefault();

                if (entity == null)
                {
                    return "Unknown customerId: " + customerId.ToString();
                }
                else
                {
                    customerGPS = new DataWarehouseDataAccess.Domain.CustomerGPS()
                    {
                        CustomerId = entity.CustomerId,
                        PartnerId = entity.PartnerId
                    };
                }
            }

            return "";
        }

        public string Add(DataWarehouseDataAccess.Domain.CustomerGPS newCustomerGPS)
        {
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                CustomerGP customerGPS = new CustomerGP()
                {
                    CustomerId = newCustomerGPS.CustomerId,
                    PartnerId = newCustomerGPS.PartnerId
                };

                dataWarehouseEntities.CustomerGPS.Add(customerGPS);
                dataWarehouseEntities.SaveChanges();
            }

            return "";
        }

        public string Delete(DataWarehouseDataAccess.Domain.CustomerGPS deleteCustomerGPS)
        {
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var query = from c in dataWarehouseEntities.CustomerGPS
                            where c.CustomerId == deleteCustomerGPS.CustomerId
                            select c;

                var entity = query.FirstOrDefault();

                if (entity == null)
                {
                    return "Unknown customerId: " + deleteCustomerGPS.CustomerId.ToString();
                }
                else
                {
                    dataWarehouseEntities.CustomerGPS.Remove(entity);
                    dataWarehouseEntities.SaveChanges();
                }
            }

            return "";
        }
    }
}
