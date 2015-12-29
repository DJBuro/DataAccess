using System.Threading.Tasks;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMenuPubishDataService : IDependency 
    {
        /// <summary>
        /// Sets the acs upload menu data task status.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <param name="status">The status.</param>
        void SetAcsUploadMenuDataTaskStatus(SiteMenu menu, TaskStatus status);
    }

    public class MenuPubishDataService : IMenuPubishDataService 
    {
        private readonly IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService;

        public MenuPubishDataService(IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService)
        {
            this.myAndromedaSiteMenuDataService = myAndromedaSiteMenuDataService;
        }

        public void SetAcsUploadMenuDataTaskStatus(SiteMenu menu, TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.Created:
                    {
                        menu.SiteMenuFtpBackupUploadTask.LastTryCount = 0;
                        menu.SiteMenuFtpBackupUploadTask.TryTask = true;
                        menu.SiteMenuFtpBackupUploadTask.TaskComplete = false;

                        break;
                    }
                case TaskStatus.Running:
                    {
                        menu.SiteMenuFtpBackupUploadTask.LastTryCount++;
                        menu.SiteMenuFtpBackupUploadTask.TaskStarted = true;
                        menu.SiteMenuFtpBackupUploadTask.LastStartedUtc = DateTime.UtcNow;
                        menu.SiteMenuFtpBackupUploadTask.LastTriedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.RanToCompletion:
                    {
                        menu.SiteMenuFtpBackupUploadTask.TryTask = false;
                        menu.SiteMenuFtpBackupUploadTask.TaskStarted = false;
                        menu.SiteMenuFtpBackupUploadTask.TaskComplete = true;
                        menu.SiteMenuFtpBackupUploadTask.LastCompletedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.Faulted:
                    {
                        //reset to run again.
                        menu.SiteMenuFtpBackupUploadTask.TryTask = true;
                        menu.SiteMenuFtpBackupUploadTask.TaskStarted = false;

                        break;
                    }

                default: { break; }
            }

            this.myAndromedaSiteMenuDataService.Update(menu);
        }
    }
}