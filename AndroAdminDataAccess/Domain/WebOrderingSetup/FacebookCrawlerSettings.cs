using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
    public class FacebookCrawlerSettings
    {
        public void DefaultFacebookCrawlerSettings()
        {
            Title = string.Empty;
            SiteName = string.Empty;
            Description = string.Empty;            
            FacebookProfileLogoPath = string.Empty;
        }

        public string Title { set; get; }
        public string SiteName { set; get; }
        public string Description { set; get; }        
        public string FacebookProfileLogoPath { set; get; }
    }
}
