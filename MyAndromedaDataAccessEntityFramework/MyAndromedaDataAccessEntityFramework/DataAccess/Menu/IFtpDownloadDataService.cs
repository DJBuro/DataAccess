using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IFtpDownloadDataService : IDependency 
    {
        /// <summary>
        /// Sets the download flag.
        /// </summary>
        /// <param name="siteMenuFtp">The site menu FTP.</param>
        /// <param name="value">The value.</param>
        void SetDownloadTaskStatus(SiteMenu siteMenu, TaskStatus status);

        /// <summary>
        /// Sets the last download date.
        /// </summary>
        /// <param name="siteMenu">The site menu.</param>
        /// <param name="dateUtc">The date UTC.</param>
        void SetLastDownloadDate(SiteMenu siteMenu, DateTime dateUtc);

        /// <summary>
        /// Found a the new publish date on the ftp.
        /// </summary>
        /// <param name="siteMenu">The site menu.</param>
        /// <param name="menuPublishDate">The menu publish date.</param>
        void FoundNewPublishDate(SiteMenu siteMenu, DateTime? menuPublishDate);
    }

    public class FtpDownloadDataService : IFtpDownloadDataService 
    {
        private readonly IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService; 

        public FtpDownloadDataService(IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService)
        {
            this.myAndromedaSiteMenuDataService = myAndromedaSiteMenuDataService;
        }

        public void SetLastDownloadDate(SiteMenu siteMenu, DateTime dateUtc)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var downloadContext = dbContext.SiteMenuFtpBackupDownloadTasks.Single(e => e.SiteMenus.Any(menu => menu.Id == siteMenu.Id));

                downloadContext.LastDownloadedDateUtc = dateUtc;

                dbContext.SaveChanges();
            }
        }

        public void FoundNewPublishDate(SiteMenu siteMenu, DateTime? menuPublishDate)
        {
            siteMenu.SiteMenuPublishTask.LastKnownFtpSitePublish = menuPublishDate;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var publishContext = dbContext.SiteMenuPublishTasks.Single(e => e.SiteMenus.Any(menu => menu.Id == siteMenu.Id));
                
                publishContext.LastKnownFtpSitePublish = menuPublishDate;

                dbContext.SaveChanges();
            }
        }

        public void SetDownloadTaskStatus(SiteMenu siteMenu, TaskStatus status)
        {
            var downloadTask = siteMenu.SiteMenuFtpBackupDownloadTask;

            switch (status)
            {
                case TaskStatus.Created:
                    {
                        downloadTask.LastTryCount = 0;
                        downloadTask.TryTask = true;
                        downloadTask.TaskCompleted = false;

                        break;
                    }
                case TaskStatus.Running:
                    {
                        downloadTask.LastTryCount++;
                        downloadTask.TaskStarted = true;
                        downloadTask.LastStartedUtc = DateTime.UtcNow;
                        downloadTask.LastTriedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.RanToCompletion:
                    {
                        downloadTask.TryTask = false;
                        downloadTask.TaskStarted = false;
                        downloadTask.TaskCompleted = true;
                        downloadTask.LastCompletedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.Faulted:
                    {
                        //reset to run again.
                        downloadTask.TryTask = true;
                        downloadTask.TaskStarted = false;

                        break;
                    }

                default: { break; }
            }

            this.myAndromedaSiteMenuDataService.Update(siteMenu);
        }
    }
}