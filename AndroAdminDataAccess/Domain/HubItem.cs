using System;

namespace AndroAdminDataAccess.Domain
{
    public class HubItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the active flag.
        /// </summary>
        /// <value>The active.</value>
        public bool Active { get; set; }
    }
}