using MyAndromeda.Core;
using MyAndromeda.Core.User;
using MyAndromeda.Logging;
using MyAndromedaDataAccess.Domain;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Users
{
    public interface IUserChainsDataService : IDependency
    {
        /// <summary>
        /// Gets the chains for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IEnumerable<Chain> GetChainsForUser(int userId);

        /// <summary>
        /// Gets the chains for user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<Chain> GetChainsForUser(int userId, Expression<Func<Model.AndroAdmin.Chain, bool>> query);

        /// <summary>
        /// Adds the chain to user.
        /// </summary>
        /// <param name="chain">The chain.</param>
        /// <param name="userId">The user id.</param>
        void AddChainLinkToUser(Chain chain, int userId);

        /// <summary>
        /// Finds the users belonging to chain.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        IEnumerable<MyAndromedaUser> FindUsersDirectlyBelongingToChain(int chainId);

        /// <summary>
        /// Finds the chains directly belonging to user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IEnumerable<Chain> FindChainsDirectlyBelongingToUser(int userId);

        /// <summary>
        /// Removes the chain link to user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="chainId">The chain id.</param>
        void RemoveChainLinkToUser(int userId, int chainId);
    }
}
