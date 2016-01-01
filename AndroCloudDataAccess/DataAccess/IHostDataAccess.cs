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
        /// Gets the public by external application id.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="applicationHostList">The application host list.</param>
        /// <returns></returns>
        string GetAllPublicHostsByExternalApplicationId(string externalApplicationId, out List<Host> applicationHostList);

        /// <summary>
        /// Gets the public hosts by external site id.
        /// </summary>
        /// <param name="externalSiteId">The external site id.</param>
        /// <param name="applicationHostList">The application host list.</param>
        /// <returns></returns>
        string GetAllPublicHostsByExternalSiteId(string externalSiteId, out List<Host> applicationHostList);

        /// <summary>
        /// Gets all public.
        /// </summary>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllGenericPublic(out List<Host> hosts);

        /// <summary>
        /// Gets the best public hosts.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetBestPublicHosts(string externalApplicationId, out List<Host> hosts);

        /// <summary>
        /// Gets the best public hosts.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="externalSiteId">The external site id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetBestPublicHosts(string externalApplicationId, string externalSiteId, out List<Host> hosts);

        /// <summary>
        /// Gets the best public hosts v2.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="applicationHostList">The application host list.</param>
        /// <returns></returns>
        string GetBestPublicHostsV2(string externalApplicationId, out List<HostV2> applicationHostList);

        /// <summary>
        /// Gets the public v2 by external application id.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="applicationHostList">The application host list.</param>
        string GetAllPublicV2HostsByExternalApplicationId(string externalApplicationId, out List<HostV2> applicationHostList);

        /// <summary>
        /// Gets all public v2.
        /// </summary>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllGenericPublicV2Hosts(out List<HostV2> hosts);
        
        /// <summary>
        /// Gets all private.
        /// </summary>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllGenericPrivateHosts(out List<AndroCloudDataAccess.Domain.PrivateHost> hosts);

        /// <summary>
        /// Gets the best private.
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetBestPrivate(int andromedaSiteId, out List<PrivateHost> hosts);

        /// <summary>
        /// Gets the best list of private v2 hosts.
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="acsApplicationId">The acs application id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetBestPrivateV2(int andromedaSiteId, int acsApplicationId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);
        

        /// <summary>
        /// Gets the best list of private v2 for the site id.
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetBestPrivateV2(int andromedaSiteId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);

        /// <summary>
        /// Gets all private v2 by Andromeda site id.
        /// </summary>
        /// <param name="acsApplicationId">The acs application id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPrivateV2ByAcsApplicationId(int acsApplicationId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);

        /// <summary>
        /// Gets all private host v2 by external application id.
        /// </summary>
        /// <param name="externalApplicationId">The external application id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPrivateHostV2ByExternalApplicationId(string externalApplicationId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);

        /// <summary>
        /// Gets all private v2 by site id.
        /// </summary>
        /// <param name="siteId">Site Id GUID.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPrivateV2BySiteId(Guid siteId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);

        /// <summary>
        /// Gets all private v2 by site id.
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllPrivateV2ByAndromedaSiteId(int andromedaSiteId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);

        /// <summary>
        /// Gets all private v2 (generic class servers).
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="hosts">The hosts.</param>
        /// <returns></returns>
        string GetAllGenericPrivateV2Hosts(out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts);

        
    }
}
