using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Data.Common;
using System.Globalization;
using System.Transactions;
using System.Data.Objects;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreDAO : IStoreDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.StoreListItem> GetAllStoreListItems()
        {
            List<Domain.StoreListItem> models = new List<Domain.StoreListItem>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typo - EF cleverly removes the S off the end
                            orderby s.Name
                            select new
                            {
                                s.Id,
                                s.Name,
                                s.AndromedaSiteId,
                                s.CustomerSiteId,
                                StoreStatusStatus = s.StoreStatu.Status
                            };

                foreach (var entity in query)
                {
                    Domain.StoreListItem model = new Domain.StoreListItem()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        StoreStatus = entity.StoreStatusStatus
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public IList<Domain.Store> GetAll()
        {
            List<Domain.Store> models = new List<Domain.Store>();
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Configuration.LazyLoadingEnabled = false;

                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typeo - EF cleverly removes the S off the end
                            join a in entitiesContext.Addresses.DefaultIfEmpty()
                            on s.AddressId equals a.Id
                            join c in entitiesContext.Countries.DefaultIfEmpty()
                            on a.CountryId equals c.Id
                       //     join pp in entitiesContext.StorePaymentProviders.DefaultIfEmpty()
                        //    on s.StorePaymentProviderID equals pp.Id
                            orderby s.Name
                            select new
                            {
                                s.Id,
                                s.Name,
                                s.AndromedaSiteId,
                                s.CustomerSiteId,
                                s.LastFTPUploadDateTime,
                                StoreStatusId = s.StoreStatu.Id,
                                StoreStatusStatus = s.StoreStatu.Status,
                                StoreStatusDescription = s.StoreStatu.Description,
                                s.ExternalId,
                                s.ExternalSiteName,
                                s.ClientSiteName,
                                s.Telephone,
                                s.TimeZone,
                                AddressId = a.Id,
                                AddressOrg1 = a.Org1,
                                AddressOrg2 = a.Org2,
                                AddressOrg3 = a.Org3,
                                AddressPrem1 = a.Prem1,
                                AddressPrem2 = a.Prem2,
                                AddressPrem3 = a.Prem3,
                                AddressPrem4 = a.Prem4,
                                AddressPrem5 = a.Prem5,
                                AddressPrem6 = a.Prem6,
                                AddressRoadNum = a.RoadNum,
                                AddressRoadName = a.RoadName,
                                AddressLocality = a.Locality,
                                AddressTown = a.Town,
                                AddressCounty = a.County,
                                AddressState = a.State,
                                AddressPostCode = a.PostCode,
                                AddressDPS = a.DPS,
                                AddressLat = a.Lat,
                                AddressLong = a.Long,
                                CountryCountryName = c.CountryName,
                                CountryId = c.Id,
                                CountryISO3166_1_alpha_2 = c.ISO3166_1_alpha_2,
                                CountryISO3166_1_numeric = c.ISO3166_1_numeric//,
                             //   PaymentProvider = pp
                            };

                foreach (var entity in query)
                {
                    Domain.Store model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        CustomerSiteName = entity.ClientSiteName,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatusId, Status = entity.StoreStatusStatus, Description = entity.StoreStatusDescription },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        Telephone = entity.Telephone,
                        TimeZone = entity.TimeZone
                    };

                    model.Address = new Domain.Address()
                    {
                        Id = entity.AddressId,
                        Org1 = entity.AddressOrg1,
                        Org2 = entity.AddressOrg2,
                        Org3 = entity.AddressOrg3,
                        Prem1 = entity.AddressPrem1,
                        Prem2 = entity.AddressPrem2,
                        Prem3 = entity.AddressPrem3,
                        Prem4 = entity.AddressPrem4,
                        Prem5 = entity.AddressPrem5,
                        Prem6 = entity.AddressPrem6,
                        RoadNum = entity.AddressRoadNum,
                        RoadName = entity.AddressRoadName,
                        Locality = entity.AddressLocality,
                        Town = entity.AddressTown,
                        County = entity.AddressCounty,
                        State = entity.AddressState,
                        PostCode = entity.AddressPostCode,
                        DPS = entity.AddressDPS,
                        Lat = entity.AddressLat,
                        Long = entity.AddressLong,
                        Country = new Domain.Country()
                        {
                            CountryName = entity.CountryCountryName,
                            Id = entity.CountryId,
                            ISO3166_1_alpha_2 = entity.CountryISO3166_1_alpha_2,
                            ISO3166_1_numeric = entity.CountryISO3166_1_numeric
                        }
                    };

                    //if (entity.PaymentProvider != null)
                    //{
                    //    model.PaymentProvider = new Domain.StorePaymentProvider();
                    //    model.PaymentProvider.Id = entity.PaymentProvider.Id;
                    //    model.PaymentProvider.ClientId = entity.PaymentProvider.ClientId;
                    //    model.PaymentProvider.ClientPassword = entity.PaymentProvider.ClientPassword;
                    //    model.PaymentProvider.DisplayText = entity.PaymentProvider.DisplayText;
                    //    model.PaymentProvider.ProviderName = entity.PaymentProvider.ProviderName;
                    //}

                    models.Add(model);
                }
            }

            return models;
        }

        public void Add(Domain.Store store)
        {
            // We will use transactionscope to implicitly enrole both EF and direct SQL in the same transaction
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    if (store.Address.Country == null)
                    {
                        throw new ArgumentNullException("Address country cannot be null");
                    }

                    entitiesContext.Database.Connection.Open();

                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                    // Get the country
                    var query = from s in entitiesContext.Countries
                                where store.Address.Country.Id == s.Id
                                select s;

                    var country = query.FirstOrDefault();

                    // Add the address
                    Address addressEntity = new Address()
                    {
                        Org1 = store.Address.Org1,
                        Org2 = store.Address.Org2,
                        Org3 = store.Address.Org3,
                        Prem1 = store.Address.Prem1,
                        Prem2 = store.Address.Prem2,
                        Prem3 = store.Address.Prem3,
                        Prem4 = store.Address.Prem4,
                        Prem5 = store.Address.Prem5,
                        Prem6 = store.Address.Prem6,
                        RoadNum = store.Address.RoadNum,
                        RoadName = store.Address.RoadName,
                        Locality = store.Address.Locality,
                        Town = store.Address.Town,
                        County = store.Address.County,
                        State = store.Address.State,
                        PostCode = store.Address.PostCode,
                        DPS = store.Address.DPS,
                        Lat = store.Address.Lat,
                        Long = store.Address.Long,
                        CountryId = country.Id,
                        DataVersion = newVersion
                    };

                    entitiesContext.Addresses.Add(addressEntity);
                    entitiesContext.SaveChanges();

                    // Add the store
                    Store storeEntity = new Store()
                    {
                        Name = store.Name, // Andro site name
                        AndromedaSiteId = store.AndromedaSiteId,
                        CustomerSiteId = store.CustomerSiteId,
                        ClientSiteName = store.CustomerSiteName,
                        LastFTPUploadDateTime = store.LastFTPUploadDateTime,
                        StoreStatusId = store.StoreStatus.Id,
                        DataVersion = newVersion,
                        ExternalId = store.ExternalSiteId,
                        ExternalSiteName = store.ExternalSiteName,
                        AddressId = addressEntity.Id,
                        Telephone = store.Telephone,
                        TimeZone = store.TimeZone,
                        LicenseKey = "A24C92FE-92D1-4705-8E33-202F51BCE38D"
                    };

                    entitiesContext.Stores.Add(storeEntity);
                    entitiesContext.SaveChanges();

                    // Commit the transacton
                    transactionScope.Complete();

                    // Return the id to the caller
                    store.Id = storeEntity.Id;
                }
            }
        }

        public void Update(Domain.Store store)
        {
            // We will use transactionscope to implicitly enrole both EF and direct SQL in the same transaction
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    entitiesContext.Database.Connection.Open();

                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                    // Get the country
                    var query = from s in entitiesContext.Countries
                                where store.Address.Country.Id == s.Id
                                select s;

                    var country = query.FirstOrDefault();

                    // Get the store that needs to be updated
                    var storeQuery = from s in entitiesContext.Stores
                                     where store.Id == s.Id
                                     select s;

                    Store storeEntity = storeQuery.FirstOrDefault();

                    if (storeEntity == null)
                    {
                        //transaction.Rollback();
                        return;  // Transaction will be automatiacally rolled back
                    }

                    // Update the store
                    storeEntity.Name = store.Name;
                    storeEntity.AndromedaSiteId = store.AndromedaSiteId;
                    storeEntity.CustomerSiteId = store.CustomerSiteId;
                    storeEntity.ClientSiteName = store.CustomerSiteName;
                    storeEntity.LastFTPUploadDateTime = store.LastFTPUploadDateTime;
                    storeEntity.StoreStatusId = store.StoreStatus.Id;
                    storeEntity.DataVersion = newVersion;
                    storeEntity.ExternalId = store.ExternalSiteId;
                    storeEntity.ExternalSiteName = store.ExternalSiteName;
                    storeEntity.Telephone = store.Telephone;
                    storeEntity.TimeZone = store.TimeZone;
                    storeEntity.StorePaymentProviderID = (store.PaymentProvider == null ? null : (int?)store.PaymentProvider.Id);

                    // Update / create an address
                    var addressQuery = from s in entitiesContext.Addresses
                                        where s.Id == storeEntity.AddressId
                                        select s;

                    Address addressEntity = addressQuery.FirstOrDefault();

                    // Is there already an address for this store?
                    if (addressEntity == null)
                    {
                        // No address - we need to create one
                        addressEntity = new Address()
                        {
                            County = storeEntity.Address.County,
                            DPS = storeEntity.Address.DPS,
                            Lat = storeEntity.Address.Lat,
                            Locality = storeEntity.Address.Locality,
                            Long = storeEntity.Address.Long,
                            Org1 = storeEntity.Address.Org1,
                            Org2 = storeEntity.Address.Org2,
                            Org3 = storeEntity.Address.Org3,
                            PostCode = storeEntity.Address.PostCode,
                            Prem1 = storeEntity.Address.Prem1,
                            Prem2 = storeEntity.Address.Prem2,
                            Prem3 = storeEntity.Address.Prem3,
                            Prem4 = storeEntity.Address.Prem4,
                            Prem5 = storeEntity.Address.Prem5,
                            Prem6 = storeEntity.Address.Prem6,
                            RoadName = storeEntity.Address.RoadName,
                            RoadNum = storeEntity.Address.RoadNum,
                            State = storeEntity.Address.State,
                            Town = storeEntity.Address.Town,
                            CountryId = country.Id,
                            DataVersion = newVersion
                        };

                        entitiesContext.Addresses.Add(addressEntity);
                    }
                    else
                    {
                        if (store.Address != null)
                        {
                            // Update the existing address
                            addressEntity.County = store.Address.County;
                            addressEntity.DPS = store.Address.DPS;
                            addressEntity.Lat = store.Address.Lat;
                            addressEntity.Locality = store.Address.Locality;
                            addressEntity.Long = store.Address.Long;
                            addressEntity.Org1 = store.Address.Org1;
                            addressEntity.Org2 = store.Address.Org2;
                            addressEntity.Org3 = store.Address.Org3;
                            addressEntity.PostCode = store.Address.PostCode;
                            addressEntity.Prem1 = store.Address.Prem1;
                            addressEntity.Prem2 = store.Address.Prem2;
                            addressEntity.Prem3 = store.Address.Prem3;
                            addressEntity.Prem4 = store.Address.Prem4;
                            addressEntity.Prem5 = store.Address.Prem5;
                            addressEntity.Prem6 = store.Address.Prem6;
                            addressEntity.RoadName = store.Address.RoadName;
                            addressEntity.RoadNum = store.Address.RoadNum;
                            addressEntity.State = store.Address.State;
                            addressEntity.Town = store.Address.Town;
                            addressEntity.CountryId = country.Id;
                            addressEntity.DataVersion = newVersion;
                        }
                    }

                    entitiesContext.SaveChanges();

                    // Commit the transacton
                    transactionScope.Complete();
                }
            }
        }

        public Domain.Store GetById(int id)
        {
            Domain.Store model = null;

   //         using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            where id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        CustomerSiteName = entity.ClientSiteName,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        Telephone = entity.Telephone,
                        TimeZone = entity.TimeZone
                    };

                    // Get the address
                    var addressQuery = from s in entitiesContext.Addresses
                                       where s.Id == entity.AddressId
                                       select s;

                    var addressEntity = addressQuery.FirstOrDefault();

                    if (addressEntity != null)
                    {
                        var countryQuery = from c in entitiesContext.Countries
                                           where c.Id == addressEntity.CountryId
                                           select c;

                        var countryEntity = countryQuery.FirstOrDefault();

                        if (addressEntity != null)
                        {
                            model.Address = new Domain.Address()
                            {
                                Id = addressEntity.Id,
                                Org1 = addressEntity.Org1,
                                Org2 = addressEntity.Org2,
                                Org3 = addressEntity.Org3,
                                Prem1 = addressEntity.Prem1,
                                Prem2 = addressEntity.Prem2,
                                Prem3 = addressEntity.Prem3,
                                Prem4 = addressEntity.Prem4,
                                Prem5 = addressEntity.Prem5,
                                Prem6 = addressEntity.Prem6,
                                RoadNum = addressEntity.RoadNum,
                                RoadName = addressEntity.RoadName,
                                Locality = addressEntity.Locality,
                                Town = addressEntity.Town,
                                County = addressEntity.County,
                                State = addressEntity.State,
                                PostCode = addressEntity.PostCode,
                                DPS = addressEntity.DPS,
                                Lat = addressEntity.Lat,
                                Long = addressEntity.Long,
                                Country = new Domain.Country()
                                {
                                    CountryName = countryEntity.CountryName,
                                    Id = countryEntity.Id,
                                    ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                    ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                                }
                            };
                        }
                    }

                    // Get the payment provider
                    var paymentProviderQuery = from s in entitiesContext.StorePaymentProviders
                                       where s.Id == entity.StorePaymentProviderID
                                       select s;

                    var paymentProviderEntity = paymentProviderQuery.FirstOrDefault();

                    if (paymentProviderEntity != null)
                    {
                        model.PaymentProvider = new Domain.StorePaymentProvider()
                        {
                            Id = paymentProviderEntity.Id,
                            ClientId = paymentProviderEntity.ClientId,
                            ClientPassword = paymentProviderEntity.ClientPassword,
                            DisplayText = paymentProviderEntity.DisplayText,
                            ProviderName = paymentProviderEntity.ProviderName
                        };
                    }
                }
            }

            return model;
        }

        public Domain.Store GetByAndromedaId(int id)
        {
            Domain.Store model = null;

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            where id == s.AndromedaSiteId
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        CustomerSiteName = entity.ClientSiteName,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        Telephone = entity.Telephone,
                        TimeZone = entity.TimeZone
                    };

                    // Get the address
                    var addressQuery = from s in entitiesContext.Addresses
                                       where s.Id == entity.AddressId
                                       select s;

                    var addressEntity = addressQuery.FirstOrDefault();

                    if (addressEntity != null)
                    {
                        var countryQuery = from c in entitiesContext.Countries
                                           where c.Id == addressEntity.CountryId
                                           select c;

                        var countryEntity = countryQuery.FirstOrDefault();

                        if (addressEntity != null)
                        {
                            model.Address = new Domain.Address()
                            {
                                Id = addressEntity.Id,
                                Org1 = addressEntity.Org1,
                                Org2 = addressEntity.Org2,
                                Org3 = addressEntity.Org3,
                                Prem1 = addressEntity.Prem1,
                                Prem2 = addressEntity.Prem2,
                                Prem3 = addressEntity.Prem3,
                                Prem4 = addressEntity.Prem4,
                                Prem5 = addressEntity.Prem5,
                                Prem6 = addressEntity.Prem6,
                                RoadNum = addressEntity.RoadNum,
                                RoadName = addressEntity.RoadName,
                                Locality = addressEntity.Locality,
                                Town = addressEntity.Town,
                                County = addressEntity.County,
                                State = addressEntity.State,
                                PostCode = addressEntity.PostCode,
                                DPS = addressEntity.DPS,
                                Lat = addressEntity.Lat,
                                Long = addressEntity.Long,
                                Country = new Domain.Country()
                                {
                                    CountryName = countryEntity.CountryName,
                                    Id = countryEntity.Id,
                                    ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                    ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                                }
                            };
                        }
                    }
                }
            }

            return model;
        }

        public Domain.Store GetByName(string name)
        {
            Domain.Store model = null;

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            where name == s.Name
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        CustomerSiteName = entity.ClientSiteName,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        Telephone = entity.Telephone,
                        TimeZone = entity.TimeZone
                    };

                    // Get the address
                    var addressQuery = from s in entitiesContext.Addresses
                                       where s.Id == entity.AddressId
                                       select s;

                    var addressEntity = addressQuery.FirstOrDefault();

                    if (addressEntity != null)
                    {
                        var countryQuery = from c in entitiesContext.Countries
                                           where c.Id == addressEntity.CountryId
                                           select c;

                        var countryEntity = countryQuery.FirstOrDefault();

                        if (addressEntity != null)
                        {
                            model.Address = new Domain.Address()
                            {
                                Id = addressEntity.Id,
                                Org1 = addressEntity.Org1,
                                Org2 = addressEntity.Org2,
                                Org3 = addressEntity.Org3,
                                Prem1 = addressEntity.Prem1,
                                Prem2 = addressEntity.Prem2,
                                Prem3 = addressEntity.Prem3,
                                Prem4 = addressEntity.Prem4,
                                Prem5 = addressEntity.Prem5,
                                Prem6 = addressEntity.Prem6,
                                RoadNum = addressEntity.RoadNum,
                                RoadName = addressEntity.RoadName,
                                Locality = addressEntity.Locality,
                                Town = addressEntity.Town,
                                County = addressEntity.County,
                                State = addressEntity.State,
                                PostCode = addressEntity.PostCode,
                                DPS = addressEntity.DPS,
                                Lat = addressEntity.Lat,
                                Long = addressEntity.Long,
                                Country = new Domain.Country()
                                {
                                    CountryName = countryEntity.CountryName,
                                    Id = countryEntity.Id,
                                    ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                    ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                                }
                            };
                        }
                    }
                }
            }

            return model;
        }

        public IList<Domain.Store> GetByACSApplicationId(int acsApplicationId)
        {
            IList<Domain.Store> stores = new List<Domain.Store>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typo - EF cleverly removes the S off the end
                            join a in entitiesContext.ACSApplicationSites
                            on s.Id equals a.SiteId
                            where a.ACSApplicationId == acsApplicationId
                            orderby s.Name
                            select s;

                foreach (Store entity in query)
                {
                    Domain.Store model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        CustomerSiteName = entity.ClientSiteName,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        Telephone = entity.Telephone,
                        TimeZone = entity.TimeZone
                    };

                    // Get the address
                    var addressQuery = from s in entitiesContext.Addresses
                                       where s.Id == entity.AddressId
                                       select s;

                    var addressEntity = addressQuery.FirstOrDefault();

                    if (addressEntity != null)
                    {
                        var countryQuery = from c in entitiesContext.Countries
                                           where c.Id == addressEntity.CountryId
                                           select c;

                        var countryEntity = countryQuery.FirstOrDefault();

                        if (addressEntity != null)
                        {
                            model.Address = new Domain.Address()
                            {
                                Id = addressEntity.Id,
                                Org1 = addressEntity.Org1,
                                Org2 = addressEntity.Org2,
                                Org3 = addressEntity.Org3,
                                Prem1 = addressEntity.Prem1,
                                Prem2 = addressEntity.Prem2,
                                Prem3 = addressEntity.Prem3,
                                Prem4 = addressEntity.Prem4,
                                Prem5 = addressEntity.Prem5,
                                Prem6 = addressEntity.Prem6,
                                RoadNum = addressEntity.RoadNum,
                                RoadName = addressEntity.RoadName,
                                Locality = addressEntity.Locality,
                                Town = addressEntity.Town,
                                County = addressEntity.County,
                                State = addressEntity.State,
                                PostCode = addressEntity.PostCode,
                                DPS = addressEntity.DPS,
                                Lat = addressEntity.Lat,
                                Long = addressEntity.Long,
                                Country = new Domain.Country()
                                {
                                    CountryName = countryEntity.CountryName,
                                    Id = countryEntity.Id,
                                    ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                    ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                                }
                            };
                        }
                    }

                    stores.Add(model);
                }
            }

            return stores;
        }

        public IList<Domain.Store> GetAfterDataVersion(int dataVersion)
        {
            List<Domain.Store> models = new List<Domain.Store>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typo - EF cleverly removes the S off the end
                            where s.DataVersion > dataVersion
                            select s;

                foreach (var entity in query)
                {
                    Domain.Store model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        CustomerSiteName = entity.ClientSiteName,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() 
                        { 
                            Id = entity.StoreStatu.Id, 
                            Status = entity.StoreStatu.Status, 
                            Description = entity.StoreStatu.Description 
                        },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        Telephone = entity.Telephone,
                        TimeZone = entity.TimeZone,
                        PaymentProvider = entity.StorePaymentProvider == null ? null :
                            new Domain.StorePaymentProvider()
                            {
                                ClientId = entity.StorePaymentProvider.ClientId,
                                ClientPassword = entity.StorePaymentProvider.ClientPassword,
                                DisplayText = entity.StorePaymentProvider.DisplayText,
                                Id = entity.StorePaymentProvider.Id,
                                ProviderName = entity.StorePaymentProvider.ProviderName
                            }
                    };

                    // Get the address
                    var addressQuery = from s in entitiesContext.Addresses
                                       where s.Id == entity.AddressId
                                       select s;

                    var addressEntity = addressQuery.FirstOrDefault();

                    if (addressEntity != null)
                    {
                        var countryQuery = from c in entitiesContext.Countries
                                           where c.Id == addressEntity.CountryId
                                           select c;

                        var countryEntity = countryQuery.FirstOrDefault();

                        if (addressEntity != null)
                        {
                            model.Address = new Domain.Address()
                            {
                                Id = addressEntity.Id,
                                Org1 = addressEntity.Org1,
                                Org2 = addressEntity.Org2,
                                Org3 = addressEntity.Org3,
                                Prem1 = addressEntity.Prem1,
                                Prem2 = addressEntity.Prem2,
                                Prem3 = addressEntity.Prem3,
                                Prem4 = addressEntity.Prem4,
                                Prem5 = addressEntity.Prem5,
                                Prem6 = addressEntity.Prem6,
                                RoadNum = addressEntity.RoadNum,
                                RoadName = addressEntity.RoadName,
                                Locality = addressEntity.Locality,
                                Town = addressEntity.Town,
                                County = addressEntity.County,
                                State = addressEntity.State,
                                PostCode = addressEntity.PostCode,
                                DPS = addressEntity.DPS,
                                Lat = addressEntity.Lat,
                                Long = addressEntity.Long,
                                Country = new Domain.Country()
                                {
                                    CountryName = countryEntity.CountryName,
                                    Id = countryEntity.Id,
                                    ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                    ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                                }
                            };
                        }
                    }

                    models.Add(model);
                }
            }

            return models;
        }

        public IList<Domain.Store> GetByACSApplicationIdAfterDataVersion(int acsApplicationId, int dataVersion)
        {
            List<Domain.Store> models = new List<Domain.Store>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typo - EF cleverly removes the S off the end
                            join a in entitiesContext.ACSApplicationSites
                            on s.Id equals a.SiteId
                            where a.ACSApplicationId == acsApplicationId
                            && s.DataVersion > dataVersion
                            select s;

                foreach (var entity in query)
                {
                    Domain.Store model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        CustomerSiteName = entity.ClientSiteName,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        Telephone = entity.Telephone,
                        TimeZone = entity.TimeZone
                    };

                    // Get the address
                    var addressQuery = from s in entitiesContext.Addresses
                                       where s.Id == entity.AddressId
                                       select s;

                    var addressEntity = addressQuery.FirstOrDefault();

                    if (addressEntity != null)
                    {
                        var countryQuery = from c in entitiesContext.Countries
                                           where c.Id == addressEntity.CountryId
                                           select c;

                        var countryEntity = countryQuery.FirstOrDefault();

                        if (addressEntity != null)
                        {
                            model.Address = new Domain.Address()
                            {
                                Id = addressEntity.Id,
                                Org1 = addressEntity.Org1,
                                Org2 = addressEntity.Org2,
                                Org3 = addressEntity.Org3,
                                Prem1 = addressEntity.Prem1,
                                Prem2 = addressEntity.Prem2,
                                Prem3 = addressEntity.Prem3,
                                Prem4 = addressEntity.Prem4,
                                Prem5 = addressEntity.Prem5,
                                Prem6 = addressEntity.Prem6,
                                RoadNum = addressEntity.RoadNum,
                                RoadName = addressEntity.RoadName,
                                Locality = addressEntity.Locality,
                                Town = addressEntity.Town,
                                County = addressEntity.County,
                                State = addressEntity.State,
                                PostCode = addressEntity.PostCode,
                                DPS = addressEntity.DPS,
                                Lat = addressEntity.Lat,
                                Long = addressEntity.Long,
                                Country = new Domain.Country()
                                {
                                    CountryName = countryEntity.CountryName,
                                    Id = countryEntity.Id,
                                    ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                    ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                                }
                            };
                        }
                    }

                    models.Add(model);
                }
            }

            return models;
        }
    }
}