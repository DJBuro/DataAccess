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
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(255, ErrorMessage = "Max 255 characters")]
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(255, ErrorMessage = "Max 255 characters")]
        public virtual string SurName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(255, ErrorMessage = "Max 255 characters")]
        public virtual string Password { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(255, ErrorMessage = "Max 255 characters")]
        public virtual string EmailAddress { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        public virtual bool Active { get; set; }

        /// <summary>
        /// Created
        /// </summary>
        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(255, ErrorMessage = "Max 255 characters")]
        public virtual DateTime Created { get; set; }
    }
}
