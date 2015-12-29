using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
                var result = dbContext.GetMenuWithTasks(siteMenu.AndromediaId);
                
                /* update menu details  */ 
                result.LastUpdatedUtc = siteMenu.LastUpdatedUtc;
                result.DataVersion = siteMenu.DataVersion;
                result.AccessMenuVersion = siteMenu.AccessMenuVersion;

                /* update ftp task sections */
                if (result.SiteMenuFtpBackupDownloadTaskId == 0) 
                {
                    result.SiteMenuFtpBackupDownloadTask = new SiteMenuFtpBackupDownloadTask();
                }
                if (result.SiteMenuFtpBackupUploadTaskId == 0) 
                {
                    result.SiteMenuFtpBackupUploadTask = new SiteMenuFtpBackupUploadTask();
                }
                if (result.SiteMenuPublishTaskId == 0) 
                {
                    result.SiteMenuPublishTask = new SiteMenuPublishTask();
                }

                result.SiteMenuFtpBackupUploadTask.Copy(siteMenu.SiteMenuFtpBackupUploadTask);
                result.SiteMenuFtpBackupDownloadTask.Copy(siteMenu.SiteMenuFtpBackupDownloadTask);
                result.SiteMenuPublishTask.Copy(siteMenu.SiteMenuPublishTask);

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