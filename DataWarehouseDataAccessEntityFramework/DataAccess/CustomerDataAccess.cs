﻿using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccessEntityFramework.Model;
using DataWarehouseDataAccess.Domain;
using System.Collections.Generic;
using System.Transactions;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetByUsernamePassword(string username, string password, int applicationId, out DataWarehouseDataAccess.Domain.Customer customer)
        {
            customer = null;

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);                

                var query = from p in dataWarehouseEntities.Customers
                            where p.Username == username
                            && p.ACSAplicationId == applicationId
                            join a in dataWarehouseEntities.Addresses
                                on p.AddressId equals a.Id
                            join c in dataWarehouseEntities.Contacts
                                on p.Id equals c.CustomerId
                            join m in dataWarehouseEntities.MarketingLevels
                                on c.MarketingLevelId equals m.Id
                            join ct in dataWarehouseEntities.ContactTypes
                                on c.ContactTypeId equals ct.Id
                            select p;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    // Hash the password
                    byte[] salt = null;
                    string passwordHash = PasswordHash.CreateHash(password, entity.PasswordSalt, out salt);

                    // Does the password provided match the one in the database?
                    if (passwordHash != entity.Password)
                    {
                        return "Incorrect password";
                    }

                    customer = new DataWarehouseDataAccess.Domain.Customer()
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        Surname = entity.Surname,
                        Title = entity.Title
                    };

                    if (entity.Address != null)
                    {
                        customer.Address = new DataWarehouseDataAccess.Domain.Address()
                        {
                            Country = entity.Address.Country.CountryName,
                            County = entity.Address.County,
                            Org1 = entity.Address.Org1,
                            Org2 = entity.Address.Org2,
                            Org3 = entity.Address.Org3,
                            Postcode = entity.Address.PostCode,
                            Prem1 = entity.Address.Prem1,
                            Prem2 = entity.Address.Prem2,
                            Prem3 = entity.Address.Prem3,
                            Prem4 = entity.Address.Prem4,
                            Prem5 = entity.Address.Prem5,
                            Prem6 = entity.Address.Prem6,
                            RoadName = entity.Address.RoadName,
                            RoadNum = entity.Address.RoadNum,
                            State = entity.Address.State,
                            Town = entity.Address.Town
                        };
                    }

                    if (entity.Contacts != null)
                    {
                        customer.Contacts = new List<DataWarehouseDataAccess.Domain.Contact>();
                        foreach (Model.Contact contact in entity.Contacts)
                        {
                            customer.Contacts.Add
                            (
                                new DataWarehouseDataAccess.Domain.Contact()
                                {
                                    MarketingLevel = contact.MarketingLevel.Name,
                                    Type = contact.ContactType.Name,
                                    Value = contact.Value
                                }
                            );
                        }
                    }
                }
            }

            return "";
        }

        public string AddCustomer(string username, string password, int applicationId, DataWarehouseDataAccess.Domain.Customer customer)
        {
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    // Has the username already been used for this application?
                    var customerQuery = from c in dataWarehouseEntities.Customers
                                        where c.Username == username
                                        && c.ACSAplicationId == applicationId
                                        select c;

                    if (customerQuery.Count() > 0)
                    {
                        return "Username already used: " + username;
                    }

                    // Hash the password
                    byte[] salt = null;
                    string passwordHash = PasswordHash.CreateHash(password, null, out salt);

                    // Create a customer entity
                    Model.Customer customerEntity = new Model.Customer()
                    {
                        Username = username,
                        FirstName = customer.FirstName,
                        Surname = customer.Surname,
                        Title = customer.Title,
                        Password = passwordHash,
                        PasswordSalt = salt,
                        ACSAplicationId = applicationId
                    };

                    // Is there an address?
                    Model.Address addressEntity = null;
                    if (customer.Address != null)
                    {
                        // Build the address entity
                        addressEntity = new Model.Address()
                        {
                            County = customer.Address.County,
                            Locality = customer.Address.Locality,
                            Org1 = customer.Address.Org1,
                            Org2 = customer.Address.Org2,
                            Org3 = customer.Address.Org3,
                            PostCode = customer.Address.Postcode,
                            Prem1 = customer.Address.Prem1,
                            Prem2= customer.Address.Prem2,
                            Prem3 = customer.Address.Prem3,
                            Prem4 = customer.Address.Prem4,
                            Prem5 = customer.Address.Prem5,
                            Prem6 = customer.Address.Prem6,
                            RoadName = customer.Address.RoadName,
                            RoadNum = customer.Address.RoadNum,
                            State = customer.Address.State,
                            Town = customer.Address.Town
                        };

                        // Get the country
                        var countryQuery = from c in dataWarehouseEntities.Countries
                                    where c.CountryName == customer.Address.Country
                                    select c;

                        var countryEntity = countryQuery.FirstOrDefault();

                        if (countryEntity == null)
                        {
                            return "Unknown country: " + customer.Address.Country;
                        }
                        
                        // Got the country
                        addressEntity.Country = countryEntity;

                        // Add the address to the customer
                        customerEntity.Address = addressEntity;
                    }

                    // Are there contact details?
                    if (customer.Contacts != null && customer.Contacts.Count > 0)
                    {
                        foreach (DataWarehouseDataAccess.Domain.Contact contact in customer.Contacts)
                        {
                            // Build the contact entity
                            Model.Contact contactEntity = new Model.Contact()
                            {
                                Value = contact.Value
                            };

                            // Get the marketing level
                            var marketingLevelQuery = from m in dataWarehouseEntities.MarketingLevels
                                        where m.Name == contact.MarketingLevel
                                        select m;

                            var marketingLevelEntity = marketingLevelQuery.FirstOrDefault();

                            if (marketingLevelEntity == null)
                            {
                                return "Unknown marketing level: " + contact.MarketingLevel;
                            }

                            // Got the marketing level
                            contactEntity.MarketingLevel = marketingLevelEntity;

                            // Get the contact type
                            var contactTypeQuery = from ct in dataWarehouseEntities.ContactTypes
                                        where ct.Name == contact.Type
                                        select ct;

                            var contactTypeEntity = contactTypeQuery.FirstOrDefault();

                            if (contactTypeEntity == null)
                            {
                                return "Unknown contact type: " + contact.Type;
                            }

                            // Got the contact type
                            contactEntity.ContactType = contactTypeEntity;

                            // Add the contact to the customer
                            customerEntity.Contacts.Add(contactEntity);
                        }
                    }

                    // Add the customer to the database
                    dataWarehouseEntities.Customers.Add(customerEntity);

                    // Commit the customer
                    dataWarehouseEntities.SaveChanges();
                }
            }

            return "";
        }

        public string UpdateCustomer(string username, string password, DataWarehouseDataAccess.Domain.Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
