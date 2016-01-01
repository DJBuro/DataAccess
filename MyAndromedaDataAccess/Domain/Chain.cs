using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccess.Domain
{
    public class ChainMetaData 
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }

    public class Chain
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the master menu id.
        /// </summary>
        /// <value>The master menu id.</value>
        public int? MasterMenuId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>The children.</value>
        public IEnumerable<Chain> Items { get; set; }

        /// <summary>
        /// Gets or sets the stores.
        /// </summary>
        /// <value>The stores.</value>
        public IEnumerable<Site> Stores { get; set; }

        
    }
}
