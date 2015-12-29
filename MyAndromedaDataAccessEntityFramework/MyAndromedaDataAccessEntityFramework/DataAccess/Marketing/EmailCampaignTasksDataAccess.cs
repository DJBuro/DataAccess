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
        public void Create(EmailCampaignTask campaignTask)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
            {
                var dbModel = dbContext.EmailCampaignTasks.Create();
                var campaign = dbContext.EmailCampaigns.Find(campaignTask.EmailCampaign.Id);
                dbModel.EmailCampaign = campaign;
                dbModel.EmailCampaignSetting = campaignTask.EmailSettings.Id > 0 
                    ? dbContext.EmailCampaignSettings.Find(campaignTask.EmailSettings.Id) 
                    : null;
                dbModel.CreatedOnUtc = campaignTask.Created;
                dbModel.RunOnUtc = campaignTask.RunOn;

                dbContext.EmailCampaignTasks.Add(dbModel);
            }
        }

        public IEnumerable<EmailCampaignTask> GetTasksToRun(DateTime dateTime)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
            {
                var query = dbContext.EmailCampaignTasks
                    .Where(e => !e.Completed)
                    .Where(e => !e.Started)
                    .Where(e => !e.RunOnUtc.HasValue) 
                    .Where(e => e.RunOnUtc <= DateTime.UtcNow);

                var results = query.ToArray();
                
                var output = results.Select(e => new Domain.EmailCampaignTask() { 
                    Completed = e.Completed,
                    Created = e.CreatedOnUtc,
                    RanAt = e.RunAtUtc,
                    RunOn = e.RunOnUtc,
                    Started = e.Started,
                    EmailSettings = e.EmailCampaignSetting == null ? null : e.EmailCampaignSetting.ToDomainModel(),
                    EmailCampaign = e.EmailCampaign == null ? null : e.EmailCampaign.ToDomainModel()
                }).ToArray();

                return output;
            }
        }
    }
}
