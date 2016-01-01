﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.Domain.WebOrderingSetup
{
   public class SEO
    {
       public string Title { set; get; }
       public string Keywords { set; get; }
       public string Description { set; get; }
       public string FaviconPath { set; get; }

       public void DefaultFacebookCrawlerSettings()
       {
           this.Title = string.Empty;
           this.Keywords = string.Empty;
           this.Description = string.Empty;
           this.FaviconPath = string.Empty;
       }
    }
}
