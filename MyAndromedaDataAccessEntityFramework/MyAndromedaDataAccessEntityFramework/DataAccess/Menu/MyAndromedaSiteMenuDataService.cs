using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public class MyAndromedaSiteMenuDataService : IMyAndromedaSiteMenuDataService 
    {
        public MyAndromedaSiteMenuDataService()
        { 
        }

        public SiteMenu GetMenu(int andromedaSiteId)
        {
            SiteMenu result = null;

            using (var dbContext = new MyAndromedaDbContext())
            {
                //will go off and create one if needed
                result = dbContext.GetMenuWithTasks(andromedaSiteId);
            }

            return result;
        }

        public IEnumerable<SiteMenu> List(Expression<Func<SiteMenu, bool>> query)
        {
            IEnumerable<SiteMenu> results = Enumerable.Empty<SiteMenu>();
            
            using (var dbContext = new MyAndromedaDbContext())
            {
                var tableQuery = dbContext.QueryMenusWithTasks(query);

                results = tableQuery.ToArray();
            }

            return results;
        }

        public void Delete(SiteMenu siteMenu)
        {
            using (var dbContext = new MyAndromedaDbContext()) 
            {
                var tableQuery = dbContext.SiteMenus.Where(e=> e.Id == siteMenu.Id).FirstOrDefault();

                if (tableQuery != null) 
                {
                    dbContext.SiteMenus.Remove(tableQuery);
                    dbContext.SaveChanges();
                }
            }
        }

        public SiteMenu Create(int andromedaSiteId)
        {
            SiteMenu menu;

            using (var dbContext = new MyAndromedaDbContext())
            {
                menu = dbContext.CreateNewMenuForAndromedaId(andromedaSiteId);
            }

            return menu;
        }

        public void Update(SiteMenu siteMenu)
        {
            using (var dbContext = new MyAndromedaDbContext()) 
            {
                var entity = dbContext.GetMenuWithTasks(siteMenu.AndromediaId);
                
                /* update menu details  */ 
                entity.LastUpdatedUtc = siteMenu.LastUpdatedUtc;
                entity.DataVersion = siteMenu.DataVersion;
                entity.AccessMenuVersion = siteMenu.AccessMenuVersion;

                /* update ftp task sections */
                if (entity.SiteMenuFtpBackupDownloadTaskId == 0) 
                {
                    entity.SiteMenuFtpBackupDownloadTask = new SiteMenuFtpBackupDownloadTask();
                }
                if (entity.SiteMenuFtpBackupUploadTaskId == 0) 
                {
                    entity.SiteMenuFtpBackupUploadTask = new SiteMenuFtpBackupUploadTask();
                }
                //update sync updates - thumbnails 
                if (entity.SiteMenuPublishTaskId == 0) 
                {
                    entity.SiteMenuPublishTask = new SiteMenuPublishTask();
                }

                entity.SiteMenuFtpBackupUploadTask.Copy(siteMenu.SiteMenuFtpBackupUploadTask);
                entity.SiteMenuFtpBackupDownloadTask.Copy(siteMenu.SiteMenuFtpBackupDownloadTask);
                entity.SiteMenuPublishTask.Copy(siteMenu.SiteMenuPublishTask);

                dbContext.SaveChanges();
            }
        }

        

        public void SetVersion(SiteMenu siteMenu)
        {
            using (var dbContext = new MyAndromedaDbContext())
            {
                var dbItem = dbContext.SiteMenus.Where(e => e.AndromediaId == siteMenu.AndromediaId).SingleOrDefault();

                dbItem.AccessMenuVersion = siteMenu.AccessMenuVersion;

                dbContext.SaveChanges();
            }
        }
    }
}