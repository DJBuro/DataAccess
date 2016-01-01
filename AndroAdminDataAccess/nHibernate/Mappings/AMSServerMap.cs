using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.nHibernate.Mappings
{
    public class AMSServerMap : ClassMap<AMSServer>
    {
        public AMSServerMap()
        {
            Table("AMSServer");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Description);

        }
    }
}
