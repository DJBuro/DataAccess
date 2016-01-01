using AndroAdminDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AndroAdminDataAccess.Domain;
using System.Transactions;
using System.Data.Entity.Validation;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class AndroWebOrderingWebsiteDAO : IAndroWebOrderingWebsiteDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.AndroWebOrderingWebsite> GetAll()
        {
            List<Domain.AndroWebOrderingWebsite> models = new List<Domain.AndroWebOrderingWebsite>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.AndroWebOrderingWebsites.Include(c => c.ACSApplication).Include(c => c.ACSApplication.ACSApplicationSites).Include(c => c.ACSApplication.ACSApplicationSites).Include(c => c.Chain).Include(c => c.Chain.Stores).Include(c => c.AndroWebOrderingSubscriptionType)
                            select s;

                foreach (var entity in query)
                {
                    Domain.AndroWebOrderingWebsite model = new Domain.AndroWebOrderingWebsite()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        ACSApplicationId = entity.ACSApplicationId,
                        ChainId = entity.ChainId,
                        DataVersion = entity.DataVersion,
                        DisabledReason = entity.DisabledReason,
                        Enabled = entity.Enabled,
                        Status = (entity.Enabled ? "Enabled" : "Disabled"),
                        SubscriptionTypeId = entity.SubscriptionTypeId,
                        SubscriptionName = (entity.AndroWebOrderingSubscriptionType != null) ? entity.AndroWebOrderingSubscriptionType.Subscription : string.Empty,
                        URL = entity.URL
                    };
                    model.MappedSiteIds = new List<int>();
                    if (entity.ACSApplication != null)
                    {
                        if (entity.ACSApplication.ACSApplicationSites != null)
                        {
                            foreach (var store in entity.ACSApplication.ACSApplicationSites)
                            {
                                model.MappedSiteIds.Add(store.SiteId);
                            }
                        }
                    }

                    models.Add(model);
                }
            }

            return models;
        }

        public Domain.AndroWebOrderingWebsite GetAndroWebOrderingWebsiteById(int id)
        {
            Domain.AndroWebOrderingWebsite webOrderingSite = new Domain.AndroWebOrderingWebsite();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                if (id > 0)
                {
                    var result = entitiesContext.AndroWebOrderingWebsites.Include(c => c.ACSApplication).Include(c => c.ACSApplication.ACSApplicationSites).Include(c => c.Chain).Include(c => c.Chain.Stores).Where(c => c.Id == id).FirstOrDefault();

                    if (result != null)
                    {
                        webOrderingSite.Id = result.Id;
                        webOrderingSite.Name = result.Name;
                        webOrderingSite.SubscriptionTypeId = result.SubscriptionTypeId;
                        webOrderingSite.URL = result.URL;
                        webOrderingSite.Enabled = result.Enabled;
                        webOrderingSite.DisabledReason = result.DisabledReason;
                        webOrderingSite.DataVersion = result.DataVersion;
                        webOrderingSite.ChainId = result.ChainId;
                        webOrderingSite.ACSApplicationId = result.ACSApplicationId;
                        webOrderingSite.Chains = entitiesContext.Chains.Select(s => new Domain.Chain { Id = s.Id, Name = s.Name }).ToList();
                        webOrderingSite.MappedSiteIds = entitiesContext.ACSApplicationSites.Where(a => a.ACSApplicationId == result.ACSApplicationId).Select(s => s.SiteId).ToList();
                    }

                }
                else
                {
                    webOrderingSite.Chains = new List<Domain.Chain>();
                    var chainsList = entitiesContext.Chains.Include(s => s.Store).ToList();
                    foreach (var chain in chainsList)
                    {
                        Domain.Chain chainObj = new Domain.Chain();
                        chainObj.Id = chain.Id;
                        chainObj.Name = chain.Name;
                        chainObj.Stores = new List<Domain.Store>();
                        if (chain.Stores != null)
                        {
                            foreach (var store in chain.Stores)
                            {
                                chainObj.Stores.Add(new Domain.Store { Id = store.Id, AndromedaSiteId = store.AndromedaSiteId, Name = store.Name });
                            }
                        }
                        webOrderingSite.Chains.Add(chainObj);
                    }
                }
                webOrderingSite.AllStores = entitiesContext.Stores.Include(s => s.StoreStatu).Select(s => new Domain.Store { Id = s.Id, Name = s.Name, AndromedaSiteId = s.AndromedaSiteId, ChainId = s.ChainId, StoreStatus = new Domain.StoreStatus { Id = s.StoreStatu.Id, Status = s.StoreStatu.Status } }).ToList();
                webOrderingSite.SubscriptionsList = entitiesContext.AndroWebOrderingSubscriptionTypes.Select(s => new Domain.AndroWebOrderingSubscriptionType { Id = s.Id, Subscription = s.Subscription, DisplayOrder = s.DisplayOrder }).OrderBy(o => o.DisplayOrder).ToList();
            }


            return webOrderingSite;
        }

        public int Add(Domain.AndroWebOrderingWebsite webOrderingSite)
        {
            int success = 0;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    ACSApplicationDAO acsDAO = new ACSApplicationDAO();
                    Partner partner = entitiesContext.Partners.Where(c => c.Name.Equals("andromeda", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                    if (partner != null)
                    {
                        if (entitiesContext.ACSApplications.Where(a => a.Name.Equals(webOrderingSite.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault() == null
                        && entitiesContext.AndroWebOrderingWebsites.Where(a => a.Name.Equals(webOrderingSite.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault() == null)
                        {
                            webOrderingSite.ACSApplication.PartnerId = partner.Id;

                            acsDAO.Add(webOrderingSite.ACSApplication);

                            DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                            entitiesContext.Database.Connection.Open();

                            var dataVersionResult = entitiesContext.Settings.Where(s => s.Name.Equals("DataVersion", StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                            int newVersion = dataVersionResult != null ? Convert.ToInt32(dataVersionResult.Value) : 0;

                            var acsApplication = entitiesContext.ACSApplications.Where(a => a.Name.Equals(webOrderingSite.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                            if (acsApplication != null)
                            {
                                entitiesContext.AndroWebOrderingWebsites.Add(new AndroWebOrderingWebsite
                                {
                                    ChainId = webOrderingSite.ChainId,
                                    ACSApplicationId = acsApplication.Id,
                                    DataVersion = newVersion,
                                    DisabledReason = webOrderingSite.DisabledReason,
                                    Enabled = webOrderingSite.Enabled,
                                    Name = webOrderingSite.Name,
                                    SubscriptionTypeId = webOrderingSite.SubscriptionTypeId,
                                    URL = webOrderingSite.URL
                                });

                                if (webOrderingSite.MappedSiteIds != null)
                                {
                                    foreach (var siteId in webOrderingSite.MappedSiteIds)
                                    {
                                        entitiesContext.ACSApplicationSites.Add(new ACSApplicationSite { SiteId = siteId, ACSApplicationId = acsApplication.Id, DataVersion = newVersion });
                                    }
                                }
                            }
                            entitiesContext.SaveChanges();
                            transactionScope.Complete();
                            success = 1;
                        }
                    }
                }
            }
            return success;
        }

        public int Update(Domain.AndroWebOrderingWebsite webOrderingSite)
        {
            int success = 0;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
                {
                    var webSite = entitiesContext.AndroWebOrderingWebsites.Include(w => w.ACSApplication).Include(e => e.ACSApplication.ACSApplicationSites).Where(w => w.Id == webOrderingSite.Id).FirstOrDefault();

                    if (webSite != null)
                    {
                        ACSApplication dupACSApp = entitiesContext.ACSApplications.Where(a => a.Id != webSite.ACSApplicationId && a.Name.Equals(webOrderingSite.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                        AndroWebOrderingWebsite dupWebsite = entitiesContext.AndroWebOrderingWebsites.Where(a => a.Id != webSite.Id && a.Name.Equals(webOrderingSite.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                        if (dupACSApp == null && dupWebsite == null)
                        {
                            int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                            webSite.ChainId = webOrderingSite.ChainId;
                            webSite.DataVersion = newVersion;
                            webSite.DisabledReason = webOrderingSite.DisabledReason;
                            webSite.Enabled = webOrderingSite.Enabled;
                            webSite.Name = webOrderingSite.Name;
                            webSite.SubscriptionTypeId = webOrderingSite.SubscriptionTypeId;
                            webSite.URL = webOrderingSite.URL;

                            if (webSite.ACSApplication != null)
                            {
                                webSite.ACSApplication.Name = webOrderingSite.Name;
                                webSite.ACSApplication.ExternalDisplayName = webOrderingSite.Name;
                                webSite.ACSApplication.DataVersion = newVersion;
                            }
                            var acsApplicationSites = entitiesContext.ACSApplicationSites.Where(a => a.ACSApplicationId == webSite.ACSApplicationId).ToList();

                            if (acsApplicationSites == null && webOrderingSite.MappedSiteIds != null)
                            {
                                foreach (var siteId in webOrderingSite.MappedSiteIds)
                                {
                                    entitiesContext.ACSApplicationSites.Add(new ACSApplicationSite { SiteId = siteId, ACSApplicationId = webOrderingSite.ACSApplicationId, DataVersion = newVersion });
                                }
                            }
                            else if (acsApplicationSites != null && (webOrderingSite.MappedSiteIds == null || webOrderingSite.MappedSiteIds.Count() == 0))
                            {
                                foreach (var site in acsApplicationSites)
                                {
                                    entitiesContext.ACSApplicationSites.Remove(site);
                                }
                            }
                            else if (webOrderingSite.MappedSiteIds != null && acsApplicationSites != null)
                            {
                                foreach (var site in acsApplicationSites)
                                {
                                    if (!webOrderingSite.MappedSiteIds.Contains(site.SiteId))
                                    {
                                        entitiesContext.ACSApplicationSites.Remove(site);
                                    }
                                }
                                foreach (int siteId in webOrderingSite.MappedSiteIds)
                                {
                                    if (acsApplicationSites.Where(a => a.SiteId == siteId).FirstOrDefault() == null)
                                    {
                                        entitiesContext.ACSApplicationSites.Add(new ACSApplicationSite { SiteId = siteId, ACSApplicationId = webSite.ACSApplicationId, DataVersion = newVersion });
                                    }
                                }
                            }
                            entitiesContext.SaveChanges();
                            success = 1;
                        }
                        else
                        {
                            success = 0;
                        }
                    }
                    transactionScope.Complete();
                }
            }
            return 0;
        }
    }
}


