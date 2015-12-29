using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccess.Model;

namespace AndroCloudDataAccess.Domain
{
    public class MenuDataAccess : IMenuDataAccess
    {
        public bool Put(string sessionToken, string data, int version)
        {
            using (var e = new ACSEntities())
            {

                var sessiontoken = Guid.Parse(sessionToken);

                var siteqry = from s in e.Sites
                              where s.SessionID == sessiontoken
                              select s;

                Model.Site site = siteqry.FirstOrDefault();

                if (site != null)
                {
                    var sitemenu = site.SiteMenus.FirstOrDefault();

                    // Update the menu record
                    if (sitemenu != null)
                    {
                        sitemenu.menuData = data;
                        sitemenu.Version = version;
                        sitemenu.LastUpdated = DateTime.UtcNow;
                        try
                        {
                            return e.SaveChanges() != 0; // persist the entity changes to the database.
                        }
                        catch (OptimisticConcurrencyException)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // no menu record, add it ??
                        // But we need to know the menu type ????

                        var sm = new Model.SiteMenu
                                     {MenuType = "XML", Version = version, menuData = data, SiteID = site.ID};

                        site.SiteMenus.Add(sm);

                        e.SaveChanges();
                    }
                }

                return false;
            }
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
