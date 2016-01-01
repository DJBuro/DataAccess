using System;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaSiteMenuDataService : IDependency 
    {
        SiteMenu GetMenu(int andromedaSiteId);
    }

    public class MyAndromedaSiteMenuDataService : IMyAndromedaSiteMenuDataService 
    {
        private readonly IMyAndromedaDbWorkContextAccessor dbWork;

        public MyAndromedaSiteMenuDataService(IMyAndromedaDbWorkContextAccessor dbWork)
        { 
            this.dbWork = dbWork;
        }

        public SiteMenu GetMenu(int andromedaSiteId)
        {
            var table = this.dbWork.DbContext.SiteMenus;
            var query = table.Where(e => e.AndromediaId == andromedaSiteId);
            var result = query.SingleOrDefault();

            return result;
        }
    }
}