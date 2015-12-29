using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.EntityFramework.Extensions
{
    public static class DbExtensions
    {
        public static AndroAdminDataAccess.Domain.HubItem ToDomain(this HubAddress hubAddress)
        {
            return new AndroAdminDataAccess.Domain.HubItem()
            {
                Id = hubAddress.Id,
                Name = hubAddress.Name,
                Removed = hubAddress.Removed,
                Address = hubAddress.Address,
                Active = hubAddress.Active
            };
        }

        public static AndroAdminDataAccess.Domain.StoreHub ToStoreHubDomainModel(this Store store, AndroAdminDataAccess.Domain.HubItem hub) 
        {
            return new Domain.StoreHub()
            {
                HubAddressId = hub.Id,
                StoreExternalId = store.ExternalId
            };
        }

        public static AndroAdminDataAccess.Domain.Store ToDomain(this Store store) 
        {
            return new Domain.Store() 
            { 
                Id =store.Id,
                AndromedaSiteId = store.AndromedaSiteId,
                ExternalSiteId = store.ExternalId
            };
        }
    }
}
