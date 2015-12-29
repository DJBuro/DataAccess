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
            Table("AMSServerFTPServerPair");
            Id(x => x.Id);
            References(x => x.Store).ForeignKey("FK_StoreAMSServerFTPServerPair_Store").Column("StoreId").Not.LazyLoad();
            References(x => x.AMSServer).ForeignKey("FK_StoreAMSServerFTPServerPair_AMSServer").Column("AMSServerId").Not.LazyLoad();
            References(x => x.PrimaryFTPSite).ForeignKey("FK_StoreAMSServerFTPServerPair_FTPSite").Column("PrimaryFTPSiteId").Not.LazyLoad();
            References(x => x.SecondaryFTPSite).ForeignKey("FK_StoreAMSServerFTPServerPair_FTPSite1").Column("SecondaryFTPSiteId").Not.LazyLoad();
        }
    }
}
