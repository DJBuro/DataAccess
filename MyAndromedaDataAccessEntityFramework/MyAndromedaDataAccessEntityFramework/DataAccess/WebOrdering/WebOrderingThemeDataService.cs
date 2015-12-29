using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.WebOrdering
{
    public class WebOrderingThemeDataService : IWebOrderingThemeDataService
    {
        private readonly AndroAdminDbContext dataContext;
        public DbSet<AndroWebOrderingTheme> Table { get; private set; }
        public IQueryable<AndroWebOrderingTheme> TableQuery { get; private set; }

        public WebOrderingThemeDataService(AndroAdminDbContext dataContext)
        {
            this.dataContext = dataContext;
            this.Table = this.dataContext.Set<AndroWebOrderingTheme>();
            this.TableQuery = this.Table;
        }

        public void ChangeIncludeScope<TPropertyModel>(System.Linq.Expressions.Expression<Func<Model.AndroAdmin.AndroWebOrderingTheme, TPropertyModel>> predicate)
        {
            throw new NotImplementedException();
        }

        public Model.AndroAdmin.AndroWebOrderingTheme New()
        {
            throw new NotImplementedException();
        }

        public Model.AndroAdmin.AndroWebOrderingTheme Get(System.Linq.Expressions.Expression<Func<Model.AndroAdmin.AndroWebOrderingTheme, bool>> predicate)
        {
            var table = this.TableQuery;
            var tableQuery = table.Where(predicate);
            return tableQuery.SingleOrDefault();
        }

        public IQueryable<Model.AndroAdmin.AndroWebOrderingTheme> List()
        {
            return this.TableQuery;
        }

        public IQueryable<Model.AndroAdmin.AndroWebOrderingTheme> List(System.Linq.Expressions.Expression<Func<Model.AndroAdmin.AndroWebOrderingTheme, bool>> predicate)
        {
            return this.TableQuery.Where(predicate);
        }
    }
}
