using System.Threading.Tasks;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IPubishMenuTaskDataService : IDependency 
    {
        /// <summary>
        /// Sets the acs upload menu data task status.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <param name="status">The status.</param>
        void SetAcsUploadMenuDataTaskStatus(SiteMenu menu, TaskStatus status, DateTime? date = null);

        /// <summary>
        /// Gets the publish tasks.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        IEnumerable<SiteMenu> GetPublishTasks(DateTime time);

        /// <summary>
        /// Adds the history log.
        /// </summary>
        /// <param name="sitemenu">The sitemenu.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="publishAll">The publish all.</param>
        /// <param name="publishMenu">The publish menu.</param>
        /// <param name="publishThumbnails">The publish thumbnails.</param>
        /// <param name="publishOnUtc">The publish on UTC.</param>
        void AddHistoryLog(SiteMenu sitemenu, string userName, bool publishAll, bool publishMenu, bool publishThumbnails, DateTime? publishOnUtc);
    }

    public class MenuPubishDataService : IPubishMenuTaskDataService 
    {
        private readonly IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService;

        public MenuPubishDataService(IMyAndromedaSiteMenuDataService myAndromedaSiteMenuDataService)
        {
            this.myAndromedaSiteMenuDataService = myAndromedaSiteMenuDataService;
        }

        public IEnumerable<SiteMenu> GetPublishTasks(DateTime time) 
        {
            IEnumerable<SiteMenu> results = Enumerable.Empty<SiteMenu>();
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var data = dbContext.QueryMenusWithTasks(e=> 
                    !e.SiteMenuPublishTask.TaskStarted &&
                    e.SiteMenuPublishTask.TryTask &&
                    e.SiteMenuPublishTask.PublishOn <= time 
                );

                results = data;
            }

            return results;
        }

        public void AddHistoryLog(SiteMenu sitemenu, string userName, bool publishAll, bool publishMenu, bool publishThumbnails, DateTime? publishOnUtc)
        {
            using (var dbContex = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContex.SiteMenuPublishTaskHistories;

                var now = DateTime.UtcNow;
                var entity = table.Create();
                entity.CreatedOnUtc = now;
                entity.SiteMenuId = sitemenu.Id;
                entity.PublishThumbnails = publishThumbnails;
                entity.PublishOnUtc = publishOnUtc.GetValueOrDefault(now);
                entity.PublishedBy = userName;
                entity.PublishAll = publishAll;

                table.Add(entity);

                dbContex.SaveChanges();
            }
        }

        public void SetAcsUploadMenuDataTaskStatus(SiteMenu menu, TaskStatus status, DateTime? date = null)
        {
            var publishTask = menu.SiteMenuPublishTask;

            switch (status)
            {
                case TaskStatus.Created:
                    {
                        publishTask.LastTryCount = 0;
                        publishTask.TryTask = true;
                        publishTask.TaskComplete = false;
                        publishTask.PublishOn = date.GetValueOrDefault(DateTime.UtcNow);

                        break;
                    }
                case TaskStatus.Running:
                    {
                        publishTask.LastTryCount++;
                        publishTask.TaskStarted = true;
                        publishTask.LastStartedUtc = DateTime.UtcNow;
                        publishTask.LastTriedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.RanToCompletion:
                    {
                        publishTask.TryTask = false;
                        publishTask.TaskStarted = false;
                        publishTask.TaskComplete = true;
                        publishTask.LastCompletedUtc = DateTime.UtcNow;

                        break;
                    }
                case TaskStatus.Faulted:
                    {
                        //reset to run again.
                        publishTask.TryTask = true;
                        publishTask.TaskStarted = false;

                        break;
                    }

                default: { break; }
            }

            this.myAndromedaSiteMenuDataService.Update(menu);
        }
    }
}