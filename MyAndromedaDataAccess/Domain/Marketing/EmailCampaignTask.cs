using System;

namespace MyAndromedaDataAccess.Domain.Marketing
{
    public class EmailCampaignTask 
    {
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

        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the run on.
        /// </summary>
        /// <value>The run on.</value>
        public DateTime? RunOn { get; set; }

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
    }
}