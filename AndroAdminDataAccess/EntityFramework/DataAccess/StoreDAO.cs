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

        public IList<Domain.Store> GetAll()
        {
            List<Domain.Store> models = new List<Domain.Store>();

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typo - EF cleverly removes the S off the end
                            orderby s.Name
                            select s;

                foreach (var entity in query)
                {
                    Domain.Store model = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName,
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

        public void Add(Domain.Store store)
        {
            using (System.Transactions.TransactionScope ts = new TransactionScope())
            {
                //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    if (store.Address.Country == null)
                    {
                        throw new ArgumentNullException("Address country cannot be null");
                    }

                    //entitiesContext.Connection.Open();
                    entitiesContext.Database.Connection.Open();

                    using (DbTransaction transaction = entitiesContext.Database.Connection.BeginTransaction())
                    {
                        // Get the next data version (see comments inside the function)
                        int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

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
                            LastFTPUploadDateTime = store.LastFTPUploadDateTime,
                            StoreStatusId = store.StoreStatus.Id,
                            DataVersion = newVersion,
                            ExternalId = store.ExternalSiteId,
                            ExternalSiteName = store.ExternalSiteName,
                            ClientSiteName = store.ClientSiteName,
                            AddressId = addressEntity.Id,
                            Telephone = store.Telephone,
                            TimeZone = store.TimeZone
                        };

                        entitiesContext.Stores.Add(storeEntity);
                        entitiesContext.SaveChanges();

                        // Fin...
                        transaction.Commit();
                    }
                }
            }
        }

        public void Update(Domain.Store store)
        {
            using (System.Transactions.TransactionScope ts = new TransactionScope())
            {
                //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    //entitiesContext.Connection.Open();
                    entitiesContext.Database.Connection.Open();

                    using (DbTransaction transaction = entitiesContext.Database.Connection.BeginTransaction())
                    {
                        // Get the next data version (see comments inside the function)
                        int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

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
                            transaction.Rollback();
                            return;
                        }

                        // Update the store
                        storeEntity.Name = store.Name;
                        storeEntity.AndromedaSiteId = store.AndromedaSiteId;
                        storeEntity.CustomerSiteId = store.CustomerSiteId;
                        storeEntity.LastFTPUploadDateTime = store.LastFTPUploadDateTime;
                        storeEntity.StoreStatusId = store.StoreStatus.Id;
                        storeEntity.DataVersion = newVersion;
                        storeEntity.ExternalId = store.ExternalSiteId;
                        storeEntity.ExternalSiteName = storeEntity.ExternalSiteName;
                        storeEntity.Telephone = storeEntity.Telephone;
                        storeEntity.TimeZone = storeEntity.TimeZone;

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
                            // Update the existing address
                            addressEntity.County = storeEntity.Address.County;
                            addressEntity.DPS = storeEntity.Address.DPS;
                            addressEntity.Lat = storeEntity.Address.Lat;
                            addressEntity.Locality = storeEntity.Address.Locality;
                            addressEntity.Long = storeEntity.Address.Long;
                            addressEntity.Org1 = storeEntity.Address.Org1;
                            addressEntity.Org2 = storeEntity.Address.Org2;
                            addressEntity.Org3 = storeEntity.Address.Org3;
                            addressEntity.PostCode = storeEntity.Address.PostCode;
                            addressEntity.Prem1 = storeEntity.Address.Prem1;
                            addressEntity.Prem2 = storeEntity.Address.Prem2;
                            addressEntity.Prem3 = storeEntity.Address.Prem3;
                            addressEntity.Prem4 = storeEntity.Address.Prem4;
                            addressEntity.Prem5 = storeEntity.Address.Prem5;
                            addressEntity.Prem6 = storeEntity.Address.Prem6;
                            addressEntity.RoadName = storeEntity.Address.RoadName;
                            addressEntity.RoadNum = storeEntity.Address.RoadNum;
                            addressEntity.State = storeEntity.Address.State;
                            addressEntity.Town = storeEntity.Address.Town;
                            addressEntity.CountryId = country.Id;
                            addressEntity.DataVersion = newVersion;
                        }

                        entitiesContext.SaveChanges();

                        // Fin...
                        transaction.Commit();
                    }
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
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName,
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

        public Domain.Store GetByAndromedaId(int id)
        {
            Domain.Store model = null;

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
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
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName,
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

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
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
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName,
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

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
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
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName,
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

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
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
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName,
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

        public IList<Domain.Store> GetByACSApplicationIdAfterDataVersion(int acsApplicationId, int dataVersion)
        {
            List<Domain.Store> models = new List<Domain.Store>();

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
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
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName,
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