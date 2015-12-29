using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class AMSServerFTPServerPairMap : ClassMap<AMSServerFTPServerPair>
    {
        public AMSServerFTPServerPairMap()
        {
            // Note there is no mapping for x.AMSServerId, x.PrimaryFTPSiteId or x.SecondaryFTPSiteId as you can't map the same column
            // to multiple properties (at least not as far as I am aware).  These properties are needed to get round a problem 
            // data mapping in MVC3 and they are populated manually in code.  Fudgetastic...
            Table("StoreAMSServerFTPSitePair");
            Id(x => x.Id);
            Map(x => x.StoreId);
            Map(x => x.Priority);
            References(x => x.AMSServer, "AMSServerId").Cascade.None().Not.LazyLoad();
            References(x => x.PrimaryFTPSite, "PrimaryFTPSiteId").Cascade.None().Not.LazyLoad();
            References(x => x.SecondaryFTPSite, "SecondaryFTPSiteId").Cascade.None().Not.LazyLoad();
        }
    }
}
