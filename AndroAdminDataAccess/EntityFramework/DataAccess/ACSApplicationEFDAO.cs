using AndroAdminDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class ACSApplicationEFDAO : IACSApplicationEFDAO
    {
        public string ConnectionStringOverride { get; set; }
        public IList<Domain.ACSApplicationEF> GetAll()
        {
            IList<Domain.ACSApplicationEF> results = new List<Domain.ACSApplicationEF>();
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);
                var query = entitiesContext.ACSApplications
                    .Include(e => e.ACSApplicationSites)
                    .Include(e => e.ACSApplicationSites.Select(x => x.Store))
                    .Include(e => e.ACSApplicationSites.Select(x => x.Store.Chain))
                    .Include(e => e.Partner);
                var result = query.ToArray();

                results = result.Select(entity => new Domain.ACSApplicationEF()
                {
                    Id = entity.Id,
                    ExternalApplicationId = entity.ExternalApplicationId,
                    Name = entity.Name,
                    ExternalDisplayName = entity.ExternalDisplayName,
                    PartnerId = entity.PartnerId,
                    DataVersion = entity.DataVersion,
                    ACSApplicationSites = entity.ACSApplicationSites
                }).ToList();
            }

            return results;
        }
    }
}
