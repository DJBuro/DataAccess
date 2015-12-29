using System;
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

        public string GetByUsernamePassword(string username, string password, out DataWarehouseDataAccess.Domain.Customer customer)
        {
            customer = null;

            using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
            {
                DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                var query = from p in dataWarehouseEntities.Customers
                            where p.Username == username
                            && p.Password == password
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
                            Country = entity.Address.Country.ISO3166_1_alpha_2,
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

        public string AddCustomer(string username, string password, DataWarehouseDataAccess.Domain.Customer customer)
        {
            // Get the 
            //var siteStatusQuery = from s in acsEntities.SiteStatuses
            //                      where s.Status == customer.
            //                      select s;
            //Model.SiteStatus siteStatusEntity = siteStatusQuery.FirstOrDefault();

            //if (siteStatusEntity == null)
            //{
            //    //TODO ERROR??
            //}

            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    Model.Customer customerEntity = new Model.Customer()
                    {
                        Username = username,
                        FirstName = customer.FirstName,
                        Surname = customer.Surname,
                        Title = customer.Title
                    };

                    dataWarehouseEntities.Customers.Add(customerEntity);
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
