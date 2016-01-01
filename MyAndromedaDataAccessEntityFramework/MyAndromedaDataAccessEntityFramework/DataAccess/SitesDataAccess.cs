using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.EntityModel;
using System.Data.Entity;

using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace MyAndromedaDataAccessEntityFramework.DataAccess
{
    public class SitesDataAccess : ISiteDataAccess
    {
        public void GetExternalAcsApplicationIds(int siteId, out IEnumerable<string> acsExternalApplicationIds)
        {
            acsExternalApplicationIds = Enumerable.Empty<string>();

            using (var dbContext = new AndroAdminDbContext())
            {
                var table = dbContext.ACSApplications;
                var query = table
                                 .Where(e => e.ACSApplicationSites.Any(site => site.SiteId == siteId))
                                 .Select(e => e.ExternalApplicationId);
                var result = query.ToArray();

                acsExternalApplicationIds = result;
            }
        }

        public string GetExternalApplicationIds(int siteId, out IEnumerable<string> externalApplicationIds)
        {
            externalApplicationIds = Enumerable.Empty<string>();

            using (var dbContext = new AndroAdminDbContext()) 
            {
                var query = dbContext
                    .ACSApplications
                    .Where(e => e.ACSApplicationSites.Any(acsSite => acsSite.SiteId == siteId))
                    .Select(e => e.ExternalApplicationId);
                
                var result = query.ToArray();

                externalApplicationIds = result;
            }

            return string.Empty;
        }

        public string GetAcsApplicationIds(int siteId, out IEnumerable<int> application)
        {
            application = null;
            using (var dbContext = new AndroAdminDbContext())
            {
                var query = dbContext.ACSApplications.Where(e => e.ACSApplicationSites.Any(acsSite => acsSite.SiteId == siteId));
                var result = query.Select(e => e.Id).ToArray();
                application = result;
            }

            return string.Empty;
        }

        public string GetById(int siteId, out MyAndromedaDataAccess.Domain.Site site)
        {
            site = null;
            using (var entitiesContext = new AndroAdminDbContext())
            {
                var table = entitiesContext.Stores.Include(e => e.Address);
                var query = table.Where(e => e.Id == siteId);

                Store entity = query.FirstOrDefault();

                if (entity != null)
                {
                    site = entity.ToDomainModel();
                }
            }

            return string.Empty;
        }

    }
}
