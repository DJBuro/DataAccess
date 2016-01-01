using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain.Menus.Items;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaMenuDataService : IDependency 
    {
        MenuItemThumbnail AddThumbnailForMenuItem(SiteMenu menu, int id, ThumbnailImage thumb);

        void ClearThumbnailsForItem(SiteMenu menu, int menuItemId);

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        SiteMenu GetMenu(int andromedaSiteId);

        /// <summary>
        /// Creates the specified Andromeda site id.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        SiteMenu Create(int andromedaSiteId);

    }

    public class MyAndromedaMenuDataService : IMyAndromedaMenuDataService 
    {
        public void ClearThumbnailsForItem(SiteMenu menu, int menuItemId)
        {
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.MenuItems;

                var menuItem = table
                    .Where(e => e.ItemId == menuItemId)
                    .Where(e => e.SiteMenus.Any(siteMenu => siteMenu.Id == menu.Id))
                    .SingleOrDefault();
                //no item excellent.
                if (menuItem == null) { return; }

                table.Remove(menuItem);
                dbContext.SaveChanges();
            }
        }

        public MenuItemThumbnail AddThumbnailForMenuItem(SiteMenu menu, int id, ThumbnailImage thumb)
        {
            using(var dbContext = NewContext())
            {
                var table = dbContext.MenuItemThumbnails;

                var entity = table.Create();

                entity.Id = Guid.NewGuid();
                entity.Src = thumb.Url;
                entity.FileName = thumb.FileName;
                entity.Height = thumb.Height;
                entity.Width = thumb.Width;

                dbContext.MenuItemThumbnails.Add(entity);
                dbContext.SaveChanges();

                return entity;
            }
        }

        public SiteMenu Create(int andromedaSiteId)
        {
            SiteMenu menu; 
            using (var dbContext = new MyAndromedaDataAccessEntityFramework.Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.SiteMenus;
                var entity = table.Create();

                entity.AndromediaId = andromedaSiteId;
                table.Add(entity);
                
                dbContext.SaveChanges();
                menu = entity;
            }

            return menu;
        }

        public SiteMenu GetMenu(int andromedaSiteId)
        {
            using (var dbContext = new MyAndromedaDataAccessEntityFramework.Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.SiteMenus;
                var query = table.SingleOrDefault(e => e.AndromediaId == andromedaSiteId);

                if (query != null)
                    return query;
            }

            //obviously there isn't one already. Need to go make another. 
            return Create(andromedaSiteId);
        }

        
        private static MyAndromedaDbContext NewContext()
        {
            return new MyAndromedaDbContext();
        }
    }
}
