using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<HostApplicationConnection> ListHostConnections(Expression<Func<ACSApplication, bool>> query)
        {
            IEnumerable<HostApplicationConnection> results = Enumerable.Empty<HostApplicationConnection>();
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.ACSApplications;
                var innerQuery =
                    (
                        from application in table
                        from hostV2List in application.HostV2
                        select new HostApplicationConnection
                        {
                            ApplicationId = application.Id,
                            HostId = hostV2List.Id
                        }
                    );

                results = innerQuery.ToArray();
            }

            return results;
        }

        public void AddCompleteRange(int applicationId, IEnumerable<Guid> selectServerListIds)
        {
            var newIdCollection = selectServerListIds.ToArray();
            
            using (var dbContext = new AndroAdminEntities())
            {
                var applicationTable = dbContext.ACSApplications;
                var hostListTable = dbContext.HostV2;

                var applicationEntity = applicationTable.SingleOrDefault(e => e.Id == applicationId);

                var oldConnectedHosts = applicationEntity.HostV2.ToArray();
                var newConnectionHostEntities = hostListTable.Where(e => newIdCollection.Contains(e.Id)).ToArray();

                var newDataVersion = dbContext.GetNextDataVersionForEntity();
                if (applicationEntity.HostV2 == null)
                {
                    applicationEntity.HostV2 = new List<HostV2>();
                }

                applicationEntity.HostV2.Clear();
                dbContext.SaveChanges();

                foreach (var server in newConnectionHostEntities)
                {
                    server.DataVersion = newDataVersion;
                    applicationEntity.HostV2.Add(server);
                }

                var updateRemovedHosts = oldConnectedHosts.Where(e => !newConnectionHostEntities.Any(server => server.Id == e.Id));
                foreach (var server in updateRemovedHosts)
                {
                    server.DataVersion = newDataVersion;
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