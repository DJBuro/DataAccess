using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreMenuThumbnailsDataService : IStoreMenuThumbnailsDataService
    {
        public IEnumerable<AndroAdminDataAccess.Domain.StoreMenu> GetStoreMenuChangesAfterDataVersion(int fromVersion)
        {
            IEnumerable<AndroAdminDataAccess.Domain.StoreMenu> results = Enumerable.Empty<AndroAdminDataAccess.Domain.StoreMenu>();

            using (var dbContext = new AndroAdminDataAccess.EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreMenus.Include(e=> e.Store);
                var query = table.Where(e => e.DataVersion > fromVersion);
                var result = query.ToArray();

                results = result.Select(e => e.ToDomain()).ToArray();
            }

            return results;
        }

        public IEnumerable<AndroAdminDataAccess.Domain.StoreMenuThumbnails> GetStoreMenuThumbnailChangesAfterDataVersion(int fromVersion)
        {
            using (var dbContext = new AndroAdminDataAccess.EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreMenuThumbnails;
                var query = table.Where(e => e.Version > fromVersion);
                var result = query.ToArray();
                var resultGroup = result.Select(e => {
                    var storeEntity = dbContext.Stores.Single(store => store.Id == e.StoreId);

                    return new[] {
                        new AndroAdminDataAccess.Domain.StoreMenuThumbnails() { 
                            Id = e.Id,
                            Version = e.Version,
                            MenuType = "xml",
                            Data = e.XmlMenuThumbnailData,
                            AndromediaSiteId = storeEntity.AndromedaSiteId,
                            LastUpdate = e.LastUpdate.GetValueOrDefault(DateTime.UtcNow)
                        },
                        new AndroAdminDataAccess.Domain.StoreMenuThumbnails(){
                            Id = e.Id,
                            Version = e.Version,
                            MenuType = "json",
                            Data = e.JsonMenuThumbnailsData,
                            AndromediaSiteId = storeEntity.AndromedaSiteId,
                            LastUpdate = e.LastUpdate.GetValueOrDefault(DateTime.UtcNow)
                        }
                    };
                }).ToArray();

                return resultGroup.SelectMany(e=> e);
            }
        }

    }
    public static class DomainExtensiosn 
    {
        public static Domain.StoreMenu ToDomain(this StoreMenu storeMenu) 
        {
            return new Domain.StoreMenu() 
            {
                Id = storeMenu.Id,
                AndromedaSiteId = storeMenu.Store.AndromedaSiteId,
                LastUpdated = storeMenu.LastUpdated.GetValueOrDefault(),
                MenuData = storeMenu.MenuData,
                MenuType = storeMenu.MenuType,
                Version = storeMenu.Version.GetValueOrDefault()
            };
        }
    }
}
