using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class OrderDataAccess : IOrdersDataAccess
    {
        public string GetById(Guid externalOrderId, out AndroCloudDataAccess.Domain.Order order)
        {
            order = null;
            var acsEntities = new ACSEntities();

            var acsQuery = from o in acsEntities.Orders
                                where o.ID == externalOrderId
                                select o;

            var acsQueryEntity = acsQuery.FirstOrDefault();

            if (acsQueryEntity != null)
            {
                order = new AndroCloudDataAccess.Domain.Order();
                order.ID = acsQueryEntity.ID;
                order.ExternalID = acsQueryEntity.ExternalID;
                order.InternetOrderNumber = acsQueryEntity.InternetOrderNumber;
                order.RamesesStatusId = acsQueryEntity.OrderStatu.RamesesStatusId;
            }

            return "";
        }

        public string GetByInternetOrderNumber(int internetOrderNumber, out AndroCloudDataAccess.Domain.Order order)
        {
            order = null;
            var acsEntities = new ACSEntities();

            var acsQuery = from o in acsEntities.Orders
                           where o.InternetOrderNumber == internetOrderNumber
                           select o;

            var acsQueryEntity = acsQuery.FirstOrDefault();

            if (acsQueryEntity != null)
            {
                order = new AndroCloudDataAccess.Domain.Order();
                order.ID = acsQueryEntity.ID;
                order.ExternalID = acsQueryEntity.ExternalID;
                order.InternetOrderNumber = acsQueryEntity.InternetOrderNumber;
                order.RamesesStatusId = acsQueryEntity.OrderStatu.RamesesStatusId;
            }

            return "";
        }

        public string GetByExternalOrderNumber(string externalOrderId, out AndroCloudDataAccess.Domain.Order order)
        {
            order = null;
            var acsEntities = new ACSEntities();

            var acsQuery = from o in acsEntities.Orders
                           where o.ExternalID == externalOrderId
                           select o;

            var acsQueryEntity = acsQuery.FirstOrDefault();

            if (acsQueryEntity != null)
            {
                order = new AndroCloudDataAccess.Domain.Order();
                order.ID = acsQueryEntity.ID;
                order.ExternalID = acsQueryEntity.ExternalID;
                order.InternetOrderNumber = acsQueryEntity.InternetOrderNumber;
                order.RamesesStatusId = acsQueryEntity.OrderStatu.RamesesStatusId;
            }

            return "";
        }

        public string Update(Guid orderId, Guid orderStatusId)
        {
            var acsEntities = new ACSEntities();

            var acsQuery = from o in acsEntities.Orders
                           where o.ID == orderId
                           select o;

            var acsQueryEntity = acsQuery.FirstOrDefault();

            // Update the menu record
            if (acsQueryEntity != null)
            {
                acsQueryEntity.StatusId = orderStatusId;
                acsEntities.SaveChanges();
            }

            return "";
        }

        public string GetByPartnerIdOrderId(Guid partnerId, Guid orderId, out AndroCloudDataAccess.Domain.Order order)
        {
            order = null;
            var acsEntities = new ACSEntities();

            var acsQuery = from o in acsEntities.Orders
                             join s in acsEntities.Sites
                             on o.SiteID equals s.ID
                             join sg in acsEntities.SitesGroups
                             on s.ID equals sg.SiteID
                             join g in acsEntities.Groups
                             on sg.GroupID equals g.ID
                             join p in acsEntities.Partners
                             on g.PartnerID equals p.ID
                             join sm in acsEntities.SiteMenus
                             on s.ID equals sm.SiteID
                             where p.ID == partnerId
                             && o.ID == orderId
                             select o;
                             //select new { s.ID, s.EstimatedDeliveryTime, s.StoreConnected, sm.Version, s.SiteName, s.ExternalId, s.LicenceKey };

            var acsQueryEntity = acsQuery.FirstOrDefault();

            if (acsQueryEntity != null)
            {
                order = new AndroCloudDataAccess.Domain.Order();
                order.ID = acsQueryEntity.ID;
                order.ExternalID = acsQueryEntity.ExternalID;
                order.InternetOrderNumber = acsQueryEntity.InternetOrderNumber;
                order.RamesesStatusId = acsQueryEntity.OrderStatu.RamesesStatusId;
            }

            return "";
        }
    }
}
