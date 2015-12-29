using System;
using System.Linq;
using System.Data.Entity;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Extensions;
using AndroCloudDataAccessEntityFramework.Model;
using System.Linq.Expressions;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class HostDataAccess : IHostDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetAllPublicHostsByExternalApplicationId(string externalApplicationId, out List<AndroCloudDataAccess.Domain.Host> applicationHostList)
        {
            var results = this
                .QueryHost(e => e.ACSApplications.Any(application => application.ExternalApplicationId == externalApplicationId))
                .OrderBy(e=> e.Order)
                .Select(e => e.ToPublicDomainModel());

            applicationHostList = results.ToList();

            return string.Empty;
        }

        public string GetAllPublicHostsByExternalSiteId(string externalSiteId, out List<AndroCloudDataAccess.Domain.Host> applicationHostList)
        {
            var results = this
                .QueryHost(e => e.Sites.Any(site => site.ExternalId == externalSiteId))
                .OrderBy(e=> e.Order)
                .Select(e=> e.ToPublicDomainModel());

            applicationHostList = results.ToList();

            return string.Empty;
        }

        public string GetAllGenericPublic(out List<AndroCloudDataAccess.Domain.Host> hosts)
        {
            var results = this
                .QueryHost(e => !e.OptInOnly)
                .OrderBy(e => e.Order)
                .Select(e=> e.ToPublicDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }

        public string GetBestPublicHosts(string externalApplicationId, out List<AndroCloudDataAccess.Domain.Host> hosts)
        {
            this.GetAllPublicHostsByExternalApplicationId(externalApplicationId, out hosts);

            if (hosts.Count > 0) { return string.Empty; }

            this.GetAllGenericPublic(out hosts);

            return string.Empty;
        }

        public string GetBestPublicHosts(string externalApplicationId, string externalSiteId, out List<AndroCloudDataAccess.Domain.Host> hosts)
        {
            this.GetAllPublicHostsByExternalSiteId(externalSiteId, out hosts);

            if (hosts.Count > 0) { return string.Empty; }

            this.GetAllPublicHostsByExternalApplicationId(externalApplicationId, out hosts);

            if (hosts.Count > 0) { return string.Empty; }

            this.GetAllGenericPublic(out hosts);

            return string.Empty;
        }

        public string GetBestPublicHostsV2(string externalApplicationId, out List<AndroCloudDataAccess.Domain.HostV2> applicationHostList)
        {
            this.GetAllPublicV2HostsByExternalApplicationId(externalApplicationId, out applicationHostList);

            //have and exit 
            if (applicationHostList.Count > 0) { return string.Empty; }

            this.GetAllGenericPublicV2Hosts(out applicationHostList);

            return string.Empty;
        }

        public string GetAllPublicV2HostsByExternalApplicationId(string externalApplicationId, out List<AndroCloudDataAccess.Domain.HostV2> applicationHostList)
        {
            var results = this
                .QueryPublicHostsV2
                (
                    e => e.ACSApplications.Any(application => application.ExternalApplicationId == externalApplicationId)
                )
                .Select(e => e.ToPublicDomainModel())
                .ToList();

            applicationHostList = results;

            return string.Empty;
        }

        public string GetAllGenericPrivateHosts(out List<AndroCloudDataAccess.Domain.PrivateHost> hosts)
        {
            var results = this
                .QueryHost(e => !e.OptInOnly)
                .Select(e=> e.ToPrivateDomainModel());
            
            hosts = results.ToList();
            
            return string.Empty;
        }

        public string GetBestPrivate(int andromedaSiteId, out List<AndroCloudDataAccess.Domain.PrivateHost> hosts)
        {
            var query = this
                .QueryHost
                (
                    e => 
                        e.Sites.Any(site => site.AndroID == andromedaSiteId)
                    ||  //direct site link or indirectly by a matched application id
                        e.ACSApplications.Any(application => application.ACSApplicationSites.Any(site => site.Site.AndroID == andromedaSiteId))
                ).ToArray();

            if(query.Length > 0)
            {
                hosts = query.Select(e => e.ToPrivateDomainModel()).ToList();

                return string.Empty;
            }

            return this.GetAllGenericPrivateHosts(out hosts);
        }

        private IEnumerable<Host> QueryHost(Expression<Func<Host, bool>> query) 
        {
            var results = Enumerable.Empty<Host>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                var acsQuery = acsEntities.Hosts.Where(query);
                results = acsQuery.ToArray();
            }

            return results;
        }

        private IEnumerable<HostsV2> QueryPublicHostsV2(Expression<Func<HostsV2, bool>> query) 
        {
            var results = Enumerable.Empty<HostsV2>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsTable = acsEntities.HostsV2
                    .Include(e => e.HostType);

                var acsQuery = acsTable
                    .Where(e=> e.Public).Where(query)
                    .OrderBy(e=> e.HostType.Name)
                    .ThenBy(e=> e.Order)
                    .ToArray();

                results = acsQuery;
            }

            return results;
        }

        private IEnumerable<HostsV2> QueryHostsV2(Expression<Func<HostsV2, bool>> query) 
        {
            var results = Enumerable.Empty<HostsV2>();

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var acsTable = acsEntities.HostsV2
                    .Include(e => e.HostType);

                var acsQuery = acsTable
                    .Where(query)
                    .OrderBy(e => e.HostType.Name)
                    .ThenBy(e => e.Order)
                    .ToArray();

                results = acsQuery;
            }

            return results;
        }

        public string GetAllGenericPublicV2Hosts(out List<AndroCloudDataAccess.Domain.HostV2> hosts)
        {
            var results = this.QueryPublicHostsV2(e => !e.OptInOnly).Select(e => e.ToPublicDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }

        public string GetBestPrivateV2(int andromedaSiteId, int acsApplicationId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            var siteSpecificError = this.GetAllPrivateV2ByAndromedaSiteId(andromedaSiteId, out hosts);

            if (hosts.Count > 0) { return siteSpecificError; }

            var applicationSpecificError = this.GetAllPrivateV2ByAcsApplicationId(acsApplicationId, out hosts);

            if (hosts.Count > 0) { return applicationSpecificError; }

            return this.GetAllGenericPrivateV2Hosts(out hosts);
        }

        public string GetBestPrivateV2(int andromedaSiteId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            var siteSpecificError = this.GetAllPrivateV2ByAndromedaSiteId(andromedaSiteId, out hosts);

            if (hosts.Count > 0) { return siteSpecificError; }

            //find any linked by application id or by site id
            
            var results = this
                .QueryHostsV2(e => 
                    e.Sites.Any(site => site.AndroID == andromedaSiteId) 
                    || //direct site link or indirectly by a matched application id
                    e.ACSApplications.Any(app => app.ACSApplicationSites.Any(site => site.Site.AndroID == andromedaSiteId))
                ).Select(e=> e.ToPrivateDomainModel()).ToList();

            hosts = results;

            if (hosts.Count > 0) { return string.Empty; }

            return this.GetAllGenericPrivateV2Hosts(out hosts);
        }

        public string GetAllPrivateV2BySiteId(Guid guid, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            var results = this.QueryHostsV2(e => e.Sites.Any(site => site.ID == guid)).Select(hostEntity => hostEntity.ToPrivateDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }

        public string GetAllPrivateV2ByAndromedaSiteId(int andromedaSiteId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            var results = this
                .QueryHostsV2(e => e.Sites.Any(site => site.AndroID == andromedaSiteId))
                .Select(e=> e.ToPrivateDomainModel());
            
            hosts = results.ToList();

            return string.Empty;
        }

        public string GetAllPrivateV2ByAcsApplicationId(int acsApplicationId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            var results = this
                .QueryHostsV2(e => e.ACSApplications.Any(application => application.Id == acsApplicationId))
                .Select(e=> e.ToPrivateDomainModel());

            hosts = results.ToList();
            
            return string.Empty;
        }
        
        public string GetAllPrivateHostV2ByExternalApplicationId(string externalApplicationId, out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            var results = this
                .QueryHostsV2(e => e.ACSApplications.Any(application => application.ExternalApplicationId == externalApplicationId))
                .Select(e => e.ToPrivateDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }

        public string GetAllGenericPrivateV2Hosts(out List<AndroCloudDataAccess.Domain.PrivateHostV2> hosts)
        {
            var results = this
                .QueryHostsV2(e => !e.OptInOnly)
                .Select(e => e.ToPrivateDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }
        
        public string GetMenuChangedHosts(out List<AndroCloudDataAccess.Domain.HostV2> hosts)
        {
            var results = this
                .QueryHostsV2(e =>
                                          //WebHooks - Update Menu
                    e.HostType.Name.Equals("WebHooks - Update Menu", StringComparison.CurrentCultureIgnoreCase))
                .Select(e=> e.ToPublicDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }


        public string GetOrderStatusChangedHosts(out List<AndroCloudDataAccess.Domain.HostV2> hosts)
        {
            var results = this
                .QueryHostsV2(e =>
                                          //WebHooks - Update Order Status
                    e.HostType.Name.Equals("WebHooks - Update Order Status", StringComparison.CurrentCultureIgnoreCase))
                .Select(e => e.ToPublicDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }

        public string GetEdtChangedHosts(out List<AndroCloudDataAccess.Domain.HostV2> hosts)
        {
            var results = this
                .QueryHostsV2(e =>
                                          //WebHooks - Update EDT
                    e.HostType.Name.Equals("WebHooks - Update EDT", StringComparison.CurrentCultureIgnoreCase))
                .Select(e => e.ToPublicDomainModel());

            hosts = results.ToList();

            return string.Empty;
        }
    }
}
