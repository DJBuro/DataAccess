using System;
using System.Collections.Generic;
using MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IEmailCampaignTasksDataAccess : IDataAccessOptions 
    {
        /// <summary>
        /// Gets the tasks to run.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
        IEnumerable<EmailCampaignTask> GetTasksToRun(DateTime dateTime);

        /// <summary>
        /// Creates the specified campaign task.
        /// </summary>
        /// <param name="campaignTask">The campaign task.</param>
        void Create(Domain.Marketing.EmailCampaignTask campaignTask);

        /// <summary>
        /// Sets as running.
        /// </summary>
        /// <param name="campaignsToSend">The campaigns to send.</param>
        void SetAsRunning(IEnumerable<EmailCampaignTask> campaignsToSend);

        /// <summary>
        /// Updates the batch.
        /// </summary>
        /// <param name="campaignsToSend">The campaigns to send.</param>
        void UpdateBatch(IEnumerable<EmailCampaignTask> campaignsToSend);

        /// <summary>
        /// Updates the specified campaign.
        /// </summary>
        /// <param name="campaign">The campaign.</param>
        void Update(EmailCampaignTask campaign);

        /// <summary>
        /// Gets the tasks by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        IEnumerable<EmailCampaignTask> GetTasksBySiteId(int siteId);
    }
}