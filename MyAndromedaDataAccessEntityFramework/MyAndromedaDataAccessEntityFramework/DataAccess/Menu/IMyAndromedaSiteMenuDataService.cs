using System;
using System.Linq;
using System.Data;
using System.Data.Entity;
using MyAndromeda.Core;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaSiteMenuDataService : IDependency 
    {


        /// <summary>
        /// Sets the version.
        /// </summary>
        /// <param name="siteMenuFtpBackup">The site menu FTP backup.</param>
        //void SetVersion(SiteMenu siteMenuFtpBackup);

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
        /// Sets the upload flag.
        /// </summary>
        /// <param name="siteMenuFtp">The site menu FTP.</param>
        /// <param name="value">The value.</param>
        void SetUploadTask(SiteMenu siteMenu, TaskStatus status);

        /// <summary>
        /// Sets the download flag.
        /// </summary>
        /// <param name="siteMenuFtp">The site menu FTP.</param>
        /// <param name="value">The value.</param>
        void SetDownloadTask(SiteMenu siteMenu, TaskStatus status);
        void SetLastDownloadDate(SiteMenu siteMenu, DateTime dateUtc);

        void CreateAcsUploadMenuDataTask(SiteMenu menu);
        void SetAcsUploadMenuDataTaskStatus(SiteMenu menu, TaskStatus status);

        /// <summary>
        /// Resets the upload tasks.
        /// </summary>
        /// <param name="notFinishedBy">The not finished by.</param>
        IEnumerable<SiteMenu> ResetUploadTasks(DateTime notFinishedBy);

        /// <summary>
        /// Resets the download tasks.
        /// </summary>
        /// <param name="notUploadedBy">The not uploaded by.</param>
        IEnumerable<SiteMenu> ResetDownloadTasks(DateTime notUploadedBy);
    }
}