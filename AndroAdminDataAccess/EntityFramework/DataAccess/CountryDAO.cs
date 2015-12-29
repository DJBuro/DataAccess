using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class CountryDAO : ICountryDAO
    {
        public string ConnectionStringOverride { get; set; }

        public List<Domain.Country> GetAll()
        {
            List<Domain.Country> models = new List<Domain.Country>();

            //using (AndroAdminEntities entitiesContext = ConnectionStringOverride == null ? new AndroAdminEntities() : new AndroAdminEntities(this.ConnectionStringOverride))
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Countries
                            select s;

                foreach (Country country in query)
                {
                    Domain.Country model = new Domain.Country()
                    {
                        Id = country.Id,
                        CountryName = country.CountryName,
                        ISO3166_1_alpha_2 = country.ISO3166_1_alpha_2,
                        ISO3166_1_numeric = country.ISO3166_1_numeric
                    };

                    models.Add(model);
                }
            }

            return models;
        }
    }
}
