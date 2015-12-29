using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AndroUsersDataAccess.Domain
{
    public class SecurityGroup
    {
        public const string AdministratorSecurityGroup = "Andro Admin Administrator";

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(1000, MinimumLength = 0)]
        public string Description { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
