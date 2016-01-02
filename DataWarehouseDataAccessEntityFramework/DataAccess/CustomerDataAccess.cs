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
    public class CustomerDataAccess : ICustomerDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetByUsernamePassword(string username, string password, int applicationId, out DataWarehouseDataAccess.Domain.Customer customer)
        {
            customer = null;

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var query = from c in dataWarehouseEntities.Customers
                                .Include(e=>e.CustomerLoyalties)
                            join ca in dataWarehouseEntities.CustomerAccounts
                                on c.CustomerAccountId equals ca.ID
                            where c.ACSAplicationId == applicationId
                                && ca.Username == username
                            select new
                            {
                                c.ID,
                                c.Title,
                                c.FirstName,
                                c.LastName,
                                c.Address,
                                Contacts = c.Contacts.Select(contact => new { ContactType = contact.ContactType.Name, MarketingLevel = contact.MarketingLevel.Name, contact.Value }),
                                c.CustomerLoyalties,
                                ca.Password,
                                ca.PasswordSalt
                            };

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
                        Id = entity.ID,
                        Title = entity.Title,
                        FirstName = entity.FirstName,
                        Surname = entity.LastName
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

                    if (entity.CustomerLoyalties != null)
                    {
                        customer.CustomerLoyalties = new List<DataWarehouseDataAccess.Domain.CustomerLoyalty>();
                        var customerLoyalty = entity.CustomerLoyalties.Select(e => new
                        {
                            e.Id,
                            e.CustomerId,
                            e.ProviderName,
                            e.Points,
                            PendingRedeemedPoints = e.Customer.OrderHeaders.SelectMany(r => r.OrderLoyalties)
                                .Where(r => r.redeemedPoints > 0)
                                .Where(r=> !r.Applied)
                                .Sum(r => r.redeemedPoints)
                        }).ToArray();

                        foreach (var loyalty in customerLoyalty)
                        {

                            customer.CustomerLoyalties.Add
                            (
                                new DataWarehouseDataAccess.Domain.CustomerLoyalty()
                                {
                                    Id = loyalty.Id,
                                    CustomerId = loyalty.CustomerId,
                                    ProviderName = loyalty.ProviderName,
                                    Points = (loyalty.Points ?? 0) - loyalty.PendingRedeemedPoints.GetValueOrDefault()
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

                exists = (from c in dataWarehouseEntities.Customers
                          .Include(e => e.CustomerLoyalties)
                          join ca in dataWarehouseEntities.CustomerAccounts
                              on c.CustomerAccountId equals ca.ID
                          where ca.Username == username
                              && c.ACSAplicationId == applicationId
                          select c).Count() > 0;
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
                                        join ca in dataWarehouseEntities.CustomerAccounts
                                            on c.CustomerAccountId equals ca.ID
                                        where ca.Username == username
                                            && c.ACSAplicationId == applicationId
                                        select c;

                    if (customerQuery.Count() > 0)
                    {
                        return "User-name already used: " + username;
                    }

                    // Hash the password
                    byte[] salt = null;
                    string passwordHash = PasswordHash.CreateHash(password, null, out salt);

                    // Create a customer entity
                    Model.Customer customerEntity = new Model.Customer()
                    {
                        ID = Guid.NewGuid(),
                        FirstName = customer.FirstName,
                        LastName = customer.Surname,
                        Title = customer.Title,
                        ACSAplicationId = applicationId,
                        RegisteredDateTime = DateTime.UtcNow,
                        CustomerAccount = new CustomerAccount()
                        {
                            ID = Guid.NewGuid(),
                            Username = username,
                            Password = passwordHash,
                            PasswordSalt = salt,
                            RegisteredDateTime = DateTime.UtcNow,
                            FacebookId = customer.FacebookId,
                            FacebookUsername = customer.FacebookUsername
                        }
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

                    // We need to return the customers account id
                    customer.Id = customerEntity.ID;
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

                    var customerQuery = from c in dataWarehouseEntities.Customers.Include(e => e.CustomerLoyalties)
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
                        // Hash the password
                        byte[] salt = null;
                        string passwordHash = PasswordHash.CreateHash(password, customerEntity.CustomerAccount.PasswordSalt, out salt);

                        // Does the password provided match the one in the database?
                        if (passwordHash != customerEntity.CustomerAccount.Password)
                        {
                            return "Incorrect password";
                        }

                        // Does the customer want to change their password?
                        if (newPassword != null && newPassword.Length > 0)
                        {
                            string newPasswordHash = PasswordHash.CreateHash(newPassword, customerEntity.CustomerAccount.PasswordSalt, out salt);
                            customerEntity.CustomerAccount.Password = newPasswordHash;
                        }

                        if (customer != null)
                        {
                            // Update the customer entity
                            customerEntity.FirstName = customer.FirstName;
                            customerEntity.LastName = customer.Surname;
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
                                                    where c.CustomerId == customerEntity.ID
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

                                // Commit the customer
                                dataWarehouseEntities.SaveChanges();

                                foreach (DataWarehouseDataAccess.Domain.Contact contact in customer.Contacts)
                                {
                                    // Build the contact entity
                                    Model.Contact contactEntity = new Model.Contact()
                                    {
                                        CustomerId = customerEntity.ID,
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
                                    //if (contactEntity.ContactType.Name == "Email")
                                    //{
                                    //    // Check to see if the username has already been used
                                    //    bool exists = false;
                                    //    this.Exists(contactEntity.Value, applicationId, out exists);

                                    //    if (exists)
                                    //    {
                                    //        return "Username already used: " + contactEntity.Value;
                                    //    }
                                    //    else
                                    //    {
                                    //        // Update
                                    //        newUsername = contactEntity.Value;
                                    //    }
                                    //}
                                }
                            }

                            // Is the customer changing their username?
                            if (newUsername.Length > 0)
                            {
                                customerEntity.CustomerAccount.Username = newUsername;
                            }
                        }

                        // Commit the customer
                        dataWarehouseEntities.SaveChanges();
                    }
                }
            }

            return "";
        }

        public string UpdateCustomerLoyaltyPoints(string userName, int applicationId, string externalOrderRef, bool commit)
        {
            //commit: true will effect the customer points 
            //commit: false will assign the row as applied, but essentially is rejected (no points changed). 

            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    var customerEntity = dataWarehouseEntities.Customers
                        .Where(e => e.ACSAplicationId == applicationId)
                        .Where(e => e.CustomerAccount.Username == userName)
                        .FirstOrDefault();

                    var loyaltyEntities = customerEntity.CustomerLoyalties;

                    var table = dataWarehouseEntities.OrderLoyalties.Include(e => e.OrderHeader.Customer);

                    var query = table
                        .Where(e => e.OrderHeader.ExternalOrderRef == externalOrderRef)
                        .Where(e => e.OrderHeader.CustomerID == customerEntity.ID)
                        .Where(e => !e.Applied);

                    var result = query.ToArray();

                    //this should have been achieved in the below loops :-/
                    if (!commit) 
                    {
                        foreach (var item in result) 
                        {
                            item.Applied = true;
                        }

                        dataWarehouseEntities.SaveChanges();
                        return string.Empty;
                    }

                    //add in any loyalty points
                    foreach (var update in result.Where(e => e.awardedPoints > 0))
                    {
                        var customerLoyaltyEntity = loyaltyEntities
                            .FirstOrDefault(e => e.ProviderName.Equals(update.ProviderName, StringComparison.InvariantCultureIgnoreCase));
                        
                        //he who doesn't exist...
                        if (customerLoyaltyEntity == null) 
                        {
                            customerLoyaltyEntity = new Model.CustomerLoyalty()
                            {
                                Id = Guid.NewGuid(),
                                Points = 0,
                                ProviderName = update.ProviderName
                            };
                            loyaltyEntities.Add(customerLoyaltyEntity);    
                        }

                        if (commit) 
                        { 
                            customerLoyaltyEntity.Points += update.awardedPoints;
                        }
                        update.Applied = true;
                    }

                    //take away any loyalty points 
                    foreach (var update in result.Where(e => e.redeemedPoints > 0))
                    {
                        var customerLoyaltyEntity = loyaltyEntities
                            .FirstOrDefault(e => e.ProviderName.Equals(update.ProviderName, StringComparison.InvariantCultureIgnoreCase));

                        //he who doesn't exist...
                        if (customerLoyaltyEntity == null)
                        {
                            customerLoyaltyEntity = new Model.CustomerLoyalty()
                            {
                                Id = Guid.NewGuid(),
                                Points = 0,
                                ProviderName = update.ProviderName
                            };
                            loyaltyEntities.Add(customerLoyaltyEntity); 
                        }

                        if (commit) 
                        { 
                            customerLoyaltyEntity.Points -= update.redeemedPoints;
                        }
                        update.Applied = true;
                    }

                    dataWarehouseEntities.SaveChanges();
                }
            }

            return string.Empty;
        }

        public void AddLoyaltyProvider(DataWarehouseDataAccess.Domain.Customer customer, SiteLoyalty loyalty)
        {
            bool exists = customer.CustomerLoyalties.Any
                (e => e.ProviderName.Equals(loyalty.ProviderName, StringComparison.InvariantCultureIgnoreCase));

            if (exists) { return; }

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities()) 
            {
                var customerTable = dataWarehouseEntities.Customers;

                var customerEntity = customerTable.FirstOrDefault(e => customer.Id == e.ID);
                var customerLoyalty = new CustomerLoyalty
                {
                    Id = Guid.NewGuid(),
                    ProviderName = loyalty.ProviderName,
                    Customer = customerEntity
                };

                if (customerEntity == null) return;

                if (loyalty.ProviderName.Equals("Andromeda", StringComparison.InvariantCultureIgnoreCase)) 
                {
                    if (loyalty.Configuration != null) 
                    {
                        if (loyalty.Configuration.AwardOnRegiration != null)
                        {
                            customerLoyalty.Points = loyalty.Configuration.AwardOnRegiration;
                        }
                        else 
                        {
                            customerLoyalty.Points = 0;
                        }
                    }
                }

                customerEntity.CustomerLoyalties = customerEntity.CustomerLoyalties == null ? new List<CustomerLoyalty>() : customerEntity.CustomerLoyalties;
                customerEntity.CustomerLoyalties.Add(customerLoyalty);

                dataWarehouseEntities.SaveChanges();

                customer.CustomerLoyalties = new List<DataWarehouseDataAccess.Domain.CustomerLoyalty>();
                foreach (CustomerLoyalty customerLoyaltyEntity in customerEntity.CustomerLoyalties)
                {
                    customer.CustomerLoyalties.Add
                    (
                        new DataWarehouseDataAccess.Domain.CustomerLoyalty() 
                        {
                            Points = customerLoyaltyEntity.Points,
                            ProviderName = customerLoyaltyEntity.ProviderName
                        }
                    );
                }
            }
        }

        public string UpdateCustomerLoyalty(string username, int applicationId, DataWarehouseDataAccess.Domain.CustomerLoyalty customerLoyalty)
        {
            if (customerLoyalty == null)
            {
                return "Customer Loyalty NULL: " + username;
            }

            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    var customerQuery = from c in dataWarehouseEntities.Customers.Include(e => e.CustomerLoyalties)
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
                        if (!customerEntity.ID.Equals(customerLoyalty.CustomerId))
                        {
                            return "CustomerId mismatch: " + username;
                        }
                        bool isAddLoyalty = customerEntity.CustomerLoyalties == null || (customerEntity.CustomerLoyalties != null && customerEntity.CustomerLoyalties.Count == 0);
                        if (isAddLoyalty)
                        {
                            // insert
                            customerEntity.CustomerLoyalties = new List<Model.CustomerLoyalty>();
                            customerEntity.CustomerLoyalties.Add(PrepareCustomerLoyalty(customerLoyalty));
                        }
                        else
                        {
                            // Update 
                            Model.CustomerLoyalty existingConfig = customerEntity.CustomerLoyalties.FirstOrDefault(c => c.ProviderName.ToLower().Equals(customerLoyalty.ProviderName.ToLower()));
                            if (existingConfig != null)
                            {
                                //existingConfig.Points = customerLoyalty.Points ?? 0;
                                decimal calculatedPoints = ((existingConfig.Points ?? 0) + (customerLoyalty.PointsGained ?? 0)) - (customerLoyalty.PointsUsed ?? 0);
                                existingConfig.Points = (calculatedPoints < 0) ? 0 : calculatedPoints;
                            }
                            else
                            {
                                customerEntity.CustomerLoyalties.Add(PrepareCustomerLoyalty(customerLoyalty));
                            }
                        }

                        // Commit the customer loyalty
                        dataWarehouseEntities.SaveChanges();
                    }
                }
            }

            return "";
        }

        private Model.CustomerLoyalty PrepareCustomerLoyalty(DataWarehouseDataAccess.Domain.CustomerLoyalty customerLoyalty)
        {
            Model.CustomerLoyalty cl = new Model.CustomerLoyalty()
            {
                Id = Guid.NewGuid(),
                CustomerId = customerLoyalty.Id,
                ProviderName = customerLoyalty.ProviderName
            };

            decimal calculatedPoints = ((customerLoyalty.PointsGained ?? 0) - (customerLoyalty.PointsUsed ?? 0));
            cl.Points = (calculatedPoints < 0) ? 0 : calculatedPoints;

            return cl;
        }
    
    }

}
