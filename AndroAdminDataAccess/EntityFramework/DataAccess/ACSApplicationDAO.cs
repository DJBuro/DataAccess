using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;
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
                        ExternalApplicationName = acsApplication.ExternalDisplayName,
                        DataVersion = acsApplication.DataVersion,
                        PartnerId = acsApplication.PartnerId,
                        EnvironmentId = acsApplication.Environment.Id,
                        EnvironmentName = acsApplication.Environment.Name

                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public IList<Domain.ACSApplication> GetAll()
        {
            List<Domain.ACSApplication> models = new List<Domain.ACSApplication>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.ACSApplications 
                            select s;

                foreach (ACSApplication acsApplication in query)
                {
                    Domain.ACSApplication model = new Domain.ACSApplication()
                    {
                        Id = acsApplication.Id,
                        Name = acsApplication.Name,
                        ExternalApplicationId = acsApplication.ExternalApplicationId,
                        ExternalApplicationName = acsApplication.ExternalDisplayName,
                        DataVersion = acsApplication.DataVersion,
                        PartnerId = acsApplication.PartnerId,
                        EnvironmentId = acsApplication.Environment.Id,
                        EnvironmentName = acsApplication.Environment.Name
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
                        ExternalApplicationName = entity.ExternalDisplayName,
                        DataVersion = entity.DataVersion,
                        PartnerId = entity.PartnerId,
                        EnvironmentId = entity.Environment.Id,
                        EnvironmentName = entity.Environment.Name
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
                        ExternalApplicationName = entity.ExternalDisplayName,
                        DataVersion = entity.DataVersion,
                        PartnerId = entity.PartnerId,
                        EnvironmentId = entity.Environment.Id,
                        EnvironmentName = entity.Environment.Name
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
                        ExternalApplicationName = entity.ExternalDisplayName,
                        DataVersion = entity.DataVersion,
                        PartnerId = entity.PartnerId,
                        EnvironmentId = entity.Environment.Id,
                        EnvironmentName = entity.Environment.Name
                    };
                }
            }

            return acsApplication;
        }

        public IList<Domain.ACSApplication> GetByStoreId(int storeId)
        {
            IList<Domain.ACSApplication> results = new List<Domain.ACSApplication>();
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = entitiesContext.ACSApplications.Where(e => e.ACSApplicationSites.Any(siteLink => siteLink.SiteId == storeId));
                var result = query.ToArray();

                results = result.Select(entity => new Domain.ACSApplication()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ExternalApplicationId = entity.ExternalApplicationId,
                    ExternalApplicationName = entity.ExternalDisplayName,
                    DataVersion = entity.DataVersion,
                    PartnerId = entity.PartnerId,
                    EnvironmentId = entity.Environment.Id,
                    EnvironmentName = entity.Environment.Name
                }).ToList();
            }

            return results;
        }

        public void Add(Domain.ACSApplication acsApplication)
        {
            // We will use transactionscope to implicitly enrole both EF and direct SQL in the same transaction
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    entitiesContext.Database.Connection.Open();
                    
                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                    // Add the new application
                    ACSApplication entity = new ACSApplication()
                    {
                        Name = acsApplication.Name,
                        ExternalApplicationId = acsApplication.ExternalApplicationId,
                        ExternalDisplayName = acsApplication.ExternalApplicationName,
                        DataVersion = newVersion,
                        PartnerId = acsApplication.PartnerId,
                        EnvironmentId = acsApplication.EnvironmentId
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

                    // Commit the transaction
                    transactionScope.Complete();
                }
            }
        }

        public void Update(Domain.ACSApplication acsApplication)
        {
            // We will use transactionscope to implicitly enrole both EF and direct SQL in the same transaction
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    entitiesContext.Database.Connection.Open();

                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                    var query = from s in entitiesContext.ACSApplications
                                where acsApplication.Id == s.Id
                                select s;

                    var entity = query.FirstOrDefault();

                    if (entity != null)
                    {
                        // Update the new application
                        entity.Name = acsApplication.Name;
                        entity.ExternalApplicationId = acsApplication.ExternalApplicationId;
                        entity.ExternalDisplayName = acsApplication.ExternalApplicationName;
                        entity.EnvironmentId = acsApplication.EnvironmentId;
                        entity.DataVersion = newVersion;
                        entitiesContext.SaveChanges();

                        // Update the partner version to signify that the partner has changed (a child of the partner has changed)
                        var partnerQuery = from s in entitiesContext.Partners
                                           where entity.PartnerId == s.Id
                                           select s;

                        var partnerEntity = partnerQuery.FirstOrDefault();

                        if (partnerEntity != null)
                        {
                            partnerEntity.DataVersion = newVersion;

                            entitiesContext.SaveChanges();
                        }

                        // Commit the transacton
                        transactionScope.Complete();
                    }
                }
            }
        }

        public void AddStore(int storeId, int acsApplicationId)
        {
            // We will use transactionscope to implicitly enrole both EF and direct SQL in the same transaction
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    entitiesContext.Database.Connection.Open();

                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

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

                    // Commit the transacton
                    transactionScope.Complete();
                }
            }
        }

        public void RemoveStore(int storeId, int acsApplicationId)
        {
            // We will use transactionscope to implicitly enrole both EF and direct SQL in the same transaction
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                    entitiesContext.Database.Connection.Open();

                    // Get the next data version (see comments inside the function)
                    int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

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

                        // Commit the transacton
                        transactionScope.Complete();
                    }
                }
            }
        }

        public IList<Domain.ACSApplication> GetByPartnerAfterDataVersion(int partnerId, int dataVersion)
        {
            List<Domain.ACSApplication> models = new List<Domain.ACSApplication>();

             
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
                        ExternalApplicationName = acsApplication.ExternalDisplayName,
                        DataVersion = acsApplication.DataVersion,
                        PartnerId = acsApplication.PartnerId
                    };

                    models.Add(model);
                }
            }

            return models;
        }

        public IList<Domain.ACSApplication> GetDataBetweenVersions(int fromDataVersion, int toDataVersion)
        {
            List<Domain.ACSApplication> models = new List<Domain.ACSApplication>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.ACSApplications
                            where s.DataVersion > fromDataVersion
                            && s.DataVersion <= toDataVersion 
                            select s;

                foreach (ACSApplication acsApplication in query)
                {
                    Domain.ACSApplication model = new Domain.ACSApplication()
                    {
                        Id = acsApplication.Id,
                        Name = acsApplication.Name,
                        ExternalApplicationId = acsApplication.ExternalApplicationId,
                        ExternalApplicationName = acsApplication.ExternalDisplayName,
                        DataVersion = acsApplication.DataVersion,
                        PartnerId = acsApplication.PartnerId,
                        EnvironmentId = acsApplication.EnvironmentId.GetValueOrDefault()
                    };

                    models.Add(model);
                }
            }

            return models;
        }


        public IList<int> GetSites(int acsApplicationId)
        {
            List<int> models = new List<int>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from sites in entitiesContext.ACSApplicationSites
                            where sites.ACSApplicationId == acsApplicationId
                            select sites;

                foreach (ACSApplicationSite acsApplicationSite in query)
                {
                    models.Add(acsApplicationSite.SiteId);
                }
            }

            return models;
        }
    }
}
