using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain
{
    public class BringgConfig
    {
        public bool IsEnabled { get; set; }
        public bool BringgCompanyId { get; set; }
        public bool BringgAccessToken { get; set; }
        public bool BringgSecretKey { get; set; }

        public BringgConfig()
        {
            IsEnabled = false;
        }
    }
}
