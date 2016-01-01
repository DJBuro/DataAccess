using System;

namespace MyAndromedaDataAccess.Domain.Marketing
{
    public class EmailCampaignTask 
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email campaign.
        /// </summary>
        /// <value>The email campaign.</value>
        public EmailCampaign EmailCampaign { get; set; }

        /// <summary>
        /// Gets or sets the email settings.
        /// </summary>
        /// <value>The email settings.</value>
        public EmailSettings EmailSettings { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the run on.
        /// </summary>
        /// <value>The run on.</value>
        public DateTime? RunLaterOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the run at.
        /// </summary>
        /// <value>The run at.</value>
        public DateTime? RanAt { get; set; }

        /// <summary>
        /// Gets or sets the started.
        /// </summary>
        /// <value>The started.</value>
        public bool Started { get; set; }

        /// <summary>
        /// Gets or sets the completed.
        /// </summary>
        /// <value>The completed.</value>
        public bool Completed { get; set; }

        /// <summary>
        /// Gets or sets when the task should be retried .
        /// </summary>
        /// <value>The retry after.</value>
        public DateTime? RetryAfter { get;set; }
        /// <summary>
        /// Gets or sets the canceled.
        /// </summary>
        /// <value>The canceled.</value>
        public bool Canceled { get; set; }

        /// <summary>
        /// Gets or sets the completed at.
        /// </summary>
        /// <value>The completed at.</value>
        public DateTime? CompletedAt { get; set; }
    }
}