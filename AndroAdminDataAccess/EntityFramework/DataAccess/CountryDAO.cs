using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Transactions;
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


        public Domain.Country GetById(int countryId)
        {
            Domain.Country model = null;

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.Countries
                            where s.Id == countryId
                            select s;
                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.Country()
                    {
                        Id = entity.Id,
                        CountryName = entity.CountryName,
                        ISO3166_1_alpha_2 = entity.ISO3166_1_alpha_2,
                        ISO3166_1_numeric = entity.ISO3166_1_numeric
                    };
                }
            }

            return model;
        }
    }
}
