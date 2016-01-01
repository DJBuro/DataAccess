using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class FTPSiteMap : ClassMap<FTPSite>
    {
        public FTPSiteMap()
        {
            Table("FTPSite");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Url);
            Map(x => x.Port);
            Map(x => x.ServerType);
            Map(x => x.Username);
            Map(x => x.Password);
            Map(x => x.IsPrimary);
        }
    }
}