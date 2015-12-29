using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class GroupsDataAccess : IGroupsDataAccess
    {
        //public string GetByExternalId(string partnerId, out MyAndromedaDataAccess.Domain.Chain chain)
        //{
        //    chain = null;
        //    var androAdminEntities = new AndroAdminEntities();

        //    var chainQuery = from c in androAdminEntities.Chains
        //                     where c.ExternalId == partnerId
        //                     && c.ExternalId == externalChainId
        //                     select c;

        //    var chainEntity = chainQuery.FirstOrDefault();

        //    if (chainEntity != null)
        //    {
        //        chain = new MyAndromedaDataAccess.Domain.Chain();
        //        chain.Id = chainEntity.ID;
        //        chain.ChainName = chainEntity.ChainName;
        //        chain.LastUpdated = chainEntity.LastUpdated;
        //        chain.PartnerID = chainEntity.PartnerID;
        //    }

        //    return "";
        //}

        public string Get(Guid partnerId, string externalChainId, out MyAndromedaDataAccess.Domain.Group chain)
        {
            chain = null;
            //AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            //var chainQuery = from c in androAdminEntities.Groups
            //                 //                where c.PartnerId == partnerId
            //                 where c.ExternalId == externalChainId
            //                 select c;

            //var chainEntity = chainQuery.FirstOrDefault();

            //if (chainEntity != null)
            //{
            //    chain = new MyAndromedaDataAccess.Domain.Group();
            //    chain.Id = chainEntity.Id;
            //    chain.ChainName = chainEntity.GroupName;
            //    chain.LastUpdated = chainEntity.LastUpdated;
            //    chain.PartnerId = chainEntity.PartnerId;
            //}

            return "";
        }
    }
}
