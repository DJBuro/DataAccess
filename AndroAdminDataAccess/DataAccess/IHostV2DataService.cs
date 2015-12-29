using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AndroAdminDataAccess.EntityFramework;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IHostV2DataService 
    {
        /// <summary>
        /// Updates the version for all.
        /// </summary>
        void UpdateVersionForAll();

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Add(HostV2 model);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(HostV2 model);

        /// <summary>
        /// Disables the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        void Disable(Guid id);

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<HostV2> List(Expression<Func<HostV2, bool>> query = null);

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <typeparam name="T">The type of the T.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="transformation">The transformation.</param>
        /// <returns></returns>
        IEnumerable<T> List<T>(Expression<Func<HostV2, bool>> query, Func<HostV2, T> transformation);

        /// <summary>
        /// Lists the deleted.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<HostV2> ListDeleted(Expression<Func<HostV2, bool>> query = null);

        /// <summary>
        /// Destroys the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Destroy(HostV2 model);
    }
}