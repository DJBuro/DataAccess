using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;


namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class DeliveryAreaDataService
    {
        public IEnumerable<DeliveryArea> List(Expression<Func<DeliveryArea, bool>> query)
        {
            var results = Enumerable.Empty<DeliveryArea>();

            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.DeliveryAreas.Include(e => e.Store).Where(query);
                results = table.ToArray();
            }

            return results;
        }

        public IEnumerable<PostCodeSector> GetListPostCodes(Expression<Func<PostCodeSector, bool>> query)
        {
            var results = Enumerable.Empty<PostCodeSector>();
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
               var table = dbContext.PostCodeSectors.Include(e=>e.DeliveryZoneName).Include(e=>e.DeliveryZoneName.Store).Where(query);
               results = table.ToArray();
            }

            return results;
        }

    }
}
