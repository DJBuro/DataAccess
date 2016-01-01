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
        public string ConnectionStringOverride { get; set; }

        public List<AndroUser> GetAll()
        {
            List<AndroUser> androUsers = new List<AndroUser>();

            using (AndroUsersEntities entitiesContext = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.tbl_AndroUser
                            select s;

                foreach (var entity in query)
                {
                    Domain.AndroUser model = new Domain.AndroUser()
                    {
                        Id = entity.id,
                        Active = entity.Active,
                        Created = entity.Created,
                        EmailAddress = entity.EmailAddress,
                        FirstName = entity.FirstName,
                        Password = entity.Password,
                        SurName = entity.SurName
                    };

                    androUsers.Add(model);
                }
            }

            return androUsers;
        }

        public AndroUser GetByEmailAddress(string emailAddress)
        {
            Domain.AndroUser model = null;

            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

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

        public string Add(AndroUser androUser)
        {
            using (AndroUsersEntities androUsersEntities = new AndroUsersEntities())
            {
                DataAccessHelper.FixConnectionString(androUsersEntities, this.ConnectionStringOverride);

                tbl_AndroUser androUserEntity = new tbl_AndroUser()
                {
                    Active = androUser.Active,
                    Created = androUser.Created,
                    EmailAddress = androUser.EmailAddress,
                    FirstName = androUser.FirstName,
                    Password = androUser.Password,
                    SurName = androUser.SurName
                };

                androUsersEntities.tbl_AndroUser.Add(androUserEntity);
                androUsersEntities.SaveChanges();

                return "";
            }
        }
    }
}
