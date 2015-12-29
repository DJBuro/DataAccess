using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHibernate;
using AndroUsersDataAccess.DataAccess;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.nHibernate.DataAccess
{
    public class AndroUserDAO : IAndroUserDAO
    {
        public AndroUser GetByEmailAddress(string emailAddress)
        {
            IList<AndroUser> androUsers = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                androUsers = session.CreateQuery("from " + typeof(AndroUser) + " where EmailAddress=:emailAddress and Active=true")
                    .SetParameter("emailAddress", emailAddress)
                    .List<AndroUser>();
            }

            AndroUser androUser = null;

            if (androUsers.Count == 1)
            {
                androUser = androUsers[0];
            }

            return androUser;
        }
    }
}
