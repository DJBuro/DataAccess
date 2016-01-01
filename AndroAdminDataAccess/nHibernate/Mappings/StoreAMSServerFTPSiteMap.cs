using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class StoreAMSServerFTPSiteMap : ClassMap<StoreAMSServerFtpSite>
    {
        public StoreAMSServerFTPSiteMap()
        {
            Table("StoreAMSServerFtpSite");
            Id(x => x.Id);
            References<FTPSite>(x => x.FTPSite).Not.LazyLoad();
            References<StoreAMSServer>(x => x.StoreAMSServer).Not.LazyLoad();
        }
    }
}