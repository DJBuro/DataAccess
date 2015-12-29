using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AndroAdminDataAccess.EntityFramework;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IHostV2ForStoreDataService 
    {
        IEnumerable<HostStoreConnection> ListHostConnections(Expression<Func<Store, bool>> query);

        IEnumerable<Store> ListByHostId(Guid id);

        IEnumerable<HostV2> ListConnectedHostsForSite(int storeId);

        void AddRange(int storeId, IEnumerable<Guid> selectServerListIds);
        void ClearAll(int storeId);
    }

    public interface IHostV2ForApplicationDataService
    {
        //List<T> List<T>(Expression<Func<ACSApplication, bool>> query, Func<ACSApplication, T> transform = null);

        IEnumerable<ACSApplication> ListByHostId(Guid id);
        IEnumerable<HostV2> ListConnectedHostsForApplication(int applicationId);
        IEnumerable<HostApplicationConnection> ListHostConnections(Expression<Func<ACSApplication, bool>> query); 
        void AddRange(int applicationId, IEnumerable<Guid> selectServerListIds);
        void ClearAll(int applicationId);
    } 
    
    public class HostApplicationConnection 
    {
        public Guid HostId { get; set; }
        public int ApplicationId { get; set; }
    }

    public class HostStoreConnection
    {
        public Guid HostId { get; set; }
        public int AndromedaSiteId { get; set; }
    }
}