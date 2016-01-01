using System;
using System.Collections.Generic;
using AndroAdminDataAccess.DataAccess;
using System.Linq;
using AndroAdminDataAccess.EntityFramework.Extensions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreHubDataService : IStoreHubDataService 
    {
        public void AddTo(AndroAdminDataAccess.Domain.Store storeDomainModel, AndroAdminDataAccess.Domain.HubItem hubDomainModel)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var hubRecord = dbContext.HubAddresses.SingleOrDefault(e => e.Id == hubDomainModel.Id);
                var storeRecord = dbContext.Stores.SingleOrDefault(e=> e.Id == storeDomainModel.Id);
                
                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                hubRecord.DataVersion = newVersion;
                hubRecord.Stores.Add(storeRecord);

                dbContext.SaveChanges();
            }
        }

        public void RemoveFrom(AndroAdminDataAccess.Domain.Store storeDomainModel, AndroAdminDataAccess.Domain.HubItem hubDomainModel)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var hubRecord = dbContext.HubAddresses.SingleOrDefault(e => e.Id == hubDomainModel.Id);
                var storeRecord = dbContext.Stores.SingleOrDefault(e => e.Id == storeDomainModel.Id);
                
                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                hubRecord.DataVersion = newVersion;
                hubRecord.Stores.Remove(storeRecord);

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<AndroAdminDataAccess.Domain.HubItem> GetHubItems()
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var data = dbContext.HubAddresses.ToArray();
                var results = data.Select(e => e.ToDomain()).ToArray();

                return results;
            }
        }

        public IEnumerable<AndroAdminDataAccess.Domain.StoreHub> GetSitesUsingHub(Guid id)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var hub = dbContext.HubAddresses.Single(e=> e.Id == id);
                var hubDomainModel = hub.ToDomain();
                
                var stores = hub.Stores;
                var results = stores.Select(e=> e.ToStoreHubDomainModel(hubDomainModel)).ToArray();

                return results;
            }
        }

        public IEnumerable<AndroAdminDataAccess.Domain.StoreHub> GetSelectedHubs(AndroAdminDataAccess.Domain.Store storeDomainModel)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var record = dbContext.Stores.Single(e => e.Id == storeDomainModel.Id);
                var data = record.HubAddresses.ToArray();

                var results = data.Select(e => new AndroAdminDataAccess.Domain.StoreHub() { 
                    HubAddressId = e.Id,
                    StoreExternalId = record.ExternalId
                }).ToArray();

                return results;
            }
        }
    }
}