using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccess.Domain;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;

namespace MyAndromedaDataAccessEntityFramework.DataAccess
{
    public class SiteDetailsDataAccess : ISiteDetailsDataAccess
    {
        public string GetBySiteId(int siteId, out SiteDetails siteDetails)
        {
            siteDetails = null;

            using (var entitiesContext = new AndroAdminDbContext())
            {
                var table = entitiesContext.Stores.Include(e => e.Address);
                var query = table.Where(e => e.Id == siteId);

                MyAndromedaDataAccessEntityFramework.Model.AndroAdmin.Store entity = query.SingleOrDefault();

                if (entity != null)
                {
                    // Create a serializable SiteDetails object
                    siteDetails = new MyAndromedaDataAccess.Domain.SiteDetails();
                    siteDetails.Id = entity.Id;
                    siteDetails.CustomerSiteId = entity.CustomerSiteId;
                    siteDetails.ExternalSiteId = entity.ExternalId;
                    siteDetails.AndroSiteName = entity.Name;
                    siteDetails.ClientSiteName = entity.ClientSiteName;
                    siteDetails.ExternalSiteName = entity.ExternalSiteName;
                    siteDetails.EstDelivTime = entity.EstimatedDeliveryTime.GetValueOrDefault(0);
                    siteDetails.TimeZone = entity.TimeZone;
                    siteDetails.Phone = entity.Telephone;

                    // Address
                    if (entity.Address != null)
                    {
                        MyAndromedaDataAccessEntityFramework.Model.AndroAdmin.Address addressEntity = entity.Address;

                        siteDetails.Address = new MyAndromedaDataAccess.Domain.Address();
                        siteDetails.Address.CountryId = addressEntity.Country.Id;
                        siteDetails.Address.County = addressEntity.County;
                        siteDetails.Address.Dps = addressEntity.DPS;
                        siteDetails.Address.Lat = addressEntity.Lat;
                        siteDetails.Address.Locality = addressEntity.Locality;
                        siteDetails.Address.Long = addressEntity.Long;
                        siteDetails.Address.Org1 = addressEntity.Org1;
                        siteDetails.Address.Org2 = addressEntity.Org2;
                        siteDetails.Address.Org3 = addressEntity.Org3;
                        siteDetails.Address.Postcode = addressEntity.PostCode;
                        siteDetails.Address.Prem1 = addressEntity.Prem1;
                        siteDetails.Address.Prem2 = addressEntity.Prem2;
                        siteDetails.Address.Prem3 = addressEntity.Prem3;
                        siteDetails.Address.Prem4 = addressEntity.Prem4;
                        siteDetails.Address.Prem5 = addressEntity.Prem5;
                        siteDetails.Address.Prem6 = addressEntity.Prem6;
                        siteDetails.Address.RoadName = addressEntity.RoadName;
                        siteDetails.Address.RoadNum = addressEntity.RoadNum;
                        siteDetails.Address.Town = addressEntity.Town;
                        siteDetails.Address.Country = new MyAndromedaDataAccess.Domain.Country()
                        {
                            CountryName = addressEntity.Country.CountryName,
                            Id = addressEntity.Country.Id,
                            ISO3166_1_alpha_2 = addressEntity.Country.ISO3166_1_alpha_2,
                            ISO3166_1_numeric = addressEntity.Country.ISO3166_1_numeric
                        };
                    }

                    
                }
            }

            return string.Empty;
        }

        public string Update(int siteId, SiteDetails siteDetails)
        {
            using (var entitiesContext = new AndroAdminDbContext())
            {
                var query = from s in entitiesContext.Stores
                            where s.Id == siteId
                            select s;

                MyAndromedaDataAccessEntityFramework.Model.AndroAdmin.Store entity = query.FirstOrDefault();

                entity.ClientSiteName = siteDetails.ClientSiteName;
                entity.ExternalSiteName = siteDetails.ExternalSiteName;
                entity.CustomerSiteId = siteDetails.CustomerSiteId;
                entity.ExternalId = siteDetails.ExternalSiteId;
                entity.Telephone = siteDetails.Phone;
                entity.DataVersion = Model.DataVersionHelper.GetNextDataVersion(entitiesContext);

                entitiesContext.SaveChanges();
            }

            return "";
        }
    }
}
