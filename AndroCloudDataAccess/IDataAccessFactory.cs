using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.DataAccess;

namespace AndroCloudDataAccess
{
    public interface IDataAccessFactory
    {
        ISiteDataAccess SiteDataAccess { get; set; }
        IACSQueueDataAccess ACSQueueDataAccess { get; set; }
        IAuditDataAccess AuditDataAccess { get; set; }
        IPartnersDataAccess PartnerDataAccess { get; set; }
        IChainsDataAccess ChainDataAccess { get; set; }
        ISiteMenuDataAccess SiteMenuDataAccess { get; set; }
    }
}
