using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class ChainDAO : IChainDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.Chain> GetAll()
        {
            List<Domain.Chain> models = new List<Domain.Chain>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Chains
                            select s;

                foreach (var entity in query)
                {
                    Domain.Chain model = new Domain.Chain()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description
                    };

                    models.Add(model);
                }
            }

            return models;
        }
    }
}