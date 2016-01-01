using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.nHibernate.Mappings
{
    public class AndroUserMap : ClassMap<AndroUser>
    {
        public AndroUserMap()
        {
            Table("tbl_AndroUser");
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.SurName);
            Map(x => x.Password);
            Map(x => x.EmailAddress);
            Map(x => x.Active);
            Map(x => x.Created);

        }
    }
}
