using AndroCloudDataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroCloudDataAccessEntityFramework.Extensions
{
    public static class ModelExtensions
    {

        public static AndroCloudDataAccess.Domain.HostV2 ToPublicDomainModel(this Model.HostsV2 dbItem)
        {
            return new AndroCloudDataAccess.Domain.HostV2()
            {
                Id = dbItem.Id,
                Order = dbItem.Order,
                Type = dbItem.HostType.Name,
                Url = dbItem.Url,
                Version = dbItem.Version
            };
        }

        public static AndroCloudDataAccess.Domain.Host ToPublicDomainModel(this Model.Host dbItem)
        {
            return new AndroCloudDataAccess.Domain.Host()
            {
                Url = dbItem.HostName,
                Order = dbItem.Order
            };
        }

        public static PrivateHostV2 ToPrivateDomainModel(this Model.HostsV2 dbItem)
        {
            return new PrivateHostV2()
            {
                Id = dbItem.Id,
                Order = dbItem.Order,
                Type = dbItem.HostType.Name,
                Url = dbItem.Url,
                Version = dbItem.Version
            };
        }

        public static PrivateHost ToPrivateDomainModel(this Model.Host dbItem) 
        {
            return new AndroCloudDataAccess.Domain.PrivateHost()
            {
                Url = dbItem.PrivateHostName,
                Order = dbItem.Order,
                SignalRUrl = dbItem.SignalRHostName
            };
        }

    }
}
