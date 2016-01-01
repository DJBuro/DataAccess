﻿using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.WebOrdering
{
    public class WebOrderingWebSiteDataService : IWebOrderingWebSiteDataService
    {
        private readonly AndroAdminDbContext dataContext;
        public DbSet<AndroWebOrderingWebsite> Table { get; private set; }
        public IQueryable<AndroWebOrderingWebsite> TableQuery { get; private set; }

        public WebOrderingWebSiteDataService(AndroAdminDbContext dataContext)
        {
            this.dataContext = dataContext;
            this.Table = this.dataContext.Set<AndroWebOrderingWebsite>();
            this.TableQuery = this.Table
                .Include(e => e.ACSApplication)
                .Include(e => e.Chain);
        }

        public void Update(AndroWebOrderingWebsite website)
        {
            AndroWebOrderingWebsite existing = this.Table.Find(website.Id);
            ((IObjectContextAdapter)this.dataContext).ObjectContext.Detach(existing);
            dataContext.Entry(website).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public void ChangeIncludeScope<TPropertyModel>(Expression<Func<Model.AndroAdmin.AndroWebOrderingWebsite, TPropertyModel>> predicate)
        {
            this.TableQuery =
                this.TableQuery.Include(predicate);
        }

        public Model.AndroAdmin.AndroWebOrderingWebsite New()
        {
            throw new NotImplementedException();
        }

        public Model.AndroAdmin.AndroWebOrderingWebsite Get(Expression<Func<Model.AndroAdmin.AndroWebOrderingWebsite, bool>> predicate)
        {
            var table = this.TableQuery;
            var tableQuery = table.Where(predicate);
            return tableQuery.FirstOrDefault();
        }

        public IQueryable<Model.AndroAdmin.AndroWebOrderingWebsite> List()
        {
            return this.TableQuery;
        }

        public IQueryable<Model.AndroAdmin.AndroWebOrderingWebsite> List(Expression<Func<Model.AndroAdmin.AndroWebOrderingWebsite, bool>> predicate)
        {
            return this.TableQuery.Where(predicate);
        }

        public AndroWebOrderingWebsite GetWebOrderingSiteForOrder(int applicationId, string externalSiteId) {
            
            var site = dataContext.Stores.Where(e => e.ExternalId.Equals(externalSiteId,StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            AndroWebOrderingWebsite website = null;

            if (site != null) {
                var application = dataContext.ACSApplications.Include(e => e.ACSApplicationSites).Include(e => e.AndroWebOrderingWebsites).Where(e => e.Id == applicationId && e.ACSApplicationSites.Where(a => a.SiteId == site.Id).Count() > 0).FirstOrDefault();
                website = application != null ? application.AndroWebOrderingWebsites.FirstOrDefault() : website;
            }

            return website;

        }
        
    }
}
