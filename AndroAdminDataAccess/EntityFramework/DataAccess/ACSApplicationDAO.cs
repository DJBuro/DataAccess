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
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.ACSApplication> GetByPartnerId(int partnerId)
        {
            List<Domain.ACSApplication> models = new List<Domain.ACSApplication>();

 //           using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

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
                        DataVersion = acsApplication.DataVersion,
                        PartnerId = acsApplication.PartnerId
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public Domain.ACSApplication GetById(int acsApplicationId)
        {
            Domain.ACSApplication model = null;

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

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

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

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
                        DataVersion = entity.DataVersion,
                        PartnerId = entity.PartnerId
                    };
                }
            }

            return acsApplication;
        }

        public Domain.ACSApplication GetByExternalId(string externalId)
        {
            Domain.ACSApplication acsApplication = null;

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

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
                        DataVersion = entity.DataVersion,
                        PartnerId = entity.PartnerId
                    };
                }
            }

            return acsApplication;
        }

        public void Add(Domain.ACSApplication acsApplication)
        {
            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                //entitiesContext.Connection.Open();
                entitiesContext.Database.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Database.Connection.BeginTransaction())
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
                    entitiesContext.ACSApplications.Add(entity);
                    entitiesContext.SaveChanges();

                    // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                    var partnerQuery = from s in entitiesContext.Partners
                                where acsApplication.PartnerId == s.Id
                                select s;

                    var partnerEntity = partnerQuery.FirstOrDefault();

                    if (partnerEntity != null)
                    {
                        partnerEntity.DataVersion = newVersion;

                        entitiesContext.SaveChanges();
                    }

                    // Fin...
                    transaction.Commit();
                }
            }
        }

        public void Update(Domain.ACSApplication acsApplication)
        {
            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                //entitiesContext.Connection.Open();
                entitiesContext.Database.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Database.Connection.BeginTransaction())
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
                        var partnerQuery = from s in entitiesContext.Partners
                                    where acsApplication.PartnerId == s.Id
                                    select s;

                        var partnerEntity = partnerQuery.FirstOrDefault();

                        if (partnerEntity != null)
                        {
                            partnerEntity.DataVersion = newVersion;

                            entitiesContext.SaveChanges();
                        }

                        // Fin...
                        transaction.Commit();
                    }
                }
            }
        }

        public void AddStore(int storeId, int acsApplicationId)
        {
            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                //entitiesContext.Connection.Open();
                entitiesContext.Database.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Database.Connection.BeginTransaction())
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

                        entitiesContext.ACSApplicationSites.Add(acsApplicationSite);
                        entitiesContext.SaveChanges();

                        // Update the application version to signify that the application has changed (a child of the application has changed)
                        IACSApplicationDAO acsApplicationDAO = new ACSApplicationDAO();
                        acsApplicationDAO.ConnectionStringOverride = this.ConnectionStringOverride;

                        // Update the application version
                        var acsApplicationsQuery = from s in entitiesContext.ACSApplications
                                                   where acsApplicationId == s.Id
                                                   select s;

                        var acsApplicationsEntity = acsApplicationsQuery.FirstOrDefault();

                        if (acsApplicationsEntity != null)
                        {
                            acsApplicationsEntity.DataVersion = newVersion;
                            entitiesContext.SaveChanges();
                        }

                        // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                        var partnerQuery = from s in entitiesContext.Partners
                                           where acsApplicationsEntity.PartnerId == s.Id
                                           select s;

                        var partnerEntity = partnerQuery.FirstOrDefault();

                        if (partnerEntity != null)
                        {
                            partnerEntity.DataVersion = newVersion;
                            entitiesContext.SaveChanges();
                        }
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
            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                //entitiesContext.Connection.Open();
                entitiesContext.Database.Connection.Open();
                using (DbTransaction transaction = entitiesContext.Database.Connection.BeginTransaction())
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
                        entitiesContext.ACSApplicationSites.Remove(entity);
                        entitiesContext.SaveChanges();

                        // Update the application version to signify that the application has changed (a child of the application has changed)
                        IACSApplicationDAO acsApplicationDAO = new ACSApplicationDAO();
                        acsApplicationDAO.ConnectionStringOverride = this.ConnectionStringOverride;

                        // Update the application version
                        var acsApplicationsQuery = from s in entitiesContext.ACSApplications
                                    where acsApplicationId == s.Id
                                    select s;

                        var acsApplicationsEntity = acsApplicationsQuery.FirstOrDefault();

                        if (acsApplicationsEntity != null)
                        {
                            acsApplicationsEntity.DataVersion = newVersion;
                            entitiesContext.SaveChanges();
                        }

                        // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                        var partnerQuery = from s in entitiesContext.Partners
                                           where acsApplicationsEntity.PartnerId == s.Id
                                           select s;

                        var partnerEntity = partnerQuery.FirstOrDefault();

                        if (partnerEntity != null)
                        {
                            partnerEntity.DataVersion = newVersion;
                            entitiesContext.SaveChanges();
                        }

                        // Fin...
                        transaction.Commit();
                    }
                }
            }
        }

        public IList<Domain.ACSApplication> GetByPartnerAfterDataVersion(int partnerId, int dataVersion)
        {
            List<Domain.ACSApplication> models = new List<Domain.ACSApplication>();

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

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
                        DataVersion = acsApplication.DataVersion,
                        PartnerId = acsApplication.PartnerId
                    };

                    models.Add(model);
                }
            }

            return models;
        }
    }
}
