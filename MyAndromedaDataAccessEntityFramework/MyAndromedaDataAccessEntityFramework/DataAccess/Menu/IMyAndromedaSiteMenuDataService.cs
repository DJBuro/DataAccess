using System;
using System.Linq;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaSiteMenuDataService : IDependency 
    {
        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="siteMenuFtpBackup">The site menu FTP backup.</param>
        //void SetVersion(SiteMenu siteMenuFtpBackup);
        //void NewPublishDate(SiteMenu siteMenu, DateTime? menuPublishDate);
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

        /// <summary>
        /// Updates the specified site menu.
        /// </summary>
        /// <param name="siteMenu">The site menu.</param>
        void Update(SiteMenu siteMenu);

        /// <summary>
        /// Sets the version from the database file.
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="version">The version.</param>
        void SetVersion(SiteMenu siteMenu);

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<SiteMenu> List(Expression<Func<SiteMenu, bool>> query);

        /// <summary>
        /// Deletes the menu.
        /// </summary>
        /// <param name="siteMenu">The site menu.</param>
        void Delete(SiteMenu siteMenu);
    }
}