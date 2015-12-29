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

        public void ChangeIncludeScope<TPropertyModel>(System.Linq.Expressions.Expression<Func<Model.AndroAdmin.AndroWebOrderingWebsite, TPropertyModel>> predicate)
        {
            this.TableQuery =
                this.TableQuery.Include(predicate);
        }

        public Model.AndroAdmin.AndroWebOrderingWebsite New()
        {
            throw new NotImplementedException();
        }

        public Model.AndroAdmin.AndroWebOrderingWebsite Get(System.Linq.Expressions.Expression<Func<Model.AndroAdmin.AndroWebOrderingWebsite, bool>> predicate)
        {
            var table = this.TableQuery;
            var tableQuery = table.Where(predicate);
            return tableQuery.SingleOrDefault();
        }

        public IQueryable<Model.AndroAdmin.AndroWebOrderingWebsite> List()
        {
            return this.TableQuery;
        }

        public IQueryable<Model.AndroAdmin.AndroWebOrderingWebsite> List(System.Linq.Expressions.Expression<Func<Model.AndroAdmin.AndroWebOrderingWebsite, bool>> predicate)
        {
            return this.TableQuery.Where(predicate);
        }
    }
}
