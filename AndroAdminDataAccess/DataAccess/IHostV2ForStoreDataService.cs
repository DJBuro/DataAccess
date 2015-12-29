using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AndroAdminDataAccess.EntityFramework;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IHostV2ForStoreDataService 
    {
        IEnumerable<Store> ListByHostId(Guid id);
        IEnumerable<HostV2> ListConnectedHostsForSite(int storeId);

        void AddRange(int storeId, IEnumerable<Guid> selectServerListIds);
        void ClearAll(int storeId);
    }

    public interface IHostV2ForApplicationDataService
    {
        IEnumerable<ACSApplication> ListByHostId(Guid id);
        IEnumerable<HostV2> ListConnectedHostsForApplication(int applicationId);
        void AddRange(int applicationId, IEnumerable<Guid> selectServerListIds);
        void ClearAll(int applicationId);
    }
}