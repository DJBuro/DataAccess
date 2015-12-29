using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Table("Store");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.AndromedaSiteId);
            Map(x => x.CustomerSiteId);
            Map(x => x.LastFTPUploadDateTime);
            HasMany(x => x.AMSServerFTPServerPairs).KeyColumn("StoreId").Inverse().Cascade.All().Not.LazyLoad(); 
        }
    }
}