using MyAndromedaDataAccess.Domain.DataWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccess.Domain.Marketing
{
    public class Customer
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
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        /// <value>The surname.</value>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the contact details.
        /// </summary>
        /// <value>The contact details.</value>
        public ICollection<Contact> ContactDetails { get; set; }

    }

    public class Contact 
    {
        /// <summary>
        /// Gets or sets the type of the contact.
        /// </summary>
        /// <value>The type of the contact.</value>
        public ContactType ContactType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
    }
}
