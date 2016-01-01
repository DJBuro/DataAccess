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
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetByUsernamePassword(string username, string password, int applicationId, out DataWarehouseDataAccess.Domain.Customer customer)
        {
            customer = null;

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var query =
                dataWarehouseEntities.Customers
                    .Where(e => e.ACSAplicationId == applicationId)
                    .Where(e => e.Username == username)
                    .Select
                    (
                        e => new
                        {
                            e.Id,
                            e.Title,
                            e.FirstName,
                            e.Surname,
                            e.Address,
                            Contacts = e.Contacts.Select(contact => new { ContactType = contact.ContactType.Name, MarketingLevel = contact.MarketingLevel.Name, contact.Value }),
                            e.Password,
                            e.PasswordSalt
                        }
                    );

                var entity = query.FirstOrDefault();

                if (entity == null)
                {
                    return "Unknown username: " + username;
                }
                else
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
                        Title = entity.Title,
                        FirstName = entity.FirstName,
                        Surname = entity.Surname
                    };

                    if (entity.Address != null)
                    {
                        customer.Address = new DataWarehouseDataAccess.Domain.Address()
                        {
                            County = entity.Address.County,
                            Locality = entity.Address.Locality,
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
                            Town = entity.Address.Town,
                            Directions = entity.Address.Directions,
                            Country = entity.Address.Country.CountryName
                        };
                    }

                    if (entity.Contacts != null)
                    {
                        customer.Contacts = new List<DataWarehouseDataAccess.Domain.Contact>();
                        foreach (var contact in entity.Contacts)
                        {
                            customer.Contacts.Add
                            (
                                new DataWarehouseDataAccess.Domain.Contact()
                                {
                                    MarketingLevel = contact.MarketingLevel,
                                    Type = contact.ContactType,
                                    Value = contact.Value
                                }
                            );
                        }
                    }
                }
            }

            return "";
        }

        public string Exists(string username, int applicationId, out bool exists)
        {
            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                exists =
                    dataWarehouseEntities.Customers
                    .Any(e => e.ACSAplicationId == applicationId && e.Username == username);
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
                        ACSAplicationId = applicationId,
                        RegisteredDateTime = DateTime.UtcNow
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
                            Town = customer.Address.Town,
                            Directions = customer.Address.Directions
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

        public string UpdateCustomer(string username, string password, string newPassword, int applicationId, DataWarehouseDataAccess.Domain.Customer customer)
        {
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
                        // Hash the password
                        byte[] salt = null;
                        string passwordHash = PasswordHash.CreateHash(password, customerEntity.PasswordSalt, out salt);

                        // Does the password provided match the one in the database?
                        if (passwordHash != customerEntity.Password)
                        {
                            return "Incorrect password";
                        }

                        // Does the customer want to change their password?
                        if (newPassword != null && newPassword.Length > 0)
                        {
                            string newPasswordHash = PasswordHash.CreateHash(newPassword, customerEntity.PasswordSalt, out salt);
                            customerEntity.Password = newPasswordHash;
                        }

                        if (customer != null)
                        {
                            // Update the customer entity
                            customerEntity.FirstName = customer.FirstName;
                            customerEntity.Surname = customer.Surname;
                            customerEntity.Title = customer.Title;

                            // Does the address need to be updated?
                            if (customer.Address != null)
                            {
                                Model.Address addressEntity = null;

                                // Is there already an address for this customer?
                                if (customerEntity.AddressId.HasValue)
                                {
                                    // Get the existing address
                                    var addressQuery = from a in dataWarehouseEntities.Addresses
                                                       where a.Id == customerEntity.AddressId
                                                       select a;

                                    addressEntity = addressQuery.FirstOrDefault();
                                }
                                else
                                {
                                    // Create a new address
                                    addressEntity = new Model.Address();
                                    dataWarehouseEntities.Addresses.Add(addressEntity);
                                    customerEntity.Address = addressEntity;
                                }

                                // Update the address entity
                                addressEntity.County = customer.Address.County;
                                addressEntity.Locality = customer.Address.Locality;
                                addressEntity.Org1 = customer.Address.Org1;
                                addressEntity.Org2 = customer.Address.Org2;
                                addressEntity.Org3 = customer.Address.Org3;
                                addressEntity.PostCode = customer.Address.Postcode;
                                addressEntity.Prem1 = customer.Address.Prem1;
                                addressEntity.Prem2 = customer.Address.Prem2;
                                addressEntity.Prem3 = customer.Address.Prem3;
                                addressEntity.Prem4 = customer.Address.Prem4;
                                addressEntity.Prem5 = customer.Address.Prem5;
                                addressEntity.Prem6 = customer.Address.Prem6;
                                addressEntity.RoadName = customer.Address.RoadName;
                                addressEntity.RoadNum = customer.Address.RoadNum;
                                addressEntity.State = customer.Address.State;
                                addressEntity.Town = customer.Address.Town;
                                addressEntity.Directions = customer.Address.Directions;

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
                            }

                            string newUsername = "";

                            // Do the contacts need to be updated?
                            if (customer.Contacts != null && customer.Contacts.Count > 0)
                            {
                                // Remove the customers contacts (easier to delete and then create rather than updating them)
                                var contactsQuery = from c in dataWarehouseEntities.Contacts
                                                    where c.CustomerId == customerEntity.Id
                                                    select c;

                                // Can't remove items from a collection while iterating through the same collection
                                List<Model.Contact> removeContacts = new List<Model.Contact>();
                                foreach (Model.Contact contact in contactsQuery)
                                {
                                    removeContacts.Add(contact);
                                }
                                foreach (Model.Contact contact in removeContacts)
                                {
                                    dataWarehouseEntities.Contacts.Remove(contact);
                                }

                                foreach (DataWarehouseDataAccess.Domain.Contact contact in customer.Contacts)
                                {
                                    // Build the contact entity
                                    Model.Contact contactEntity = new Model.Contact()
                                    {
                                        CustomerId = customerEntity.Id,
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

                                    // Add the contact
                                    dataWarehouseEntities.Contacts.Add(contactEntity);

                                    // Is the customer changing their username (we use the email as the username)?
                                    if (contactEntity.ContactType.Name == "Email")
                                    {
                                        newUsername = contactEntity.Value;
                                    }
                                }
                            }

                            // Is the customer changing their username?
                            if (newUsername.Length > 0)
                            {
                                customerEntity.Username = newUsername;
                            }
                        }

                        // Commit the customer
                        dataWarehouseEntities.SaveChanges();
                    }
                }
            }

            return "";
        }
    }
}
