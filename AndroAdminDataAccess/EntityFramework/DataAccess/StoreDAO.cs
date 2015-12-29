using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Data.Common;
using System.Globalization;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreDAO : IStoreDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.Store> GetAll()
        {
            List<Domain.Store> models = new List<Domain.Store>();

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
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
                        ClientSiteName = entity.ClientSiteName
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
                            model.Country = new Domain.Country()
                            {
                                CountryName = countryEntity.CountryName,
                                Id = countryEntity.Id,
                                ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
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
            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                entitiesContext.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Connection.BeginTransaction())
                {
                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

                    Store entity = new Store()
                    {
                        Name = store.Name, // Andro site name
                        AndromedaSiteId = store.AndromedaSiteId,
                        CustomerSiteId = store.CustomerSiteId,
                        LastFTPUploadDateTime = store.LastFTPUploadDateTime,
                        StoreStatusId = store.StoreStatus.Id,
                        DataVersion = newVersion,
                        ExternalId = store.ExternalSiteId,
                        ExternalSiteName = store.ExternalSiteName,
                        ClientSiteName = store.ClientSiteName
                    };

                    entitiesContext.AddToStores(entity);
                    entitiesContext.SaveChanges();

                    // Fin...
                    transaction.Commit();
                }
            }
        }

        public void Update(Domain.Store store)
        {
            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                entitiesContext.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Connection.BeginTransaction())
                {
                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

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
                            County = "",
                            DPS = "",
                            Lat = 0,
                            Locality = "",
                            Long = 0,
                            Org1 = "",
                            Org2 = "",
                            Org3 = "",
                            PostCode = "",
                            Prem1 = "",
                            Prem2 = "",
                            Prem3 = "",
                            Prem4 = "",
                            Prem5 = "",
                            Prem6 = "",
                            RoadName = "",
                            RoadNum = "",
                            State = "",
                            Town = "",
                            CountryId = store.Country.Id
                        };

                        entitiesContext.Addresses.AddObject(addressEntity);
                    }
                    else
                    {
                        // Update the existing address
                        addressEntity.CountryId = store.Country.Id;
                    }

                    entitiesContext.SaveChanges();

                    // Fin...
                    transaction.Commit();
                }
            }
        }

        public Domain.Store GetById(int id)
        {
            Domain.Store store = null;

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.Stores
                            where id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    store = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName
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
                            store.Country = new Domain.Country()
                            {
                                CountryName = countryEntity.CountryName,
                                Id = countryEntity.Id,
                                ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                            };
                        }
                    }
                }
            }

            return store;
        }

        public Domain.Store GetByAndromedaId(int id)
        {
            Domain.Store store = null;

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.Stores
                            where id == s.AndromedaSiteId
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    store = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName
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
                            store.Country = new Domain.Country()
                            {
                                CountryName = countryEntity.CountryName,
                                Id = countryEntity.Id,
                                ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                            };
                        }
                    }
                }
            }

            return store;
        }

        public Domain.Store GetByName(string name)
        {
            Domain.Store store = null;

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.Stores
                            where name == s.Name
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    store = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName
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
                            store.Country = new Domain.Country()
                            {
                                CountryName = countryEntity.CountryName,
                                Id = countryEntity.Id,
                                ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                            };
                        }
                    }
                }
            }

            return store;
        }

        public IList<Domain.Store> GetByACSApplicationId(int acsApplicationId)
        {
            IList<Domain.Store> stores = new List<Domain.Store>();

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typo - EF cleverly removes the S off the end
                            join a in entitiesContext.ACSApplicationSites
                            on s.Id equals a.SiteId
                            where a.ACSApplicationId == acsApplicationId
                            orderby s.Name
                            select s;

                foreach (Store entity in query)
                {
                    Domain.Store store = new Domain.Store()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        AndromedaSiteId = entity.AndromedaSiteId,
                        CustomerSiteId = entity.CustomerSiteId,
                        LastFTPUploadDateTime = entity.LastFTPUploadDateTime,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description },
                        ExternalSiteId = entity.ExternalId,
                        ExternalSiteName = entity.ExternalSiteName,
                        ClientSiteName = entity.ClientSiteName
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
                            store.Country = new Domain.Country()
                            {
                                CountryName = countryEntity.CountryName,
                                Id = countryEntity.Id,
                                ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
                            };
                        }
                    }

                    stores.Add(store);
                }
            }

            return stores;
        }

        public IList<Domain.Store> GetAfterDataVersion(int dataVersion)
        {
            List<Domain.Store> models = new List<Domain.Store>();

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
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
                        ClientSiteName = entity.ClientSiteName
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
                            model.Country = new Domain.Country()
                            {
                                CountryName = countryEntity.CountryName,
                                Id = countryEntity.Id,
                                ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
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

            using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            {
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
                        ClientSiteName = entity.ClientSiteName
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
                            model.Country = new Domain.Country()
                            {
                                CountryName = countryEntity.CountryName,
                                Id = countryEntity.Id,
                                ISO3166_1_alpha_2 = countryEntity.ISO3166_1_alpha_2,
                                ISO3166_1_numeric = countryEntity.ISO3166_1_numeric
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