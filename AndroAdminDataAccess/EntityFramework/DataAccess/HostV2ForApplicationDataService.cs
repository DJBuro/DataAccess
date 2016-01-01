using System;
using System.Collections.Generic;
using System.Linq;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class HostV2ForApplicationDataService : IHostV2ForApplicationDataService 
    {
        public IEnumerable<ACSApplication> ListByHostId(Guid id)
        {
            IEnumerable<ACSApplication> results = Enumerable.Empty<ACSApplication>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.ACSApplications;
                var query = table.Where(e => e.HostV2.Any(host => host.Id == id));

                results = query.ToArray();
            }

            return results;
        }

        public IEnumerable<HostV2> ListConnectedHostsForApplication(int applicationId)
        {
            IEnumerable<HostV2> results = Enumerable.Empty<HostV2>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HostV2;
                var query = table.Where(e => e.ACSApplications.Any(application => application.Id == applicationId));

                results = query.ToArray();
            }

            return results;
        }

        public void AddRange(int applicationId, IEnumerable<Guid> selectServerListIds)
        {
            var idCollection = selectServerListIds.ToArray();
            using (var dbContext = new AndroAdminEntities())
            {
                var applications = dbContext.ACSApplications;
                var hostListTable = dbContext.HostV2;

                var application = applications.SingleOrDefault(e => e.Id == applicationId);
                var serversQuery = hostListTable.Where(e => idCollection.Contains(e.Id)).ToArray();

                if (application.HostV2 == null)
                {
                    application.HostV2 = new List<HostV2>();
                }

                application.HostV2.Clear();

                foreach (var server in serversQuery)
                {
                    application.HostV2.Add(server);
                }

                dbContext.SaveChanges();
            }
        }

        public void ClearAll(int applicationId)
        {
            using (var dbContext = new AndroAdminEntities())
            {
                var ApplicationTable = dbContext.ACSApplications;
                var hostListTable = dbContext.HostV2;

                var application = ApplicationTable.SingleOrDefault(e => e.Id == applicationId);

                if (application.HostV2 == null)
                {
                    application.HostV2 = new List<HostV2>();
                }
                application.HostV2.Clear();

                dbContext.SaveChanges();
            }
        }
    }
}