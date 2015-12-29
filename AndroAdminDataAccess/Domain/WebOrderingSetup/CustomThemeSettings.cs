using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class CustomThemeSettings
    {
        public void DefaultCustomThemeSettings()
        {
            DesktopBackgroundImagePath = string.Empty;
            MobileBackgroundImagePath = string.Empty;
        }
        public string DesktopBackgroundImagePath { set; get; }
        public string MobileBackgroundImagePath { set; get; }
    }
}
