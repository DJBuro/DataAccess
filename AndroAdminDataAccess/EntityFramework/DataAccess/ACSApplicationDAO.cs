using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class ACSApplicationDAO : IACSApplicationDAO
    {
        public IList<Domain.ACSApplication> GetByPartnerId(int partnerId)
        {
            List<Domain.ACSApplication> models = new List<Domain.ACSApplication>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.ACSApplications
                            where partnerId == s.PartnerId
                            select s;

                foreach (ACSApplication acsApplication in query)
                {
                    Domain.ACSApplication model = new Domain.ACSApplication()
                    {
                        Id = acsApplication.Id,
                        Name = acsApplication.Name,
                        ExternalApplicationId = acsApplication.ExternalApplicationId,
                        DataVersion = acsApplication.DataVersion
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public Domain.ACSApplication GetById(int acsApplicationId)
        {
            Domain.ACSApplication model = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.ACSApplications
                            where acsApplicationId == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.ACSApplication()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ExternalApplicationId = entity.ExternalApplicationId,
                        DataVersion = entity.DataVersion,
                        PartnerId = entity.PartnerId
                    };
                }
            }

            return model;
        }


        public Domain.ACSApplication GetByName(string name)
        {
            Domain.ACSApplication acsApplication = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.ACSApplications
                            where name == s.Name
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    acsApplication = new Domain.ACSApplication()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ExternalApplicationId = entity.ExternalApplicationId,
                        DataVersion = entity.DataVersion
                    };
                }
            }

            return acsApplication;
        }

        public Domain.ACSApplication GetByExternalId(string externalId)
        {
            Domain.ACSApplication acsApplication = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.ACSApplications
                            where externalId == s.ExternalApplicationId
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    acsApplication = new Domain.ACSApplication()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ExternalApplicationId = entity.ExternalApplicationId,
                        DataVersion = entity.DataVersion
                    };
                }
            }

            return acsApplication;
        }

        public void Add(Domain.ACSApplication acsApplication)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                ACSApplication entity = new ACSApplication()
                {
                    Name = acsApplication.Name,
                    ExternalApplicationId = acsApplication.ExternalApplicationId,
                    DataVersion = 0,
                    PartnerId = acsApplication.PartnerId
                };

                entitiesContext.AddToACSApplications(entity);
                entitiesContext.SaveChanges();
            }
        }

        public void Update(Domain.ACSApplication acsApplication)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.ACSApplications
                            where acsApplication.Id == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entity.Name = acsApplication.Name;
                    entity.ExternalApplicationId = acsApplication.ExternalApplicationId;

                    entitiesContext.SaveChanges();
                }
            }
        }
    }
}
