using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccess.Domain.Marketing;
using Domain = MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class EmailCampaignTasksDataAccess : IEmailCampaignTasksDataAccess
    {
        public void Update(Domain.EmailCampaignTask campaign)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
            {
                var dbModel = dbContext.EmailCampaignTasks.SingleOrDefault(e => e.Id == campaign.Id);

                dbModel.Update(campaign);
                dbContext.SaveChanges();
            }
        }

        public void Create(Domain.EmailCampaignTask campaignTask)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
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
            using (var dbContext = new Model.MyAndromedaEntities()) 
            {
                var query = dbContext.EmailCampaignTasks
                    .Where(e => !e.Completed)
                    .Where(e => !e.Started)
                    .Where(e => !e.RunLaterOnUtc.HasValue || e.RunLaterOnUtc <= DateTime.UtcNow);

                var results = query.ToArray();

                var output = results.Select(e => e.ToDomainModel()).ToArray();
                //var output = results.Select(e => new Domain.EmailCampaignTask() { 
                //    Id = e.Id,
                //    Completed = e.Completed,
                //    Created = e.CreatedOnUtc,
                //    RanAt = e.RanAtUtc,
                //    RunLaterOnUtc = e.RunLaterOnUtc,
                //    Started = e.Started,
                //    EmailSettings = e.EmailCampaignSetting == null ? null : e.EmailCampaignSetting.ToDomainModel(),
                //    EmailCampaign = e.EmailCampaign == null ? null : e.EmailCampaign.ToDomainModel()
                //}).ToArray();

                return output;
            }
        }

        public void SetAsRunning(IEnumerable<Domain.EmailCampaignTask> campaignsToSend)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
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
            using (var dbContext = new Model.MyAndromedaEntities())
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
