using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaSiteMenuDataService : IDependency 
    {
        
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
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<SiteMenu> List(Expression<Func<SiteMenu, bool>> query);

        /// <summary>
        /// Sets the upload flag.
        /// </summary>
        /// <param name="siteMenuFtp">The site menu FTP.</param>
        /// <param name="value">The value.</param>
        void SetUploadFlag(SiteMenuFtpBackup siteMenuFtp, bool value = true);

        /// <summary>
        /// Sets the download flag.
        /// </summary>
        /// <param name="siteMenuFtp">The site menu FTP.</param>
        /// <param name="value">The value.</param>
        void SetDownloadFlag(SiteMenuFtpBackup siteMenuFtp, bool value = true);

    }
}