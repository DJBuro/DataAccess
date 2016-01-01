using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SiteDetailsDataAccess : ISiteDetailsDataAccess
    {
        public string GetBySiteId(Guid siteId, DataTypeEnum dataType, out AndroCloudDataAccess.Domain.SiteDetails siteDetails)
        {
            siteDetails = null;
            ACSEntities acsEntities = new ACSEntities();

            var sitesQuery = from s in acsEntities.Sites
                             where s.ID == siteId
                             select s;
            Model.Site siteEntity = sitesQuery.FirstOrDefault();

            // Create a serializable SiteDetails object
            siteDetails = new AndroCloudDataAccess.Domain.SiteDetails();
            siteDetails.Id = siteEntity.ID;
            siteDetails.ExternalId = siteEntity.ExternalId;
            siteDetails.Name = siteEntity.SiteName;
            siteDetails.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
            siteDetails.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
            siteDetails.TimeZone = siteEntity.TimeZone;

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
            if (siteEntity.Addresses != null && siteEntity.Addresses.Count == 1)
            {
                Model.Address dbAddress = siteEntity.Addresses.First();

                siteDetails.Address = new AndroCloudDataAccess.Domain.Address();
                siteDetails.Address.Country = dbAddress.Country;
                siteDetails.Address.County = dbAddress.County;
                siteDetails.Address.Dps = dbAddress.DPS;
                siteDetails.Address.Lat = dbAddress.Lat.HasValue ? dbAddress.Lat.ToString() : "";
                siteDetails.Address.Locality = dbAddress.Locality;
                siteDetails.Address.Long = dbAddress.Long.HasValue ? dbAddress.Lat.ToString() : "";
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
                    timeSpanBlock.Day = openingHour.Day.Description;
                    timeSpanBlock.StartTime = openingHour.TimeStart.Hours + ":" + openingHour.TimeStart.Minutes;
                    timeSpanBlock.EndTime = openingHour.TimeEnd.Hours + ":" + openingHour.TimeStart.Minutes;

                    siteDetails.OpeningHours.Add(timeSpanBlock);
                }

            }

            return "";
        }
    }
}
