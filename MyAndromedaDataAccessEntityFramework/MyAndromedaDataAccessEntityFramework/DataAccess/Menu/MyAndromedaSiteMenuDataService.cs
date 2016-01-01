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
        private readonly IMyAndromedaDbWorkContextAccessor dbWork;

        public MyAndromedaSiteMenuDataService(IMyAndromedaDbWorkContextAccessor dbWork)
        { 
            this.dbWork = dbWork;
        }

        public SiteMenu Create(int andromedaSiteId)
        {
            SiteMenu menu;
            using (var dbContext = NewContext())
            {
                var table = dbContext.SiteMenus;
                var entity = table.Create();

                entity.AndromediaId = andromedaSiteId;
                entity.LastUpdatedUtc = DateTime.UtcNow;
                table.Add(entity);

                dbContext.SaveChanges();
                menu = entity;
            }

            return menu;
        }

        public void Update(SiteMenu siteMenu)
        {
            using (var dbContext = NewContext()) 
            {
                var result = this.GetMenuWithContext(dbContext, siteMenu.AndromediaId);
                
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

                result.SiteMenuFtpBackupUploadTask.Copy(siteMenu.SiteMenuFtpBackupUploadTask);
                result.SiteMenuFtpBackupDownloadTask.Copy(siteMenu.SiteMenuFtpBackupDownloadTask);

                //result.SiteMenuFtpBackup.LastFtpCheckDateUtc = siteMenu.SiteMenuFtpBackup.LastFtpCheckDateUtc;
                //result.SiteMenuFtpBackup.LastDownloadedDateUtc = siteMenu.SiteMenuFtpBackup.LastDownloadedDateUtc;
                //result.SiteMenuFtpBackup.LastUploadedDateUtc = siteMenu.SiteMenuFtpBackup.LastUploadedDateUtc;
                //result.SiteMenuFtpBackup.MenuVersion = siteMenu.SiteMenuFtpBackup.MenuVersion;

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<SiteMenu> List(Expression<Func<SiteMenu, bool>> query)
        {
            IEnumerable<SiteMenu> results = Enumerable.Empty<SiteMenu>();
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.SiteMenus;

                results = table
                    .Include(e=> e.SiteMenuFtpBackupDownloadTask)
                    .Include(e=> e.SiteMenuFtpBackupUploadTask)
                    .Where(query)
                    .ToArray();

            }
            return results;
        }

        public void SetUploadTask(SiteMenu siteMenu, TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.Created:
                    {
                        siteMenu.SiteMenuFtpBackupUploadTask.TryTask = true;
                        siteMenu.SiteMenuFtpBackupUploadTask.TaskComplete = false;
                        siteMenu.SiteMenuFtpBackupUploadTask.LastTryCount = 0;

                        break;
                    }
                case TaskStatus.Running:
                    {
                        siteMenu.SiteMenuFtpBackupUploadTask.LastTryCount++;
                        siteMenu.SiteMenuFtpBackupUploadTask.TaskStarted = true;
                        siteMenu.SiteMenuFtpBackupUploadTask.LastStartedUtc = DateTime.UtcNow;
                        siteMenu.SiteMenuFtpBackupUploadTask.LastTriedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.RanToCompletion:
                    {
                        siteMenu.SiteMenuFtpBackupUploadTask.TryTask = false;
                        siteMenu.SiteMenuFtpBackupUploadTask.TaskStarted = false;
                        siteMenu.SiteMenuFtpBackupUploadTask.TaskComplete = true;
                        siteMenu.SiteMenuFtpBackupUploadTask.LastCompletedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.Faulted:
                    {
                        //reset to run again.
                        siteMenu.SiteMenuFtpBackupUploadTask.TryTask = true;
                        siteMenu.SiteMenuFtpBackupUploadTask.TaskStarted = false;

                        break;
                    }

                default: { break; }
            }

            this.Update(siteMenu);
        }

        public void SetDownloadTask(SiteMenu siteMenu, TaskStatus status)
        {
            switch (status) 
            {
                case TaskStatus.Created: 
                {
                    siteMenu.SiteMenuFtpBackupDownloadTask.LastTryCount = 0;
                    siteMenu.SiteMenuFtpBackupDownloadTask.TryTask = true;
                    siteMenu.SiteMenuFtpBackupDownloadTask.TaskCompleted = false;

                    break; 
                }
                case TaskStatus.Running: 
                {
                    siteMenu.SiteMenuFtpBackupDownloadTask.LastTryCount++;
                    siteMenu.SiteMenuFtpBackupDownloadTask.TaskStarted = true;
                    siteMenu.SiteMenuFtpBackupDownloadTask.LastStartedUtc = DateTime.UtcNow;
                    siteMenu.SiteMenuFtpBackupDownloadTask.LastTriedUtc = DateTime.UtcNow;

                    break;
                }
                case TaskStatus.RanToCompletion: 
                {
                    siteMenu.SiteMenuFtpBackupDownloadTask.TryTask = false;
                    siteMenu.SiteMenuFtpBackupDownloadTask.TaskStarted = false;
                    siteMenu.SiteMenuFtpBackupDownloadTask.LastCompletedUtc = DateTime.UtcNow;

                    break; 
                }
                case TaskStatus.Faulted: 
                {
                    //reset to run again.
                    siteMenu.SiteMenuFtpBackupDownloadTask.TryTask = true;
                    siteMenu.SiteMenuFtpBackupDownloadTask.TaskStarted = false;
                    
                    break;
                }

                default: { break; }
            }

            this.Update(siteMenu);
        }

        public void SetDownloadTask(SiteMenu siteMenu, bool value = true, bool inProgress = false)
        {
            using (var dbContext = NewContext()) 
            {
                var dbItem = this.GetMenuWithContext(dbContext, siteMenu.AndromediaId);

                var downloadTask = dbItem.SiteMenuFtpBackupDownloadTask;
                downloadTask.TryTask = value;
                downloadTask.TaskStarted = inProgress;
                downloadTask.LastTriedUtc = DateTime.UtcNow;
                
                //var dbItem = dbContext.SiteMenuFtpBackups.Where(e => e.Id == siteMenuFtp.Id).Single();
                //dbItem.CheckToDownload = value;
                //dbItem.LastFtpCheckDateUtc = DateTime.UtcNow;
                //siteMenuFtp.CheckToDownload = value;
                //siteMenuFtp.CheckInProgress = inProgress;

                //if (!value && !inProgress) { siteMenuFtp.LastFtpCheckDateUtc = DateTime.UtcNow; }

                //dbContext.SaveChanges();
            }
        }

        //public void SetUploadTaskStarted(SiteMenu siteMenu, bool value = true, bool inProgress = false)
        //{
        //    using (var dbContext = NewContext())
        //    {
        //        var dbItem = dbContext.SiteMenuFtpBackups.Where(e => e.Id == siteMenuFtp.Id).Single();
        //        dbItem.CheckToUpload = value;
        //        siteMenuFtp.CheckToUpload = value;
        //        siteMenuFtp.CheckInProgress = inProgress;

        //        dbContext.SaveChanges();
        //    }
        //}


        //public void SetVersion(int andromedaSiteId, int version)
        //{
        //    using (var dbContext = NewContext()) 
        //    {
        //        var dbItem = this.GetMenuWithContext(dbContext, andromedaSiteId);
        //        dbItem.SiteMenuFtpBackup.MenuVersion = version;

        //        dbContext.SaveChanges();
        //    }
        //}

        //public void SetVersion( siteMenuFtp)
        //{
        //    using (var dbContext = NewContext())
        //    {
        //        var dbItem = dbContext.SiteMenuFtpBackups.Where(e => e.Id == siteMenuFtp.Id).Single();
        //        dbItem.MenuVersion = siteMenuFtp.MenuVersion;

        //        dbContext.SaveChanges();
        //    }
        //}

        public void SetVersion(int andromedaSiteId, int version)
        {
            using (var dbContext = NewContext()) 
            {
                var dbItem = dbContext.SiteMenus.Where(e => e.AndromediaId == andromedaSiteId).SingleOrDefault();

                dbItem.AccessMenuVersion = version;

                dbContext.SaveChanges();
            }
        }

        public SiteMenu GetMenu(int andromedaSiteId)
        {
            using (var dbContext = NewContext())
            {
                var result = this.GetMenuWithContext(dbContext, andromedaSiteId);

                if (result != null)
                {
                    return result;
                }
            }

            //obviously there isn't one already. Need to go make another. 
            return this.Create(andromedaSiteId);
        }

        private static MyAndromedaDbContext NewContext()
        {
            return new MyAndromedaDbContext();
        }

        private SiteMenu GetMenuWithContext(MyAndromedaDbContext dbContext, int andromedaSiteId)
        {
            var table = dbContext.SiteMenus;
            var query = table
                             //.Include(e => e.SiteMenuFtpBackup)
                             .Include(e=> e.SiteMenuFtpBackupDownloadTask)
                             .Include(e=> e.SiteMenuFtpBackupDownloadTask)
                             //.Include(e=> e.SiteMenuPublishTasks)
                             ///.Include(e=> e.
                             .Where(e => e.AndromediaId == andromedaSiteId);

            var result = query.SingleOrDefault();
            if (result == null) 
            {
                this.Create(andromedaSiteId);
            }
            
            //if (result.SiteMenuFtpBackupId == 0 || result.SiteMenuFtpBackupId== null) 
            if(result.SiteMenuFtpBackupUploadTask == null)
            {
                result.SiteMenuFtpBackupUploadTask = new SiteMenuFtpBackupUploadTask();
            }
            if (result.SiteMenuFtpBackupDownloadTask == null) 
            {
                result.SiteMenuFtpBackupDownloadTask = new SiteMenuFtpBackupDownloadTask();
            }
            dbContext.SaveChanges();

            return result;
        }
    }
}