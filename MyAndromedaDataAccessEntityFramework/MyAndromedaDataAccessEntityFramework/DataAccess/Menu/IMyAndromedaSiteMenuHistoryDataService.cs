using System;
using MyAndromeda.Core;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public interface IMyAndromedaSiteMenuHistoryDataService : IDependency 
    {
        void Create(Guid siteMenuId,
            DateTime createdOnUtc, DateTime PublishedOnUtc, string userName,
            bool publishAll,
            bool publishMenu,
            bool publishThumbnails);
    }
}