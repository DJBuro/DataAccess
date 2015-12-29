using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface ICustomerDataAccess
    {
        
        string ConnectionStringOverride { get; set; }

        string GetByUsernamePassword(string username, string password, int applicationId, out DataWarehouseDataAccess.Domain.Customer customer);
        string Exists(string username, int applicationId, out bool exists);
        string AddCustomer(string username, string password, int applicationId, DataWarehouseDataAccess.Domain.Customer customer);
        string UpdateCustomer(string username, string password, string newPassword, int applicationId, DataWarehouseDataAccess.Domain.Customer customer);
        string UpdateCustomerLoyalty(string username, int applicationId, DataWarehouseDataAccess.Domain.CustomerLoyalty customerLoyalty);
        string UpdateCustomerLoyaltyPoints(string userName, int applicationId, string externalOrderRef, bool commit);

        void AddLoyaltyProvider(DataWarehouseDataAccess.Domain.Customer customer, AndroCloudDataAccess.Domain.SiteLoyalty loyalty);
    }
}
