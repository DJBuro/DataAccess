using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

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
                entitiesContext.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Connection.BeginTransaction())
                {
                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

                    // Add the new application
                    ACSApplication entity = new ACSApplication()
                    {
                        Name = acsApplication.Name,
                        ExternalApplicationId = acsApplication.ExternalApplicationId,
                        DataVersion = newVersion,
                        PartnerId = acsApplication.PartnerId
                    };
                    entitiesContext.AddToACSApplications(entity);
                    entitiesContext.SaveChanges();

                    // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                    IPartnerDAO partnerDAO = new PartnerDAO();

                    // Get the partner so we can update it
                    partnerDAO.UpdateDataVersion(acsApplication.PartnerId, newVersion);

                    // Fin...
                    transaction.Commit();
                }
            }
        }

        public void Update(Domain.ACSApplication acsApplication)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Connection.BeginTransaction())
                {
                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

                    var query = from s in entitiesContext.ACSApplications
                                where acsApplication.Id == s.Id
                                select s;

                    var entity = query.FirstOrDefault();

                    if (entity != null)
                    {
                        // Update the new application
                        entity.Name = acsApplication.Name;
                        entity.ExternalApplicationId = acsApplication.ExternalApplicationId;
                        entity.DataVersion = newVersion;
                        entitiesContext.SaveChanges();

                        // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                        IPartnerDAO partnerDAO = new PartnerDAO();

                        // Get the partner so we can update it
                        partnerDAO.UpdateDataVersion(acsApplication.PartnerId, newVersion);

                        // Fin...
                        transaction.Commit();
                    }
                }
            }
        }

        public void AddStore(int storeId, int acsApplicationId)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Connection.BeginTransaction())
                {
                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

                    // We don't delete application stores - we mark them as deleted.
                    // When adding an application store we could be effectively "undeleting" a store previously marked as deleted

                    // Get the existing application store so we can delete it
                    var query2 = from s in entitiesContext.ACSApplicationSites
                            where storeId == s.SiteId
                            && acsApplicationId == s.ACSApplicationId
                            select s;
                    var entity2 = query2.FirstOrDefault();

                    // Does the application store already exist?
                    if (entity2 == null)
                    {
                        // No existing application store - add one
                        ACSApplicationSite acsApplicationSite = new ACSApplicationSite();
                        acsApplicationSite.SiteId = storeId;
                        acsApplicationSite.ACSApplicationId = acsApplicationId;
                        acsApplicationSite.DataVersion = newVersion;

                        entitiesContext.AddToACSApplicationSites(acsApplicationSite);
                        entitiesContext.SaveChanges();

                        // Update the application version to signify that the application has changed (a child of the application has changed)
                        IACSApplicationDAO acsApplicationDAO = new ACSApplicationDAO();

                        // Update the application version
                        acsApplicationDAO.UpdateDataVersion(acsApplicationId, newVersion);

                        // Get the application (we need the partner id)
                        Domain.ACSApplication acsApplication = acsApplicationDAO.GetById(acsApplicationId);

                        // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                        IPartnerDAO partnerDAO = new PartnerDAO();

                        // Get the partner so we can update it
                        partnerDAO.UpdateDataVersion(acsApplication.PartnerId, newVersion);
                    }
                    else
                    {
                        // Un-delete the existing application store
                        entity2.DataVersion = newVersion;
                        entitiesContext.SaveChanges();
                    }

                    // Fin...
                    transaction.Commit();
                }
            }
        }

        public void RemoveStore(int storeId, int acsApplicationId)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                entitiesContext.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Connection.BeginTransaction())
                {
                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext, transaction);

                    // Get the existing application store so we can delete it
                    var query = from s in entitiesContext.ACSApplicationSites
                                where storeId == s.SiteId
                                && acsApplicationId == s.ACSApplicationId
                                select s;

                    var entity = query.FirstOrDefault();

                    if (entity != null)
                    {
                        // Delete the application store (not really, just mark it as deleted)
                        entitiesContext.DeleteObject(entity);
                        entitiesContext.SaveChanges();

                        // Update the application version to signify that the application has changed (a child of the application has changed)
                        IACSApplicationDAO acsApplicationDAO = new ACSApplicationDAO();

                        // Update the application version
                        acsApplicationDAO.UpdateDataVersion(acsApplicationId, newVersion);

                        // Get the application (we need the partner id)
                        Domain.ACSApplication acsApplication = acsApplicationDAO.GetById(acsApplicationId);

                        // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                        IPartnerDAO partnerDAO = new PartnerDAO();

                        // Get the partner so we can update it
                        partnerDAO.UpdateDataVersion(acsApplication.PartnerId, newVersion);

                        // Fin...
                        transaction.Commit();
                    }
                }
            }
        }

        public IList<Domain.ACSApplication> GetByPartnerAfterDataVersion(int partnerId, int dataVersion)
        {
            List<Domain.ACSApplication> models = new List<Domain.ACSApplication>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.ACSApplications
                            where partnerId == s.PartnerId
                            && s.DataVersion > dataVersion
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

        public void UpdateDataVersion(int acsApplicationId, int newVersion)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.ACSApplications
                            where acsApplicationId == s.Id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    // Update the new application
                    entity.DataVersion = newVersion;
                    entitiesContext.SaveChanges();
                }
            }
        }
    }
}
