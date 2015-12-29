using System;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaSiteMenuDataService : IDependency 
    {
        SiteMenu GetMenu(int andromedaSiteId);

        /// <summary>
        /// Creates the specified Andromeda site id.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        SiteMenu Create(int andromedaSiteId);
    }

    public class MyAndromedaSiteMenuDataService : IMyAndromedaSiteMenuDataService 
    {
        private readonly IMyAndromedaDbWorkContextAccessor dbWork;

        public MyAndromedaSiteMenuDataService(IMyAndromedaDbWorkContextAccessor dbWork)
        { 
            this.dbWork = dbWork;
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
}