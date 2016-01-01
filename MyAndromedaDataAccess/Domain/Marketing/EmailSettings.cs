using System.ComponentModel.DataAnnotations;
using System.Security;

namespace MyAndromedaDataAccess.Domain.Marketing
{
    public class EmailSettings 
    {
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        [Required]
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the SSL.
        /// </summary>
        /// <value>The SSL.</value>
        public bool SSL { get; set; }

        /// <summary>
        /// Gets or sets the pickup folder.
        /// </summary>
        /// <value>The pickup folder.</value>
        public string PickupFolder { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int? Port { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the chain id these settings belong to.
        /// </summary>
        /// <value>The chain id.</value>
        public int ChainId { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        [Required]
        public string From { get; set; }

        public bool Authenticated { 
            get 
            {
                return !string.IsNullOrWhiteSpace(this.UserName) && !string.IsNullOrWhiteSpace(this.Password);
            }
        }
    }
}