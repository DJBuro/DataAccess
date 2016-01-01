using System;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaMenuSyncDataService : IDependency 
    {
        /// <summary>
        /// Syncs the menu thumbnails.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <param name="xml">The XML.</param>
        /// <param name="json">The json.</param>
        void SyncMenuThumbnails(int andromedaSiteId, string xml, string json);

        /// <summary>
        /// Check if the 'compiled json or xml data is out of date.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        bool AnyPointSyncing(int andromedaSiteId);
    }

    public class MyAndromedaMenuSyncDataService : IMyAndromedaMenuSyncDataService 
    {
        public void SyncMenuThumbnails(int andromedaSiteId, string xml, string json)
        {
            using (var androAdminDbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
            {
                var menuThumbnailTable = androAdminDbContext.StoreMenuThumbnails;
                var query = menuThumbnailTable.Where(e => e.Store.AndromedaSiteId == andromedaSiteId);
                var menuThumbnailResult = //already exist? 
                query.SingleOrDefault() ?? this.Create(androAdminDbContext, andromedaSiteId);

                menuThumbnailResult.LastUpdate = DateTime.UtcNow;
                menuThumbnailResult.XmlMenuThumbnailData = xml;
                menuThumbnailResult.JsonMenuThumbnailsData = json;

                //update version for cloud sync
                menuThumbnailResult.Version = Model.DataVersionHelper.GetNextDataVersion(androAdminDbContext);

                androAdminDbContext.SaveChanges();
            }
        }

        public bool AnyPointSyncing(int andromedaSiteId)
        {
            bool result = false;
            using (var myAndromediaDbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var menuTable = myAndromediaDbContext.SiteMenus;
                var menuTableQuery = menuTable.Where(e => e.AndromediaId == andromedaSiteId);
                var menuResult = menuTableQuery.SingleOrDefault();

                if (menuResult == null)
                {
                    return false;
                }

                using (var androAdminDbContex = new Model.AndroAdmin.AndroAdminDbContext()) 
                {
                    var menuThumbnailTable = androAdminDbContex.StoreMenuThumbnails;
                    var query = menuThumbnailTable.Where(e => e.Store.AndromedaSiteId == andromedaSiteId);
                    var menuThumbnailResult = query.SingleOrDefault();

                    //no sync record yet 
                    if (menuThumbnailResult == null)
                    {
                        return true;
                    }

                    result = menuThumbnailResult.LastUpdate < menuResult.LastUpdated; 
                }
            }

            return result;
        }

        private Model.AndroAdmin.StoreMenuThumbnail Create(Model.AndroAdmin.AndroAdminDbContext dbContext, int andromediaSiteId) 
        {
            var table = dbContext.StoreMenuThumbnails;
            var entity = table.Add(table.Create());

            entity.XmlMenuThumbnailData = string.Empty;
            entity.JsonMenuThumbnailsData = string.Empty;

            entity.Store = dbContext.Stores.Single(e => e.AndromedaSiteId == andromediaSiteId);

            dbContext.SaveChanges();
            return entity;
        }
    }
}