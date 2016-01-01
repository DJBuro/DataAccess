using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccessEntityFramework.Model;
using DataWarehouseDataAccess.Domain;
using System.Collections.Generic;
using System.Transactions;
using DataWarehouseDataAccessEntityFramework.Domain;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class PasswordResetRequestDataAccess : IPasswordResetRequestDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string RequestPasswordReset(string username, int applicationId, out string token)
        {
            token = "";

            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    var customerQuery = from c in dataWarehouseEntities.Customers
                                        join ca in dataWarehouseEntities.CustomerAccounts
                                            on c.CustomerAccountId equals ca.ID
                                        where ca.Username == username
                                        && c.ACSAplicationId == applicationId
                                        select c;

                    var customerEntity = customerQuery.FirstOrDefault();

                    if (customerEntity == null)
                    {
                        return "Unknown username: " + username;
                    }
                    else
                    {
                        // Is there an existing password reset request?
                        var passwordResetQuery = from p in dataWarehouseEntities.PasswordResetRequests
                                                where p.CustomerId == customerEntity.ID
                                                select p;

                        var passwordResetEntity = passwordResetQuery.FirstOrDefault();

                        // Does the customer already have a password reset request?
                        if (passwordResetEntity == null)
                        {
                            // No existing request - create one
                            passwordResetEntity = new PasswordResetRequest()
                            {
                                CustomerId = customerEntity.ID
                            };

                            dataWarehouseEntities.PasswordResetRequests.Add(passwordResetEntity);
                        }

                        // Create the new request
                        passwordResetEntity.RequestedDateTime = DateTime.UtcNow;
                        passwordResetEntity.Token = Guid.NewGuid().ToString().Replace("-", "");

                        // Commit the password reset request
                        dataWarehouseEntities.SaveChanges();

                        token = passwordResetEntity.Token;
                    }
                }
            }

            return "";
        }

        public string PasswordReset(string username, string token, string newPassword, out Guid? customerId)
        {
            customerId = null;

            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    var passwordResetQuery = from p in dataWarehouseEntities.PasswordResetRequests
                                             where p.Token == token
                                             select p;

                    var passwordResetEntity = passwordResetQuery.FirstOrDefault();

                    // Is there a password reset request for this token?
                    if (passwordResetEntity == null)
                    {
                        return "Unknown token";
                    }
                    else 
                    {
                        var customerQuery = from c in dataWarehouseEntities.Customers
                                            join ca in dataWarehouseEntities.CustomerAccounts
                                                on c.CustomerAccountId equals ca.ID
                                            where c.ID == passwordResetEntity.CustomerId
                                                && ca.Username == username
                                            select c;

                        var customerEntity = customerQuery.FirstOrDefault();

                        if (customerEntity == null)
                        {
                            return "Unknown customer: " + passwordResetEntity.CustomerId;
                        }
                        else
                        {
                            // Is the request still valid?
                            TimeSpan timespan = DateTime.UtcNow - passwordResetEntity.RequestedDateTime;
                            if (timespan.Minutes > 30)
                            {
                                return "Password reset request expired";
                            }

                            // Reset the password
                            byte[] salt = null;
                            string passwordHash = PasswordHash.CreateHash(newPassword, customerEntity.CustomerAccount.PasswordSalt, out salt);

                            customerEntity.CustomerAccount.Password = passwordHash;

                            // Delete the password reset request
                            dataWarehouseEntities.PasswordResetRequests.Remove(passwordResetEntity);
                            
                            // Commit changes
                            dataWarehouseEntities.SaveChanges();

                            // Return the customer id so it can be logged
                            customerId = customerEntity.ID;
                        }
                    }
                }
            }

            return "";
        }
    }
}
