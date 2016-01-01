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
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

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
    }
}
