using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Collections.Generic;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class CountryDataAccess : ICountryDataAccess
    {
        public string GetAll(out List<MyAndromedaDataAccess.Domain.Country> models)
        {
            models = new List<MyAndromedaDataAccess.Domain.Country>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                var query = from s in entitiesContext.Countries
                                 select s;

                foreach (Country country in query)
                {
                    MyAndromedaDataAccess.Domain.Country model = new MyAndromedaDataAccess.Domain.Country()
                    {
                        Id = country.Id,
                        CountryName = country.CountryName,
                        ISO3166_1_alpha_2 = country.ISO3166_1_alpha_2,
                        ISO3166_1_numeric = country.ISO3166_1_numeric
                    };

                    models.Add(model);
                }
            }

            return "";
        }
    }
}
