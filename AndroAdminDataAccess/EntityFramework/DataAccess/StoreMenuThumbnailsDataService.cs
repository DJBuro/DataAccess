﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreMenuThumbnailsDataService : IStoreMenuThumbnailsDataService
    {
        public IEnumerable<AndroAdminDataAccess.Domain.StoreMenuThumbnails> GetAfterDataVersion(int fromVersion)
        {
            using (var dbContext = new AndroAdminDataAccess.EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.StoreMenuThumbnails;
                var query = table.Where(e => e.Version > fromVersion);
                var result = query.ToArray();
                var resultGroup = result.Select(e => {
                    var storeEntity = dbContext.Stores.Single(store => store.Id == e.Id);

                    return new[] {
                        new AndroAdminDataAccess.Domain.StoreMenuThumbnails() { 
                            Id = e.Id,
                            Version = e.Version,
                            MenuType = "xml",
                            Data = e.XmlMenuThumbnailData,
                            AndromediaSiteId = storeEntity.AndromedaSiteId
                        },
                        new AndroAdminDataAccess.Domain.StoreMenuThumbnails(){
                            Id = e.Id,
                            Version = e.Version,
                            MenuType = "json",
                            Data = e.JsonMenuThumbnailsData,
                            AndromediaSiteId = storeEntity.AndromedaSiteId
                        }
                    };
                });

                return resultGroup.SelectMany(e=> e);
            }
        }
    }
}
