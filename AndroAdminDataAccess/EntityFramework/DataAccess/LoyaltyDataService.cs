using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class LoyaltyDataService 
    {
        public IEnumerable<StoreLoyalty> List(Expression<Func<StoreLoyalty, bool>> query) 
        {
            IEnumerable<StoreLoyalty> results = Enumerable.Empty<StoreLoyalty>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                results = dbContext.StoreLoyalties
                                   .Include(e => e.Store)
                                   .Where(query)
                                   .ToArray();    
            }

            return results;
        }
    }
}