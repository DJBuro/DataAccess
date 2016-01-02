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
            HeaderColour = string.Empty;
            FooterColour = string.Empty;

            this.CustomTemplates = new Dictionary<string, CmsPages>();
        }

        public string HeaderColour { set; get; }
        public string FooterColour { set; get; }

        public Dictionary<string, CmsPages> CustomTemplates { get; set; }
    }
}
