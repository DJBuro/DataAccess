using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class CustomEmailTemplate
    {
        public void DefaultCustomThemeSettings()
        {
            EmailColourRange1 = string.Empty;
            EmailColourRange2 = string.Empty;
        }
        public string EmailColourRange1 { set; get; }
        public string EmailColourRange2 { set; get; }
    }
}
