using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IFtpUploadDataService : IDependency 
    {
        /// <summary>
        /// Sets the upload flag.
        /// </summary>
        /// <param name="siteMenuFtp">The site menu FTP.</param>
        /// <param name="value">The value.</param>
        void SetUploadTaskStatus(SiteMenu siteMenu, TaskStatus status);
    }

    public class FtpUploadDataService : IFtpUploadDataService 
    {
        private readonly IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService; 

        public FtpUploadDataService(IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService)
        {
            this.myAndromedaSiteMenuDataService = myAndromedaSiteMenuDataService;
        }

        public void SetUploadTaskStatus(SiteMenu siteMenu, TaskStatus status)
        {
            var uploadTask = siteMenu.SiteMenuFtpBackupUploadTask;
            switch (status)
            {
                case TaskStatus.Created:
                    {
                        uploadTask.TryTask = true;
                        uploadTask.TaskComplete = false;
                        uploadTask.LastTryCount = 0;

                        break;
                    }
                case TaskStatus.Running:
                    {
                        uploadTask.LastTryCount++;
                        uploadTask.TaskStarted = true;
                        uploadTask.LastStartedUtc = DateTime.UtcNow;
                        uploadTask.LastTriedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.RanToCompletion:
                    {
                        uploadTask.TryTask = false;
                        uploadTask.TaskStarted = false;
                        uploadTask.TaskComplete = true;
                        uploadTask.LastCompletedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.Faulted:
                    {
                        //reset to run again.
                        uploadTask.TryTask = true;
                        uploadTask.TaskStarted = false;

                        break;
                    }

                default: { break; }
            }

            this.myAndromedaSiteMenuDataService.Update(siteMenu);
        }
    }
}