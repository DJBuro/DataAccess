using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class GroupsDataAccess : IGroupsDataAccess
    {
        //public string GetByExternalId(string partnerId, out AndroCloudDataAccess.Domain.Chain chain)
        //{
        //    chain = null;
        //    var acsEntities = new ACSEntities();

        //    var chainQuery = from c in acsEntities.Chains
        //                     where c.ExternalId == partnerId
        //                     && c.ExternalId == externalChainId
        //                     select c;

        //    var chainEntity = chainQuery.FirstOrDefault();

        //    if (chainEntity != null)
        //    {
        //        chain = new AndroCloudDataAccess.Domain.Chain();
        //        chain.Id = chainEntity.ID;
        //        chain.ChainName = chainEntity.ChainName;
        //        chain.LastUpdated = chainEntity.LastUpdated;
        //        chain.PartnerID = chainEntity.PartnerID;
        //    }

        //    return "";
        //}

        public string Get(Guid partnerId, string externalChainId, out AndroCloudDataAccess.Domain.Group chain)
        {
            chain = null;
            var acsEntities = new ACSEntities();

            var chainQuery = from c in acsEntities.Groups
                             where c.PartnerID == partnerId
                             && c.ExternalId == externalChainId
                             select c;

            var chainEntity = chainQuery.FirstOrDefault();

            if (chainEntity != null)
            {
                chain = new AndroCloudDataAccess.Domain.Group();
                chain.Id = chainEntity.ID;
                chain.ChainName = chainEntity.GroupName;
                chain.LastUpdated = chainEntity.LastUpdated;
                chain.PartnerID = chainEntity.PartnerID;
            }

            return "";
        }
    }
}
