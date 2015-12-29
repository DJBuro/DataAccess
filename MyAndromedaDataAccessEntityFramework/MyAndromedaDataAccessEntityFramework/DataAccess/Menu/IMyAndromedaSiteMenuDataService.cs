using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
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

        void Update(SiteMenu siteMenu);
    }
}