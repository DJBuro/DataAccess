using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class StoreAMSServerMap : ClassMap<StoreAMSServer>
    {
        public StoreAMSServerMap()
        {
            Table("StoreAMSServer");
            Id(x => x.Id);
            References<AMSServer>(x => x.AMSServer).Not.LazyLoad();
            References<Store>(x => x.Store).Not.LazyLoad();
        }
    }
}