using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromedaDataAccess.DataAccess.MyAndromeda.Email;
using MyAndromedaDataAccess.Domain.Marketing;
using Domain = MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class EmailCampaignTasksDataAccess : IEmailCampaignTasksDataAccess
    {
        public IEnumerable<EmailCampaignTask> GetTasksBySiteId(int siteId)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var query = dbContext.EmailCampaignTasks
                                     .Where(e => e.EmailCampaign.EmailCampaignSites.Any(site => site.SiteId == siteId))
                                     .OrderByDescending(e => e.CreatedOnUtc);

                var results = query.ToArray();

                var output = results.Select(e => e.ToDomainModel()).ToArray();

                return output;
            }
        }

        /// <summary>
        /// Updates the specified campaign.
        /// </summary>
        /// <param name="campaign">The campaign.</param>
        public void Update(Domain.EmailCampaignTask campaign)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var dbModel = dbContext.EmailCampaignTasks.SingleOrDefault(e => e.Id == campaign.Id);

                dbModel.Update(campaign);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Creates the specified campaign task.
        /// </summary>
        /// <param name="campaignTask">The campaign task.</param>
        public void Create(Domain.EmailCampaignTask campaignTask)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var dbModel = dbContext.EmailCampaignTasks.Create();
                var campaign = dbContext.EmailCampaigns.Find(campaignTask.EmailCampaign.Id);

                dbModel.EmailCampaign = campaign;
                dbModel.EmailCampaignSetting = campaignTask.EmailSettings.Id > 0
                                               ? dbContext.EmailCampaignSettings.Find(campaignTask.EmailSettings.Id)
                                               : null;
                
                dbModel.Update(campaignTask);

                dbContext.EmailCampaignTasks.Add(dbModel);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the tasks to run.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <param name="started">The started.</param>
        /// <param name="completed">The completed.</param>
        /// <returns></returns>
        public IEnumerable<EmailCampaignTask> GetTasksToRun(DateTime dateTime, bool? started, bool? completed)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                bool startedQuery = started.GetValueOrDefault(false);
                bool completedQuery = completed.GetValueOrDefault(false);

                var query = dbContext.EmailCampaignTasks
                                     .Where(e => e.Completed == completedQuery)
                                     .Where(e => e.Started == startedQuery)
                                     .Where(e => !e.RunLaterOnUtc.HasValue || e.RunLaterOnUtc <= DateTime.UtcNow);

                var results = query.ToArray();
                
                var output = results.Select(e => e.ToDomainModel()).ToArray();

                return output;
            }
        }

        public void SetAsRunning(IEnumerable<Domain.EmailCampaignTask> campaignsToSend)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var ids = campaignsToSend.Select(e => e.Id).ToArray();
                var query = dbContext.EmailCampaignTasks.Where(e => ids.Any(id => id == e.Id));

                foreach (var record in query) 
                {
                    record.Started = true;
                    record.RanAtUtc = DateTime.UtcNow;
                }

                dbContext.SaveChanges();
            }
        }

        public void UpdateBatch(IEnumerable<Domain.EmailCampaignTask> campaignsToSend)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var ids = campaignsToSend.Select(e => e.Id).ToArray();
                var query = dbContext.EmailCampaignTasks
                                     .Where(e => ids.Any(id => id == e.Id));

                foreach (var record in query)
                {
                    record.Completed = true;
                    record.CompletedAt = DateTime.UtcNow;
                }

                dbContext.SaveChanges();
            }
        }
    }
}