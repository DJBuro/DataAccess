using System;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaMediaProfilesDataService : IDependency 
    {
        IQueryable<SiteMenuMediaProfileSize> Query();
    }

    public class MyAndromedaMediaProfilesDataService : IMyAndromedaMediaProfilesDataService 
    {
        private MyAndromedaDbContext dbContext;

        public MyAndromedaMediaProfilesDataService() 
        {
            this.dbContext = new Model.MyAndromeda.MyAndromedaDbContext();
        }

        public IQueryable<SiteMenuMediaProfileSize> Query()
        {
            var table = dbContext.SiteMenuMediaProfileSizes;
            return table;
        }
    }
}