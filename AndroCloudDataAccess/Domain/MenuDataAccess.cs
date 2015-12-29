using System;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccess.Model;

namespace AndroCloudDataAccess.Domain
{
    public class MenuDataAccess : IMenuDataAccess
    {
        public bool Put(string sessionToken, string data)
        {
            var e = new ACSEntities();

            Guid sessiontoken = Guid.Parse(sessionToken);

            var siteqry = from s in e.SiteMenus
                       where s.Site.SessionID == sessiontoken
                       select s;

            Model.SiteMenu sitemenu = siteqry.FirstOrDefault();

            if (sitemenu != null)
            {

                sitemenu.menuData = data;

                e.SaveChanges();

                return true;
            }

            return false;
        }


        /// <summary>
        /// Get a menu
        /// </summary>
        /// <param name="sessionToken"></param>
        /// <param name="siteID">AndroSite ID</param>
        /// <returns>SiteMenu Object</returns>
        public SiteMenu Get(string sessionToken, string siteID)
        {
            var e = new ACSEntities();

            var token = Guid.Parse(sessionToken);
            var androSiteId = int.Parse(siteID);

            var menuqry = from m in e.SiteMenus
                          where m.Site.SessionID == token && m.Site.AndroID == androSiteId
                          select m;


            var sitemenu = menuqry.FirstOrDefault();

            if (sitemenu != null)
            {
                var sm = new SiteMenu
                             {
                                 MenuType = sitemenu.MenuType,
                                 SiteID = sitemenu.SiteID.GetValueOrDefault(),
                                 Version = sitemenu.Version.GetValueOrDefault(0),
                                 menuData = sitemenu.menuData
                             };

                return sm;
            }

            return null;
        }
    }
}
