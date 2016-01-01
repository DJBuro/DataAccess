using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AndroAdminDataAccess.EntityFramework;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IHostTypesDataService 
    {
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Add(HostType model);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Update(HostType model);

        /// <summary>
        /// Destroys the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        void Destroy(HostType model);

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<HostType> List(Expression<Func<HostType, bool>> query = null); 
    }
}