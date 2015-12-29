using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class PartnerDAO : IPartnerDAO
    {
        public IEnumerable<Domain.Partner> GetAll()
        {
            List<Domain.Partner> models = new List<Domain.Partner>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from p in entitiesContext.Partners
                            select p;

                foreach (var entity in query)
                {
                    Domain.Partner model = new Domain.Partner()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        DataVersion = entity.DataVersion,
                        ExternalId = entity.ExternalId
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public Domain.Partner GetById(int partnerId)
        {
            Domain.Partner partner = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Partners
                            where partnerId == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    partner = new Domain.Partner()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ExternalId = entity.ExternalId,
                        DataVersion = entity.DataVersion
                    };
                }
            }

            return partner;
        }

        public Domain.Partner GetByName(string name)
        {
            Domain.Partner partner = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Partners
                            where name == s.Name
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    partner = new Domain.Partner()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ExternalId = entity.ExternalId,
                        DataVersion = entity.DataVersion
                    };
                }
            }

            return partner;
        }

        public Domain.Partner GetByExternalId(string externalId)
        {
            Domain.Partner partner = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Partners
                            where externalId == s.ExternalId
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    partner = new Domain.Partner()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ExternalId = entity.ExternalId,
                        DataVersion = entity.DataVersion
                    };
                }
            }

            return partner;
        }

        public void Add(Domain.Partner partner)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                Partner entity = new Partner()
                {
                    Name = partner.Name,
                    ExternalId = partner.ExternalId,
                    DataVersion = 0
                };

                entitiesContext.AddToPartners(entity);
                entitiesContext.SaveChanges();
            }
        }

        public void Update(Domain.Partner partner)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Partners
                            where partner.Id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = partner.Name;
                    entity.ExternalId = partner.ExternalId;

                    entitiesContext.SaveChanges();
                }
            }
        }
    }
}