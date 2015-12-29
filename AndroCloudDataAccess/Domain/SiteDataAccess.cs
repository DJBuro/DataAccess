using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccess.Model;
using System.Collections.Generic;

namespace AndroCloudDataAccess.Domain
{
    public class SiteDataAccess : ISiteDataAccess
    {
        /// <summary>
        /// Gets sites
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <param name="siteID">AndroSite ID</param>
        /// <returns>SiteMenu Object</returns>
        public List<Site> Get(Guid securityGuidstring, Guid? chainGuidText, float? maxDistance, float? longitude, float? latitude)
        {
            //var e = new ACSEntities();

            //var token = Guid.Parse(sessionToken);
            //var androSiteId = int.Parse(siteID);

            //var menuqry = from m in e.SiteMenus
            //              where m.Site.SessionID == token && m.Site.AndroID == androSiteId
            //              select m;


            //var sitemenu = menuqry.FirstOrDefault();

            //if (sitemenu != null)
            //{
            //    var sm = new SiteMenu
            //                 {
            //                     MenuType = sitemenu.MenuType,
            //                     SiteID = sitemenu.SiteID.GetValueOrDefault(),
            //                     Version = sitemenu.Version.GetValueOrDefault(0),
            //                     menuData = sitemenu.menuData
            //                 };

            //    return sm;
            //}

            return null;
        }
    }
}
