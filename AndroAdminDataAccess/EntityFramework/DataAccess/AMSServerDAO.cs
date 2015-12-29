using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class AMSServerDAO : IAMSServerDAO
    {

        public IList<Domain.AMSServer> GetAll()
        {
            List<Domain.AMSServer> amsServers = new List<Domain.AMSServer>();

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.AMSServers
                        select s;

            foreach (var entity in query)
            {
                Domain.AMSServer model = new Domain.AMSServer()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                };

                amsServers.Add(model);
            }

            return amsServers;
        }

        public void Add(Domain.AMSServer amsServer)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            AMSServer entity = new AMSServer()
            {
                Name = amsServer.Name,
                Description = amsServer.Description
            };

            androAdminEntities.AddToAMSServers(entity);
            androAdminEntities.SaveChanges();
        }

        public void Update(Domain.AMSServer amsServer)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.AMSServers
                        where amsServer.Id == s.Id
                        select s;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                entity.Name = amsServer.Name;
                entity.Description = amsServer.Description;

                androAdminEntities.SaveChanges();
            }
        }

        public Domain.AMSServer GetById(int id)
        {
            Domain.AMSServer model = null;

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.AMSServers
                        where id == s.Id
                        select s;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model = new Domain.AMSServer()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                };
            }

            return model;
        }

        public Domain.AMSServer GetByName(string name)
        {
            Domain.AMSServer model = null;

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.AMSServers
                        where name == s.Name
                        select s;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                model = new Domain.AMSServer()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description
                };
            }

            return model;
        }


        public void Delete(int amsServerId)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.AMSServers
                        where amsServerId == s.Id
                        select s;

            var entity = query.FirstOrDefault();

            if (entity != null)
            {
                androAdminEntities.AMSServers.DeleteObject(entity);

                androAdminEntities.SaveChanges();
            }
        }
    }
}
