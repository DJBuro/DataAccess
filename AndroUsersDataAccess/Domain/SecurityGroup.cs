using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AndroUsersDataAccess.Domain
{
    public class SecurityGroup
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
