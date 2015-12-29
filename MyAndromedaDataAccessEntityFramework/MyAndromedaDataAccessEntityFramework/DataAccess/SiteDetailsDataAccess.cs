using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Collections.Generic;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Model;
using MyAndromedaDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class SiteDetailsDataAccess : ISiteDetailsDataAccess
    {
        //public string GetBySiteId(int siteId, DataTypeEnum dataType, out MyAndromedaDataAccess.Domain.SiteDetails siteDetails)
        //{
        //    siteDetails = null;
//            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

//            var sitesQuery = from s in androAdminEntities.Stores
// //                            join spp in androAdminEntities.StorePaymentProviders
//  //                           on s.StorePaymentProviderID equals spp.ID
//  //                           into spp2
// //                            from spp3 in spp2.DefaultIfEmpty()
//                             where s.Id == siteId
//                             select new
//                             {
//                                 s.Id,
//                                 s.ExternalId,
//                                 s.ExternalSiteName,
// //                                s.StoreConnected,
//                                 s.EstimatedDeliveryTime,
//                                 s.TimeZone,
// //                                s.SiteMenus,
//                                 s.Address,
//                                 s.OpeningHours//,
//                                 //ProviderName = (spp3 == null ? "" : spp3.ProviderName),
//                                 //ClientId = (spp3 == null ? "" : spp3.ClientId),
//                                 //ClientPassword = (spp3 == null ? "" : spp3.ClientPassword)
//                             };
//            var siteEntity = sitesQuery.FirstOrDefault();

//            // Create a serializable SiteDetails object
//            siteDetails = new SiteDetails();
//            siteDetails.Id = siteEntity.Id;
//            siteDetails.ExternalId = siteEntity.ExternalId;
//            siteDetails.Name = siteEntity.ExternalSiteName;
////            siteDetails.IsOpen = siteEntity.StoreConnected.GetValueOrDefault(false);
//            siteDetails.EstDelivTime = siteEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
//            siteDetails.TimeZone = siteEntity.TimeZone;
////            siteDetails.PaymentProvider = siteEntity.ProviderName;
////            siteDetails.PaymentClientId = siteEntity.ClientId;
////            siteDetails.PaymentClientPassword = siteEntity.ClientPassword;

//            // Get the menu version for the requested data type (JSON or XML)
//            //foreach (Model.SiteMenu siteMenu in siteEntity.SiteMenus)
//            //{
//            //    if (siteMenu.MenuType == dataType.ToString() && siteMenu.Version.HasValue)
//            //    {
//            //        siteDetails.MenuVersion = siteMenu.Version.Value;
//            //        break;
//            //    }
//            //}

//            // Address
//            if (siteEntity.Address != null)
//            {
//                MyAndromedaDataAccessEntityFramework.Model.Address dbAddress = siteEntity.Address;

//                siteDetails.Address = new MyAndromedaDataAccess.Domain.Address();
//                siteDetails.Address.Country = dbAddress.Country;
//                siteDetails.Address.County = dbAddress.County;
//                siteDetails.Address.Dps = dbAddress.DPS;
//                siteDetails.Address.Lat = dbAddress.Lat.HasValue ? dbAddress.Lat.ToString() : "";
//                siteDetails.Address.Locality = dbAddress.Locality;
//                siteDetails.Address.Long = dbAddress.Long.HasValue ? dbAddress.Lat.ToString() : "";
//                siteDetails.Address.Org1 = dbAddress.Org1;
//                siteDetails.Address.Org2 = dbAddress.Org2;
//                siteDetails.Address.Org3 = dbAddress.Org3;
//                siteDetails.Address.Postcode = dbAddress.PostCode;
//                siteDetails.Address.Prem1 = dbAddress.Prem1;
//                siteDetails.Address.Prem2 = dbAddress.Prem2;
//                siteDetails.Address.Prem3 = dbAddress.Prem3;
//                siteDetails.Address.Prem4 = dbAddress.Prem4;
//                siteDetails.Address.Prem5 = dbAddress.Prem5;
//                siteDetails.Address.Prem6 = dbAddress.Prem6;
//                siteDetails.Address.RoadName = dbAddress.RoadName;
//                siteDetails.Address.RoadNum = dbAddress.RoadNum;
//                siteDetails.Address.Town = dbAddress.Town;
//            }

//            // Opening hours
//            siteDetails.OpeningHours = new List<TimeSpanBlock>();
//            if (siteEntity.OpeningHours != null)
//            {
//                foreach (OpeningHour openingHour in siteEntity.OpeningHours)
//                {
//                    TimeSpanBlock timeSpanBlock = new TimeSpanBlock();
//                    timeSpanBlock.Id = openingHour.Id;
//                    timeSpanBlock.Day = openingHour.Day.Description;
//                    timeSpanBlock.StartTime = openingHour.TimeStart.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
//                    timeSpanBlock.EndTime = openingHour.TimeEnd.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
//                    timeSpanBlock.OpenAllDay = openingHour.OpenAllDay;

//                    siteDetails.OpeningHours.Add(timeSpanBlock);
//                }
//            }

        //    return "";
        //}

        public string GetBySiteId(int siteId, out SiteDetails siteDetails)
        {
            siteDetails = null;
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var acsQuery = from s in androAdminEntities.Stores
                           where s.Id == siteId
                           select s;

            MyAndromedaDataAccessEntityFramework.Model.Store acsEntity = acsQuery.FirstOrDefault();

            if (acsEntity != null)
            {
                // Create a serializable SiteDetails object
                siteDetails = new MyAndromedaDataAccess.Domain.SiteDetails();
                siteDetails.Id = acsEntity.Id;
                siteDetails.ExternalId = acsEntity.ExternalId;
                siteDetails.Name = acsEntity.ExternalSiteName;
                //                siteDetails.IsOpen = acsEntity.StoreConnected.GetValueOrDefault(false);
                siteDetails.EstDelivTime = acsEntity.EstimatedDeliveryTime.GetValueOrDefault(0);
                siteDetails.TimeZone = acsEntity.TimeZone;
                siteDetails.Phone = acsEntity.Telephone;

                // Address
                if (acsEntity.Address != null)
                {
                    MyAndromedaDataAccessEntityFramework.Model.Address dbAddress = acsEntity.Address;

                    siteDetails.Address = new MyAndromedaDataAccess.Domain.Address();
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
                        timeSpanBlock.Id = openingHour.Id;
                        timeSpanBlock.Day = openingHour.Day.Description;
                        timeSpanBlock.StartTime = openingHour.TimeStart.Hours.ToString("00") + ":" + openingHour.TimeStart.Minutes.ToString("00");
                        timeSpanBlock.EndTime = openingHour.TimeEnd.Hours.ToString("00") + ":" + openingHour.TimeEnd.Minutes.ToString("00");
                        timeSpanBlock.OpenAllDay = openingHour.OpenAllDay;

                        siteDetails.OpeningHours.Add(timeSpanBlock);
                    }
                }
            }

            return "";
        }


        public string Update(int siteId, SiteDetails siteDetails)
        {
            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var acsQuery = from s in androAdminEntities.Stores
                           where s.Id == siteId
                           select s;

            MyAndromedaDataAccessEntityFramework.Model.Store acsEntity = acsQuery.FirstOrDefault();

            acsEntity.Telephone = siteDetails.Phone;

            androAdminEntities.SaveChanges();

            return "";
        }
    }
}
