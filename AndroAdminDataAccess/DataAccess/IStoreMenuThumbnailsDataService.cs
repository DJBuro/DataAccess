using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IStoreMenuThumbnailsDataService 
    {
        IEnumerable<Domain.StoreMenuThumbnails> GetAfterDataVersion(int fromVersion);
    }
}
