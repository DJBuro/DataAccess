using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreDAO : IStoreDAO
    {

        public IEnumerable<Domain.Store> GetAll()
        {
            List<Domain.Store> models = new List<Domain.Store>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Stores
                            .Include("StoreStatu") // No this isn't a typo - EF cleverly removes the S off the end
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
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description }
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public void Add(Domain.Store store)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                Store entity = new Store()
                {
                    Name = store.Name,
                    AndromedaSiteId = store.AndromedaSiteId,
                    CustomerSiteId = store.CustomerSiteId,
                    LastFTPUploadDateTime = store.LastFTPUploadDateTime,
                    StoreStatusId = store.StoreStatus.Id
                };

                entitiesContext.AddToStores(entity);
                entitiesContext.SaveChanges();
            }
        }

        public void Update(Domain.Store store)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Stores
                            where store.Id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = store.Name;
                    entity.AndromedaSiteId = store.AndromedaSiteId;
                    entity.CustomerSiteId = store.CustomerSiteId;
                    entity.LastFTPUploadDateTime = store.LastFTPUploadDateTime;
                    entity.StoreStatusId = store.StoreStatus.Id;

                    entitiesContext.SaveChanges();
                }
            }
        }

        public Domain.Store GetById(int id)
        {
            Domain.Store store = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
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
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description }
                    };
                }
            }

            return store;
        }

        public Domain.Store GetByAndromedaId(int id)
        {
            Domain.Store store = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
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
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description }
                    };
                }
            }

            return store;
        }

        public Domain.Store GetByName(string name)
        {
            Domain.Store store = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
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
                        StoreStatus = new Domain.StoreStatus() { Id = entity.StoreStatu.Id, Status = entity.StoreStatu.Status, Description = entity.StoreStatu.Description }
                    };
                }
            }

            return store;
        }
    }
}