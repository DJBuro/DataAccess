using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class StoreHub
    {
        /// <summary>
        /// Gets or sets the external id.
        /// </summary>
        /// <value>The external id.</value>
        public string StoreExternalId { get; set; }

        /// <summary>
        /// Gets or sets the hub.
        /// </summary>
        /// <value>The hub.</value>
        public HubItem Hub { get; set; }
    }
}
