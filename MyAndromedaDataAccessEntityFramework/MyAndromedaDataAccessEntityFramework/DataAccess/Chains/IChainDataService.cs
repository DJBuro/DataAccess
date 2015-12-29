using MyAndromeda.Core;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using MyAndromedaDataAccess.Domain;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Chains
{
    public interface IChainDataService : IDependency
    {
        /// <summary>
        /// Gets the specified chain id.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        MyAndromedaDataAccess.Domain.Chain Get(int chainId);

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<MyAndromedaDataAccess.Domain.Chain> List(Expression<Func<Model.AndroAdmin.Chain, bool>> query);

        /// <summary>
        /// Gets the site list of a chain.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        IEnumerable<MyAndromedaDataAccess.Domain.Site> GetSiteList(int chainId);

        /// <summary>
        /// Updates the specified chain.
        /// </summary>
        /// <param name="chain">The chain.</param>
        void Update(MyAndromedaDataAccess.Domain.Chain chain); 
        
    }
}
