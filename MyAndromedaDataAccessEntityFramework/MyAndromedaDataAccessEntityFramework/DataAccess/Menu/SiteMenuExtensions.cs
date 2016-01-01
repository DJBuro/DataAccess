using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public static class SiteMenuExtensions 
    {
        public static SiteMenu CreateNewMenuForAndromedaId(this MyAndromedaDbContext dbContext, int andromedaSiteId) 
        {
            var table = dbContext.SiteMenus;

            if (table.Any(e => e.AndromediaId == andromedaSiteId)) 
            {
                throw new InvalidOperationException("Menu already exists in the database, i cant create it");
            }

            var entity = table.Create();

            entity.Id = Guid.NewGuid();
            entity.SiteMenuFtpBackupUploadTask = new SiteMenuFtpBackupUploadTask();
            entity.SiteMenuFtpBackupDownloadTask = new SiteMenuFtpBackupDownloadTask();
            entity.SiteMenuPublishTask = new SiteMenuPublishTask();

            entity.AndromediaId = andromedaSiteId;
            entity.LastUpdatedUtc = DateTime.UtcNow;

            table.Add(entity);

            dbContext.SaveChanges();

            return entity;
        }

        public static IEnumerable<SiteMenu> QueryMenusWithTasks(this MyAndromedaDbContext dbContext, Expression<Func<SiteMenu, bool>> query) 
        { 
            var table = dbContext.SiteMenus;
            var tableQuery = table
                                  .Include(e => e.SiteMenuFtpBackupDownloadTask)
                                  .Include(e => e.SiteMenuFtpBackupUploadTask)
                                  .Include(e => e.SiteMenuPublishTask)
                                  .Where(query)
                                  .ToArray();

            foreach (var item in tableQuery) 
            {
                dbContext.FixRelatedTables(item); 
            }

            return tableQuery;
        }

        private static void FixRelatedTables(this MyAndromedaDbContext dbContext, SiteMenu menu) 
        {
            bool changes = false;

            if (menu.SiteMenuFtpBackupUploadTaskId.GetValueOrDefault() == 0)
            {
                menu.SiteMenuFtpBackupUploadTask = dbContext.SiteMenuFtpBackupUploadTasks.Create();
                changes = true;
            }
            if (menu.SiteMenuFtpBackupDownloadTaskId.GetValueOrDefault() == 0)
            {
                menu.SiteMenuFtpBackupDownloadTask = dbContext.SiteMenuFtpBackupDownloadTasks.Create();
                changes = true;
            }
            if (menu.SiteMenuPublishTaskId.GetValueOrDefault() == 0)
            {
                menu.SiteMenuPublishTask = dbContext.SiteMenuPublishTasks.Create();
                changes = true;
            }

            if (changes)
            {
                dbContext.SaveChanges();
            }
        }

        public static SiteMenu GetMenuWithTasks(this MyAndromedaDbContext dbContext, int andromedaSiteId)
        {
            var query = dbContext.QueryMenusWithTasks(e => e.AndromediaId == andromedaSiteId);

            var result = query.SingleOrDefault();
            if (result == null)
            {
                result = dbContext.CreateNewMenuForAndromedaId(andromedaSiteId);
            }

            return result;
        }
    }
}