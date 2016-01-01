using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class FTPSiteTypeMap : ClassMap<FTPSiteType>
    {
        public FTPSiteTypeMap()
        {
            Table("FTPSiteType");
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
}