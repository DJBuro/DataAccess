using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class StorePaymentProvider
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Please enter some Display Text")]
        public virtual string DisplayText { get; set; }

        [Required(ErrorMessage = "Please enter a Provider Name")]
        public virtual string ProviderName { get; set; }

        [Required(ErrorMessage = "Please enter a Client Id")]
        public virtual string ClientId { get; set; }

        [Required(ErrorMessage = "Please enter a Client Pasword")]
        public virtual string ClientPassword { get; set; }
    }
}