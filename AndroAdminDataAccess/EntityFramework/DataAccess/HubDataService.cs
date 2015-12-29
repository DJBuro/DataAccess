using AndroAdminDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.EntityFramework.Extensions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class HubDataService : IHubDataService, IHubResetDataService
    {

        
        public void ResetStore(int storeId)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            { 
                //var result = dbContext.Stores.Single(e=> e.Id == storeId);

                var entity = dbContext.StoreHubResets.Create();

                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                entity.DataVersion = newVersion;
                entity.AddedOn = DateTime.UtcNow;
                entity.StoreId = storeId;
                
                dbContext.StoreHubResets.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public IEnumerable<AndroAdminDataAccess.Domain.HubItem> GetAfterDataVersion(int fromVersion)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var dataResult = dbContext.HubAddresses
                    .Where(e => e.DataVersion > fromVersion)
                    .WhereValidItems()
                    .ToArray();

                var results = dataResult.Select(e => e.ToDomain()).ToList();

                return results;
            }
        }

        public void Add(AndroAdminDataAccess.Domain.HubItem domainModel)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.HubAddresses;
                var entity = table.Create();

                entity.Id = Guid.NewGuid();
                entity.Active = domainModel.Active;
                entity.Address = domainModel.Address;
                entity.DataVersion = 0;
                entity.Name = domainModel.Name;
                entity.Removed = domainModel.Removed;

                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                entity.DataVersion = newVersion;

                table.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Update(AndroAdminDataAccess.Domain.HubItem domainModel)
        {
            using(var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HubAddresses;
                var entity = table.Single(e => e.Id == domainModel.Id);

                entity.Name = domainModel.Name;
                entity.Removed = domainModel.Removed;
                entity.Address = domainModel.Address;
                entity.Active = domainModel.Active;

                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                entity.DataVersion = newVersion;

                dbContext.SaveChanges();
            }
        }

        public void Remove(AndroAdminDataAccess.Domain.HubItem domainModel)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HubAddresses;
                var entity = table.Single(e => e.Id == domainModel.Id);

                entity.Removed = true;

                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                entity.DataVersion = newVersion;

                dbContext.SaveChanges();
            }
        }

        public AndroAdminDataAccess.Domain.HubItem GetHub(Guid id)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HubAddresses;
                var data = table.Where(e => e.Id == id).ToArray();
                var result = data.Select(e => e.ToDomain()).SingleOrDefault();

                return result;
            }
        }

        public IEnumerable<AndroAdminDataAccess.Domain.HubItem> GetHubs()
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var data = dbContext.HubAddresses
                    .WhereValidItems()
                    .ToArray();
                
                var results = data.Select(e => e.ToDomain()).ToArray();

                return results;
            }
        }

        public IEnumerable<AndroAdminDataAccess.Domain.Store> GetResetsAfterDataVersion(int fromVersion)
        {

            IEnumerable<AndroAdminDataAccess.Domain.Store> results; 
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreHubResets;
                var query = table.Where(e => e.DataVersion >= fromVersion);
                var result = query.Select(e => e.Store).ToArray();

                results = result.Select(e=> e.ToDomain()).ToArray();
            }

            return results;
        }
    }

    public static class HubAddressExtensions 
    {
        public static IQueryable<HubAddress> WhereValidItems(this IQueryable<HubAddress> hubAddressQuery) 
        {
            return hubAddressQuery.Where(e => !e.Removed);
        }
    }
}
