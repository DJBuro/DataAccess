using System;
using System.Linq;
using MyAndromeda.Core;
using MyAndromeda.Storage.Azure;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaSiteMediaServerDataService : IDependency
    {
        /// <summary>
        /// Gets the default Media Server.
        /// </summary>
        /// <returns></returns>
        SiteMenuMediaServer GetDefault();

        /// <summary>
        /// Gets the media server, with default if there is no alternative attached.
        /// </summary>
        /// <param name="andromedaSiteId">The Andromeda site id.</param>
        /// <returns></returns>
        SiteMenuMediaServer GetMediaServerWithDefault(int andromedaSiteId);
    }

    public class MyAndromedaSiteMediaServerService : IMyAndromedaSiteMediaServerDataService 
    {
        private readonly IMyAndromedaDbWorkContextAccessor dbWork;

        private readonly IBlobStorageService blobStorageService;

        public MyAndromedaSiteMediaServerService(IMyAndromedaDbWorkContextAccessor dbWork, IBlobStorageService blobStorageService)
        { 
            this.dbWork = dbWork;
            this.blobStorageService = blobStorageService;
        }

        public SiteMenuMediaServer GetDefault()
        {
            var table = this.dbWork.DbContext.SiteMenuMediaServers;
            var query = table.Where(e => e.Name.Equals("Default"));
            var result = query.SingleOrDefault();

            if (result == null) { 
                result = new SiteMenuMediaServer(){
                    Name = "Default",
                    Address = this.blobStorageService.RemoteLocation(),
                    ContentPath = "https://{0}/menus/{1}/",
                    StoragePath = "menus/{0}"
                };
                this.dbWork.DbContext.SiteMenuMediaServers.Add(result);
                this.dbWork.DbContext.SaveChanges();
            }

            return result;
        }

        public SiteMenuMediaServer GetMediaServerWithDefault(int andomediaSiteId)
        {
            var table = this.dbWork.DbContext.SiteMenuMediaServers;
            var query = table.Where(e => e.SiteMenus.Any(siteMenu => siteMenu.AndromediaId == andomediaSiteId));
            var result = query.FirstOrDefault();

            if (result != null)
            {
                return result;
            }

            result = this.GetDefault();

            return result;
        }
    }
}
