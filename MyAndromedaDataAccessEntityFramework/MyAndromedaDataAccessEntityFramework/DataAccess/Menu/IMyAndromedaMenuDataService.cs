using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain.Menus.Items;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaMenuDataService : IDependency 
    {
        
        /// <summary>
        /// Gets the menu items.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        IQueryable<MenuItemThumbnail> GetMenuItems(int andromedaSiteId);

        /// <summary>
        /// Adds the thumbnail for menu item.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="thumb">The thumb.</param>
        /// <returns></returns>
        MenuItemThumbnail AddThumbnailForMenuItem(MenuItem menuItem, ThumbnailImage thumb);
        MenuItemThumbnail AddThumbnailForMenuItems(IEnumerable<MenuItem> menuItems, ThumbnailImage thumb);
 
        /// <summary>
        /// Clears the thumbnails for menu item.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <param name="menuItemId">The menu item id.</param>
        void ClearThumbnailsForItem(MenuItem menuItem);

        /// <summary>
        /// Clears the thumbnails for items.
        /// </summary>
        /// <param name="menuItems">The menu items.</param>
        void ClearThumbnailsForItems(IEnumerable<MenuItem> menuItems);

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        SiteMenu GetMenu(int andromedaSiteId);

        /// <summary>
        /// Gets the menu and do some work on the item with the same dbcontext.
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="whileOpen">While the context is open.</param>
        void GetMenuAndTranslate(int andromedaSiteId, Action<MenuDbJob> whileOpen);
        
        /// <summary>
        /// Creates the specified Andromeda site id.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        SiteMenu Create(int andromedaSiteId);

        IEnumerable<MenuItem> GetOrCreateMenuItems(SiteMenu menu, IEnumerable<int> similarMenuItemIds);

        MenuItem GetOrCreateMenuItem(SiteMenu menu, int acsMenuItemId);

        MenuItem CreateMenuItem(SiteMenu menu, int acsMenuItemId);

        void UpdateMenuHasChanged(Guid menuId);
    }

    public class MyAndromedaMenuDataService : IMyAndromedaMenuDataService
    {
        public IQueryable<MenuItemThumbnail> GetMenuItems(int andromedaSiteId)
        {
            var dbContext = NewContext();
            var table = dbContext.MenuItemThumbnails;
            var query = table
                .Where(thumbnails => thumbnails.MenuItemThumbnailRelations
                    .Any(menuItemLink => menuItemLink.MenuItem.SiteMenu.AndromediaId == andromedaSiteId)
            );

            return query;
            //return table.Where(e => e.MenuItems.Any(menuItem => menuItem.SiteMenus.Any(siteMenu => siteMenu.AndromediaId == andromedaSiteId)));
        }

        public void ClearThumbnailsForItem(MenuItem menuItem)
        {
            using (var dbContext = NewContext())
            {
                var table = dbContext.MenuItems;

                //fetch menu item
                //enforce the expectation that there are one/no records for the table 
                var dbMenutItem = table
                                       .Where(e => e.Id == menuItem.Id)
                                       .SingleOrDefault();

                //no item excellent.
                if (dbMenutItem == null)
                {
                    return;
                }

                var thumbs = dbMenutItem.MenuItemThumbnailsLinkTables;
                thumbs.Clear();

                dbContext.SaveChanges();
            }
        }

        public void ClearThumbnailsForItems(IEnumerable<MenuItem> menuItems)
        {
            foreach (var menuItem in menuItems) { this.ClearThumbnailsForItem(menuItem); }
        }

        public MenuItemThumbnail AddThumbnailForMenuItem(MenuItem menuItem, ThumbnailImage thumb)
        {
            return this.AddThumbnailForMenuItems(new[] { menuItem }, thumb);
        }

        public MenuItemThumbnail AddThumbnailForMenuItems(IEnumerable<MenuItem> menuItems, ThumbnailImage thumb)
        {
            using (var dbContext = NewContext())
            {
                
                var table = dbContext.MenuItemThumbnails;
                var entity = table.Create();

                //entity.Id = Guid.NewGuid();
                entity.Src = thumb.Url ?? string.Empty;
                entity.Alt = thumb.Alt ?? string.Empty;
                entity.FileName = thumb.FileName;
                entity.Height = thumb.Height;
                entity.Width = thumb.Width;

                table.Add(entity);
                dbContext.SaveChanges();
                //associate with the menu-item
                var menuItemIds = menuItems.Select(e => e.Id).ToArray();
                var menuItemEntitys = dbContext.MenuItems.Where(e => menuItemIds.Any(id => id == e.Id)).ToArray();

                foreach (var menuItemEntity in menuItemEntitys) 
                {
                    entity.MenuItemThumbnailRelations.Add(new MenuItemThumbnailsLinkTable() { MenuItem = menuItemEntity, MenuItemThumbnail = entity });
                }

                dbContext.SaveChanges();

                return entity;
            }
        }

        
        public void UpdateMenuHasChanged(Guid menuId) 
        {
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.SiteMenus;
                var query = table.Where(e => e.Id == menuId);

                var menu = query.Single();
                if (menu == null)
                { 
                    return;
                }

                menu.LastUpdated = DateTime.UtcNow;
                dbContext.SaveChanges();
            }
        }

        public MenuItem GetOrCreateMenuItem(SiteMenu menu, int itemId)
        {
            using (var dbContext = NewContext())
            {
                var table = dbContext.MenuItems;
                var query = table
                                 .Where(e => e.ItemId == itemId)
                                 .Where(e => e.SiteMenu.Id == menu.Id);
                //.Where(e => e.SiteMenus.Any(itemMenu => itemMenu.Id == menu.Id));

                var result = query.SingleOrDefault();

                if (result == null)
                {
                    result = this.CreateMenuItem(menu, itemId);
                }

                return result;
            }
        }

        public IEnumerable<MenuItem> GetOrCreateMenuItems(SiteMenu menu, IEnumerable<int> similarMenuItemIds)
        {
            List<MenuItem> menuItemsToReturn = new List<MenuItem>();
            using (var dbContext = NewContext())
            {
                var table = dbContext.MenuItems;
                var query = table.Where(e => e.SiteMenu.Id == menu.Id).ToList();

                foreach (var id in similarMenuItemIds)
                {
                    var result = query.SingleOrDefault(e=> e.ItemId == id);
                    if (result == null) 
                    {
                        result = this.CreateMenuItem(menu, id);
                    }

                    menuItemsToReturn.Add(result);
                }
            }

            return menuItemsToReturn;
        }


        public MenuItem CreateMenuItem(SiteMenu menu, int itemId)
        {
            using (var dbContext = NewContext())
            {
                var table = dbContext.MenuItems;
                var entity = table.Create();
                var menuEntity = dbContext.SiteMenus.Single(e => e.AndromediaId == menu.AndromediaId);
                
                //entity.Id = Guid.NewGuid();
                entity.ItemId = itemId;
                entity.SiteMenu = menuEntity;
                
                table.Add(entity);
                
                dbContext.SaveChanges();

                return entity;
            }
        }

        public SiteMenu Create(int andromedaSiteId)
        {
            SiteMenu menu;
            using (var dbContext = NewContext())
            {
                var table = dbContext.SiteMenus;
                var entity = table.Create();

                entity.AndromediaId = andromedaSiteId;
                entity.LastUpdated = DateTime.UtcNow;
                table.Add(entity);

                dbContext.SaveChanges();
                menu = entity;
            }

            return menu;
        }

        public SiteMenu GetMenu(int andromedaSiteId)
        {
            using (var dbContext = NewContext())
            {
                var result = this.GetMenuWithContext(dbContext, andromedaSiteId);

                if (result != null)
                {
                    return result;
                }
            }

            //obviously there isn't one already. Need to go make another. 
            return this.Create(andromedaSiteId);
        }

        public void GetMenuAndTranslate(int andromedaSiteId, Action<MenuDbJob> workWithSiteMenuWhileOpen) 
        {
            using (var dbConext = NewContext())
            { 
                var menu = this.GetMenuWithContext(dbConext, andromedaSiteId);

                var menuItemQuery = dbConext.MenuItems.Where(e=> e.SiteMenu.AndromediaId == andromedaSiteId);
                var menuItemThumbQuery = dbConext.MenuItemThumbnails
                    .Where(e => e.MenuItemThumbnailRelations
                        .Any(menuItemLink => menuItemLink.MenuItem.SiteMenu.AndromediaId == andromedaSiteId));

                var menuItemThumbnailLinkQuery = dbConext.MenuItemThumbnailsLinkTable
                    .Where(e => e.MenuItem.SiteMenu.AndromediaId == andromedaSiteId);

                var job = new MenuDbJob(menu, menuItemQuery,menuItemThumbnailLinkQuery, menuItemThumbQuery);

                workWithSiteMenuWhileOpen(job);
            }
        }

        private static MyAndromedaDbContext NewContext()
        {
            return new MyAndromedaDbContext();
        }

        private SiteMenu GetMenuWithContext(MyAndromedaDbContext dbContext, int andromedaSiteId) 
        {
            var table = dbContext.SiteMenus;
            var query = table.Where(e => e.AndromediaId == andromedaSiteId);
            var result = query.SingleOrDefault();

            return result;
        }
    }

    public class MenuDbJob 
    {
        private readonly IQueryable<MenuItem> menuItemQuery;
        private readonly IQueryable<MenuItemThumbnail> menuItemThumbQuery;

        private SiteMenu menu;
        private readonly IQueryable<MenuItemThumbnailsLinkTable> menuItemThumbnailLinkQuery;

        public MenuDbJob(SiteMenu menu,
            IQueryable<MenuItem> menuItemQuery,
            IQueryable<MenuItemThumbnailsLinkTable> menuItemThumbnailLinkQuery,
            IQueryable<MenuItemThumbnail> menuItemThumbQuery)
        {
            this.menuItemThumbnailLinkQuery = menuItemThumbnailLinkQuery;
            this.menu = menu;
            this.menuItemQuery = menuItemQuery;
            this.menuItemThumbQuery = menuItemThumbQuery;
        }

        public SiteMenu Menu
        {
            get
            {
                return this.menu;
            }
        }

        public IQueryable<MenuItem> QueryMenuItems() 
        {
            return this.menuItemQuery;
        }

        public IQueryable<MenuItemThumbnail> QueryThumbnailImages() 
        {
            return this.menuItemThumbQuery;
        }

        public IQueryable<MenuItemThumbnailsLinkTable> QueryMenuItemThumbnailLinkTable() 
        {
            return this.menuItemThumbnailLinkQuery;
        }
    }
}