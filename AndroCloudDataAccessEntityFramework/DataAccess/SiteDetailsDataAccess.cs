using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;
using AndroCloudHelper;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SiteDetailsDataAccess : ISiteDetailsDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string GetBySiteId(Guid siteId, DataTypeEnum dataType, out AndroCloudDataAccess.Domain.SiteDetails siteDetails)
        {
            siteDetails = null;

            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                var sitesQuery = from site in acsEntities.Sites.Include(e => e.SiteLoyalties)
                                 join siteStatus in acsEntities.SiteStatuses
                                   on site.SiteStatusID equals siteStatus.ID
                                 join spp in acsEntities.StorePaymentProviders
                                   on site.StorePaymentProviderID equals spp.ID
                                 into spp2
                                 from spp3 in spp2.DefaultIfEmpty()
                                 where
                                    site.ID == siteId
                                    && siteStatus.Status == "Live"
                                 select new
                                 {
                                     site.ID,
                                     site.ExternalId,
                                     site.ExternalSiteName,
                                     site.StoreConnected,
                                     site.EstimatedDeliveryTime,
                                     site.TimeZone,
                                     site.SiteMenus,
                                     site.Address,
                                     site.OpeningHours,
                                     site.Telephone,
                                     site.SiteLoyalties,
                                     ProviderName = (spp3 == null ? "" : spp3.ProviderName),
                                     ClientId = (spp3 == null ? "" : spp3.ClientId),
                                     ClientPassword = (spp3 == null ? "" : spp3.ClientPassword)
                                 };
                 var siteEntity = sitesQuery.FirstOrDefault();

                // Create a serializable SiteDetails object
                siteDetails = new AndroCloudDataAccess.Domain.SiteDetails();
                siteDetails.Id = siteEntity.ID;
                siteDetails.ExternalId = siteEntity.ExternalId;
                siteDetails.Name = siteEntity.ExternalSiteName;
                siteDetails.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
                siteDetails.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                siteDetails.TimeZone = siteEntity.TimeZone;
                siteDetails.PaymentProvider = siteEntity.ProviderName;
                siteDetails.PaymentClientId = siteEntity.ClientId;
                siteDetails.PaymentClientPassword = siteEntity.ClientPassword;
                siteDetails.Phone = siteEntity.Telephone;

                // Get the menu version for the requested data type (JSON or XML)
                foreach (Model.SiteMenu siteMenu in siteEntity.SiteMenus)
                {
                    if (siteMenu.MenuType == dataType.ToString() && siteMenu.Version.HasValue)
                    {
                        siteDetails.MenuVersion = siteMenu.Version.Value;
                        break;
                    }
                }

                // Address
                if (siteEntity.Address != null)
                {
                    Model.Address dbAddress = siteEntity.Address;

                    siteDetails.Address = new AndroCloudDataAccess.Domain.Address();
                    siteDetails.Address.Country = dbAddress.Country.CountryName;
                    siteDetails.Address.County = dbAddress.County;
                    siteDetails.Address.Dps = dbAddress.DPS;
                    siteDetails.Address.Lat = dbAddress.Lat;
                    siteDetails.Address.Locality = dbAddress.Locality;
                    siteDetails.Address.Long = dbAddress.Long;
                    siteDetails.Address.Org1 = dbAddress.Org1;
                    siteDetails.Address.Org2 = dbAddress.Org2;
                    siteDetails.Address.Org3 = dbAddress.Org3;
                    siteDetails.Address.Postcode = dbAddress.PostCode;
                    siteDetails.Address.Prem1 = dbAddress.Prem1;
                    siteDetails.Address.Prem2 = dbAddress.Prem2;
                    siteDetails.Address.Prem3 = dbAddress.Prem3;
                    siteDetails.Address.Prem4 = dbAddress.Prem4;
                    siteDetails.Address.Prem5 = dbAddress.Prem5;
                    siteDetails.Address.Prem6 = dbAddress.Prem6;
                    siteDetails.Address.RoadName = dbAddress.RoadName;
                    siteDetails.Address.RoadNum = dbAddress.RoadNum;
                    siteDetails.Address.Town = dbAddress.Town;
                }

                // Opening hours
                siteDetails.OpeningHours = new List<TimeSpanBlock>();
                if (siteEntity.OpeningHours != null)
                {
                    foreach (OpeningHour openingHour in siteEntity.OpeningHours)
                    {
                        TimeSpanBlock timeSpanBlock = new TimeSpanBlock();
                        timeSpanBlock.ID = openingHour.ID;
                        timeSpanBlock.Day = openingHour.Day.Description;
                        timeSpanBlock.StartTime = openingHour.TimeStart.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
                        timeSpanBlock.EndTime = openingHour.TimeEnd.Hours.ToString("00") + ":" + openingHour.TimeEnd.Minutes.ToString("00");
                        timeSpanBlock.OpenAllDay = openingHour.OpenAllDay;

                        siteDetails.OpeningHours.Add(timeSpanBlock);
                    }
                }
                siteDetails.SiteLoyalties = new List<AndroCloudDataAccess.Domain.SiteLoyalty>();
                if (siteEntity.SiteLoyalties != null)
                {
                    foreach (var config in siteEntity.SiteLoyalties)
                    {
                        AndroCloudDataAccess.Domain.SiteLoyalty siteConfig = new AndroCloudDataAccess.Domain.SiteLoyalty();
                        siteConfig.Id = config.Id;
                        //siteConfig.SiteId = config.SiteId;
                        siteConfig.RawConfiguration = config.Configuration;
                        siteConfig.ProviderName = config.ProviderName;
                        siteConfig.RawConfiguration = config.Configuration;
                        var t =  JsonConvert.DeserializeObject(string.IsNullOrWhiteSpace(config.Configuration) ? "{}" : config.Configuration);
                        

                        if(config.ProviderName.Equals("Andromeda", StringComparison.InvariantCultureIgnoreCase))
                        {
                            siteConfig.Configuration = JsonConvert.DeserializeObject<AndromedaLoyaltyConfiguration>(string.IsNullOrWhiteSpace(config.Configuration) ? "{}" : config.Configuration);
                        }
                        else 
                        {
                            siteConfig.Configuration = new AndromedaLoyaltyConfiguration(){ };
                        }
                        
                        if (siteConfig.Configuration != null && siteConfig.Configuration.Enabled.GetValueOrDefault())
                        {
                           siteDetails.SiteLoyalties.Add(siteConfig);
                        }
                    }
                }
            }

            return "";
        }
    }
}
