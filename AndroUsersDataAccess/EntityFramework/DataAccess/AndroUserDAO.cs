using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.DataAccess;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.EntityFramework.DataAccess
{
    public class AndroUserDAO : IAndroUserDAO
    {
        public AndroUser GetByEmailAddress(string emailAddress)
        {
            Domain.AndroUser model = null;

            AndroUsersEntities1 androUsersEntities = new AndroUsersEntities1();

            var query = from s in androUsersEntities.tbl_AndroUser
                        where emailAddress == s.EmailAddress
                        && s.Active == true
                        select s;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model = new Domain.AndroUser()
                {
                    Id = entity.id,
                    Active = entity.Active,
                    Created = entity.Created,
                    EmailAddress = entity.EmailAddress,
                    FirstName = entity.FirstName,
                    Password = entity.Password,
                    SurName = entity.SurName
                };
            }

            return model;
        }
    }
}
