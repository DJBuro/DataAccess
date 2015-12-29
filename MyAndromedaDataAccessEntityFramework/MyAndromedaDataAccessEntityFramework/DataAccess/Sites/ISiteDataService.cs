using MyAndromeda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using MyAndromedaDataAccess.Domain;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Sites
{
    public interface ISiteDataService : IDependency
    {
        IEnumerable<Site> Query(Expression<Func<Store, bool>> query);
        IEnumerable<TResult> QueryTransform<TResult>(Expression<Func<Store, bool>> query, Expression<Func<Store, TResult>> transform);
    }

    public class SiteDataService : ISiteDataService 
    {
        public IEnumerable<Site> Query(Expression<Func<Store, bool>> query)
        {
            IEnumerable<Site> sites;

            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext())
            {
                var table = dbContext.Stores;
                var storeQuery = table.Where(query);
                var storeResult = storeQuery.ToArray();

                sites = storeResult.Select(e => e.ToDomain()).ToArray();
            }

            return sites;
        }

        public IEnumerable<TResult> QueryTransform<TResult>(Expression<Func<Store, bool>> query, Expression<Func<Store, TResult>> transform)
        {
            IEnumerable<TResult> data;

            using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext())
            {
                var table = dbContext.Stores;
                var storeQuery = table.Where(query).Select(transform);
                data = storeQuery.ToArray();
            }

            return data;
        }

        //public bool SiteBelongsToChain(int siteId) 
        //{
        //    Store store;
        //    using (var dbContext = new Model.AndroAdmin.AndroAdminDbContext()) 
        //    {
        //        var table = db
        //    }
        //}
    }
}
