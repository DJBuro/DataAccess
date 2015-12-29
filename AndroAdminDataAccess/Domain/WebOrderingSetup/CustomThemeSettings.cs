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
            ColourRange1 = string.Empty;
            ColourRange2 = string.Empty;
            ColourRange3 = string.Empty;
            ColourRange4 = string.Empty;
            ColourRange5 = string.Empty;
            ColourRange6 = string.Empty;
        }
        public string DesktopBackgroundImagePath { set; get; }
        public string MobileBackgroundImagePath { set; get; }
       
        public string ColourRange1 { set; get; }
        public string ColourRange2 { set; get; }
        public string ColourRange3 { set; get; }
        public string ColourRange4 { set; get; }
        public string ColourRange5 { set; get; }
        public string ColourRange6 { set; get; }

    }    
}
