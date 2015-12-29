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

                    var customerQuery = from p in dataWarehouseEntities.Customers
                                        where p.Username == username
                                        && p.ACSAplicationId == applicationId
                                        select p;

                    var customerEntity = customerQuery.FirstOrDefault();

                    if (customerEntity == null)
                    {
                        return "Unknown username: " + username;
                    }
                    else
                    {
                        // Is there an existing password reset request?
                        var passwordResetQuery = from p in dataWarehouseEntities.PasswordResetRequests
                                                where p.CustomerId == customerEntity.Id
                                                select p;

                        var passwordResetEntity = passwordResetQuery.FirstOrDefault();

                        // Does the customer already have a password reset request?
                        if (passwordResetEntity == null)
                        {
                            // No existing request - create one
                            passwordResetEntity = new PasswordResetRequest()
                            {
                                Customer = customerEntity
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

        public string PasswordReset(string username, string token, string newPassword, out int customerId)
        {
            customerId = 0;

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
                        var customerQuery = from p in dataWarehouseEntities.Customers
                                            where p.Id == passwordResetEntity.CustomerId
                                            && p.Username == username
                                            select p;

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
                            string passwordHash = PasswordHash.CreateHash(newPassword, customerEntity.PasswordSalt, out salt);

                            customerEntity.Password = passwordHash;

                            // Delete the password reset request
                            dataWarehouseEntities.PasswordResetRequests.Remove(passwordResetEntity);
                            
                            // Commit changes
                            dataWarehouseEntities.SaveChanges();

                            // Return the customer id so it can be logged
                            customerId = customerEntity.Id;
                        }
                    }
                }
            }

            return "";
        }
    }
}
