using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IHostDataAccess
    {
        string ConnectionStringOverride { get; set; }

        /// <summary>
        /// Gets the public.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="applicationHostList">The application host list.</param>
        /// <returns></returns>
        string GetPublic(string externalApplicationId, out List<Host> applicationHostList);

        /// <summary>
        /// Gets all public.
        /// </summary>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPublic(out List<Host> hosts);

        /// <summary>
        /// Gets all public v2.
        /// </summary>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPublicV2(out List<HostV2> hosts);

        /// <summary>
        /// Gets all private.
        /// </summary>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPrivate(out List<AndroCloudDataAccess.Domain.PrivateHost> hosts);

        /// <summary>
        /// Gets all private v2.
        /// </summary>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPrivateV2(out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);

        /// <summary>
        /// Gets the public v2.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="applicationHostList">The application host list.</param>
        void GetPublicV2(string externalApplicationId, out List<HostV2> applicationHostList);
    }
}
