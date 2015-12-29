using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccess.Domain.Marketing
{
    public class EmailCampaign
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>The reference.</value>
        public string Reference { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the email template.
        /// </summary>
        /// <value>The email template.</value>
        public string EmailTemplate { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The create.</value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>The modified.</value>
        public DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the chain id.
        /// </summary>
        /// <value>The chain id.</value>
        public int ChainId { get; set; }

        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        /// <value>The site id.</value>
        public IEnumerable<EmailCampaignSitePart> SiteIds { get; set; }

        /// <summary>
        /// Gets or sets the editable.
        /// </summary>
        /// <value>The editable.</value>
        public bool Editable { get; set; }

        /// <summary>
        /// Is the email campaign local or for the entire chain.
        /// </summary>
        /// <value>The visible to chain.</value>
        public bool ChainOnly { get; set; }
    }

    public class EmailCampaignSitePart 
    {
        /// <summary>
        /// Gets or sets the email campaign.
        /// </summary>
        /// <value>The email campaign.</value>
        public EmailCampaign EmailCampaign { get; set; }

        /// <summary>
        /// Gets or sets the site id.
        /// </summary>
        /// <value>The site id.</value>
        public int SiteId { get; set; }
        
        /// <summary>
        /// Gets or sets the editable.
        /// </summary>
        /// <value>The editable.</value>
        public bool Editable { get; set; }
    }

    public class EmailSettings 
    {
        public string Host { get; set; }
        public int? Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int ChainId { get; set; }

        public string From { get; set; }
    }
}
