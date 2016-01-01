using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AndroUsersDataAccess.Domain
{
    public class AndroUser
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [Required(ErrorMessage = "Please enter a first name")]
        [StringLength(10, ErrorMessage = "Max 10 characters")]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        [Required(ErrorMessage = "Please enter a surname")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public virtual string SurName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Please enter a password")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public virtual string Password { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [Required(ErrorMessage = "Please enter an email address")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        public virtual bool Active { get; set; }

        /// <summary>
        /// Created
        /// </summary>
        public virtual DateTime Created { get; set; }

        public List<SecurityGroup> SecurityGroups { get; set; }

        public AndroUser()
        {
            this.SecurityGroups = new List<SecurityGroup>();
        }
    }
}
