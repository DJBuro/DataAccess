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
            siteDetails.Name = siteEntity.ExternalSiteName;
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
            if (siteEntity.Address != null)
            {
                Model.Address dbAddress = siteEntity.Address;

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
                    timeSpanBlock.ID = openingHour.ID;
                    timeSpanBlock.Day = openingHour.Day.Description;
                    timeSpanBlock.StartTime = openingHour.TimeStart.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
                    timeSpanBlock.EndTime = openingHour.TimeEnd.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
                    timeSpanBlock.OpenAllDay = openingHour.OpenAllDay;

                    siteDetails.OpeningHours.Add(timeSpanBlock);
                }

            }

            return "";
        }

        public string GetByExternalSiteIdMyAndromedaUserId(string externalSiteId, string myAndromedaUserId, out SiteDetails siteDetails)
        {
            siteDetails = null;
            ACSEntities acsEntities = new ACSEntities();

            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           join sg in acsEntities.SitesGroups
                             on g.ID equals sg.GroupID
                           join s in acsEntities.Sites
                             on sg.SiteID equals s.ID
                           where u.Username == myAndromedaUserId
                             && u.IsEnabled == true
                             && s.ExternalId == externalSiteId
                           select s;

            Model.Site acsEntity = acsQuery.FirstOrDefault();

            if (acsEntity != null)
            {
                // Create a serializable SiteDetails object
                siteDetails = new AndroCloudDataAccess.Domain.SiteDetails();
                siteDetails.Id = acsEntity.ID;
                siteDetails.ExternalId = acsEntity.ExternalId;
                siteDetails.Name = acsEntity.ExternalSiteName;
                siteDetails.IsOpen = acsEntity.StoreConnected.GetValueOrDefault(false);
                siteDetails.EstDelivTime = acsEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                siteDetails.TimeZone = acsEntity.TimeZone;
                siteDetails.Phone = acsEntity.Telephone;

                // Address
                if (acsEntity.Address != null)
                {
                    Model.Address dbAddress = acsEntity.Address;

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
                if (acsEntity.OpeningHours != null)
                {
                    foreach (OpeningHour openingHour in acsEntity.OpeningHours)
                    {
                        TimeSpanBlock timeSpanBlock = new TimeSpanBlock();
                        timeSpanBlock.ID = openingHour.ID;
                        timeSpanBlock.Day = openingHour.Day.Description;
                        timeSpanBlock.StartTime = openingHour.TimeStart.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
                        timeSpanBlock.EndTime = openingHour.TimeEnd.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
                        timeSpanBlock.OpenAllDay = openingHour.OpenAllDay;

                        siteDetails.OpeningHours.Add(timeSpanBlock);
                    }
                }
            }

            return "";
        }


        public string Update(string myAndromedaUserId, SiteDetails siteDetails)
        {
            ACSEntities acsEntities = new ACSEntities();

            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           join sg in acsEntities.SitesGroups
                             on g.ID equals sg.GroupID
                           join s in acsEntities.Sites
                             on sg.SiteID equals s.ID
                           where u.Username == myAndromedaUserId
                             && u.IsEnabled == true
                             && s.ExternalId == siteDetails.ExternalId
                           select s;

            Model.Site acsEntity = acsQuery.FirstOrDefault();

            acsEntity.Telephone = siteDetails.Phone;

            acsEntities.SaveChanges();

            return "";
        }
    }
}
