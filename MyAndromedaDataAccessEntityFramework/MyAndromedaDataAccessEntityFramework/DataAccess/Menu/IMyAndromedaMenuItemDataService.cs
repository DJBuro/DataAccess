using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccess.Domain.Menus.Items;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaMenuItemDataService : IDependency 
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
        /// Gets the menu and do some work on the item with the same dbcontext.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <param name="whileOpen">While the context is open.</param>
        void GetMenuAndTranslate(int andromedaSiteId, Action<MenuDbJob> whileOpen);
        
        IEnumerable<MenuItem> GetOrCreateMenuItems(SiteMenu menu, IEnumerable<int> similarMenuItemIds);

        MenuItem GetOrCreateMenuItem(SiteMenu menu, int acsMenuItemId);

        void UpdateMenuItem(SiteMenu menu, MenuItem menuItem);

        //MenuItem CreateMenuItem(SiteMenu menu, int acsMenuItemId);

        void UpdateMenuHasChanged(Guid menuId);

        void UpdateMenuItems(SiteMenu menu, MenuItem[] dbItems);
    }

 
    public class MyAndromedaMenuDataService : IMyAndromedaMenuItemDataService
    {
        private readonly IMyAndromedaSiteMenuDataService siteMenuDataService;

        public MyAndromedaMenuDataService(IMyAndromedaSiteMenuDataService siteMenuDataService) 
        {
            this.siteMenuDataService = siteMenuDataService;
        }

        /// <summary>
        /// Gets the menu items.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        public IQueryable<MenuItemThumbnail> GetMenuItems(int andromedaSiteId)
        {
            var dbContext = NewContext();
            var table = dbContext.MenuItemThumbnails;
            var query = table
                             .Where(thumbnails => thumbnails.MenuItemThumbnailRelations
                                                            .Any(menuItemLink => menuItemLink.MenuItem.SiteMenu.AndromediaId == andromedaSiteId));

            return query;
            //return table.Where(e => e.MenuItems.Any(menuItem => menuItem.SiteMenus.Any(siteMenu => siteMenu.AndromediaId == andromedaSiteId)));
        }

        /// <summary>
        /// Clears the thumbnails for menu item.
        /// </summary>
        /// <param name="menuItem"></param>
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

        /// <summary>
        /// Clears the thumbnails for items.
        /// </summary>
        /// <param name="menuItems">The menu items.</param>
        public void ClearThumbnailsForItems(IEnumerable<MenuItem> menuItems)
        {
            foreach (var menuItem in menuItems)
            {
                this.ClearThumbnailsForItem(menuItem);
            }
        }

        /// <summary>
        /// Adds the thumbnail for menu item.
        /// </summary>
        /// <param name="menuItem">The menu item.</param>
        /// <param name="thumb">The thumb.</param>
        /// <returns></returns>
        public MenuItemThumbnail AddThumbnailForMenuItem(MenuItem menuItem, ThumbnailImage thumb)
        {
            return this.AddThumbnailForMenuItems(new[] { menuItem }, thumb);
        }

        /// <summary>
        /// Adds the thumbnail for menu items.
        /// </summary>
        /// <param name="menuItems">The menu items.</param>
        /// <param name="thumb">The thumb.</param>
        /// <returns></returns>
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
                    table.Add(result);
                    dbContext.SaveChanges();
                }

                return result;
            }
        }

        public void UpdateMenuItems(SiteMenu siteMenu, MenuItem[] dbItems)
        {
            using (var dbContext = NewContext())
            {
                var ids = dbItems.Select(e => e.ItemId);
                var dbMenuItems = GetOrCreateMenuItems(dbContext, siteMenu, ids).ToArray();

                foreach (var dbEntity in dbMenuItems) 
                {
                    var updateWith = dbItems.Single(e=> e.ItemId == dbEntity.ItemId);
                    dbEntity.Name = updateWith.Name;
                    dbEntity.WebName = updateWith.WebName;
                    dbEntity.WebDescription = updateWith.WebDescription;
                }

                dbContext.SaveChanges();
            }
        }

        public void UpdateMenuItem(SiteMenu siteMenu, MenuItem menuItem)
        {
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.MenuItems;
                var query = table.Where(e => e.Id == menuItem.Id);
                var result = query.Single();

                result.Name = menuItem.Name;
                result.WebName = menuItem.WebName;
                result.WebDescription = menuItem.WebDescription;

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<MenuItem> GetOrCreateMenuItems(SiteMenu menu, IEnumerable<int> similarMenuItemIds)
        {
            IEnumerable<MenuItem> menuItemsToReturn = Enumerable.Empty<MenuItem>();
            using (var dbContext = NewContext())
            {
                menuItemsToReturn = this.GetOrCreateMenuItems(dbContext, menu, similarMenuItemIds);

                dbContext.SaveChanges();
            }

            return menuItemsToReturn;
        }

        private IEnumerable<MenuItem> GetOrCreateMenuItems(MyAndromedaDbContext dbContext, SiteMenu menu, IEnumerable<int> similarMenuItemIds) 
        {
            List<MenuItem> menuItemsToReturn = new List<MenuItem>();
            

            var menuItemTable = dbContext.MenuItems;
            var menuItemQuery = menuItemTable.Where(e => e.SiteMenu.Id == menu.Id);
            var allMenuItemResult = menuItemQuery.ToArray();

            List<MenuItem> menuItemsToCreate = new List<MenuItem>();

            foreach (var id in similarMenuItemIds)
            {
                var itemResult = allMenuItemResult.SingleOrDefault(e => e.ItemId == id);

                if (itemResult == null)
                {
                    itemResult = this.CreateMenuItem(menu, id);
                    menuItemsToCreate.Add(itemResult);
                }

                menuItemsToReturn.Add(itemResult);
            }

            foreach (var item in menuItemsToCreate)
            {
                dbContext.MenuItems.Add(item);
            }

            return menuItemsToReturn;
        }

        private MenuItem CreateMenuItem(SiteMenu menu, int itemId)
        {
            //using (var dbContext = NewContext())
            {
                //var table = dbContext.MenuItems;
                var entity = new MenuItem();// table.Create();
                  
                //entity.Id = Guid.NewGuid();
                entity.ItemId = itemId;
                entity.SiteMenuId = menu.Id;
                  
                //table.Add(entity);
                  
                //dbContext.SaveChanges();

                return entity;
            }
        }

        public void GetMenuAndTranslate(int andromedaSiteId, Action<MenuDbJob> workWithSiteMenuWhileOpen) 
        {
            using (var dbConext = NewContext())
            {
                var menu = this.siteMenuDataService.GetMenu(andromedaSiteId); //this.GetMenuWithContext(dbConext, andromedaSiteId);

                var menuItemQuery = dbConext.MenuItems.Where(e => e.SiteMenu.AndromediaId == andromedaSiteId);
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