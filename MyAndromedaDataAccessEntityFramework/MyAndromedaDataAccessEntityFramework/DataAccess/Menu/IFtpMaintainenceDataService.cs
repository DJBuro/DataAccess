using System;
using System.Collections.Generic;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System.Linq;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IFtpMaintainenceDataService : IDependency 
    {
        /// <summary>
        /// Resets the upload tasks.
        /// </summary>
        /// <param name="notFinishedBy">The not finished by.</param>
        IEnumerable<SiteMenu> ResetUploadTasks(DateTime notFinishedBy);

        /// <summary>
        /// Resets the download tasks.
        /// </summary>
        /// <param name="notUploadedBy">The not uploaded by.</param>
        IEnumerable<SiteMenu> ResetDownloadTasks(DateTime notUploadedBy);
    }

    public class FtpMaintainenceDataService : IFtpMaintainenceDataService 
    {
        public IEnumerable<SiteMenu> ResetUploadTasks(DateTime notFinishedBy)
        {
            var results = Enumerable.Empty<SiteMenu>();

            using (var dbContext = new MyAndromedaDbContext())
            {
                var query = dbContext.QueryMenusWithTasks(e => 
                    !e.SiteMenuFtpBackupUploadTask.TaskComplete && 
                    e.SiteMenuFtpBackupUploadTask.TaskStarted && 
                    e.SiteMenuFtpBackupUploadTask.LastTriedUtc < notFinishedBy
                );

                results = query.ToArray();

                foreach (var menu in results)
                {
                    menu.SiteMenuFtpBackupUploadTask.TaskStarted = false;
                    menu.SiteMenuFtpBackupUploadTask.TryTask = true;
                }

                dbContext.SaveChanges();
            }

            return results;
        }

        public IEnumerable<SiteMenu> ResetDownloadTasks(DateTime notFinishedBy)
        {
            var results = Enumerable.Empty<SiteMenu>();

            using (var dbContext = new MyAndromedaDbContext())
            {
                var query = dbContext.QueryMenusWithTasks(
                    e => 
                        !e.SiteMenuFtpBackupDownloadTask.TaskCompleted &&
                        e.SiteMenuFtpBackupDownloadTask.TaskStarted &&
                        e.SiteMenuFtpBackupDownloadTask.LastTriedUtc < notFinishedBy
                );
                
                results = query.ToArray();

                foreach (var menu in results)
                {
                    menu.SiteMenuFtpBackupDownloadTask.TaskStarted = false;
                    menu.SiteMenuFtpBackupDownloadTask.TryTask = true;
                }

                dbContext.SaveChanges();
            }

            return results;
        }
    }
}