using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccess.Domain.Marketing;
using Domain = MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class EmailCampaignTasksDataAccess : IEmailCampaignTasksDataAccess
    {
        public IEnumerable<EmailCampaignTask> GetTasksBySiteId(int siteId)
        {
            using (var dbContext = new Model.MyAndro.MyAndromedaDbContext())
            {
                var query = dbContext.EmailCampaignTasks
                    .Where(e=> e.EmailCampaign.EmailCampaignSites.Any(site => site.SiteId == siteId));

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
            using (var dbContext = new Model.MyAndro.MyAndromedaDbContext()) 
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
            using (var dbContext = new Model.MyAndro.MyAndromedaDbContext()) 
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

        public IEnumerable<EmailCampaignTask> GetTasksToRun(DateTime dateTime)
        {
            using (var dbContext = new Model.MyAndro.MyAndromedaDbContext()) 
            {
                var query = dbContext.EmailCampaignTasks
                    .Where(e => !e.Completed)
                    .Where(e => !e.Started)
                    .Where(e => !e.RunLaterOnUtc.HasValue || e.RunLaterOnUtc <= DateTime.UtcNow);

                var results = query.ToArray();

                var output = results.Select(e => e.ToDomainModel()).ToArray();

                return output;
            }
        }

        public void SetAsRunning(IEnumerable<Domain.EmailCampaignTask> campaignsToSend)
        {
            using (var dbContext = new Model.MyAndro.MyAndromedaDbContext()) 
            {
                var ids = campaignsToSend.Select(e => e.Id).ToArray();
                var query = dbContext.EmailCampaignTasks.Where(e=> ids.Any(id => id == e.Id));

                foreach (var record in query) 
                {
                    record.Started = true;
                }

                dbContext.SaveChanges();
            }
        }

        public void UpdateBatch(IEnumerable<Domain.EmailCampaignTask> campaignsToSend)
        {
            using (var dbContext = new Model.MyAndro.MyAndromedaDbContext())
            {
                var ids = campaignsToSend.Select(e => e.Id).ToArray();
                var query = dbContext.EmailCampaignTasks.Where(e => ids.Any(id => id == e.Id));

                foreach (var record in query)
                {
                    record.Completed = true;
                }

                dbContext.SaveChanges();
            }
        }

        
    }
}
