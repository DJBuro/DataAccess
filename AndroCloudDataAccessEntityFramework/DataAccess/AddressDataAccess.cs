using System;
using System.Data;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class AddressDataAccess : IAddressDataAccess
    {
        public string ConnectionStringOverride { get; set; }
        
        public string UpsertByExternalSiteIdMyAndromedaUserId(string externalSiteId, string myAndromedaUserId, AndroCloudDataAccess.Domain.Address address)
        {
            using (ACSEntities acsEntities = new ACSEntities())
            {
                DataAccessHelper.FixConnectionString(acsEntities, this.ConnectionStringOverride);

                //var acsQuery = from u in acsEntities.MyAndromedaUsers
                //               join e in acsEntities.Employees
                //                 on u.EmployeeID equals e.ID
                //               join g in acsEntities.Groups
                //                 on u.GroupID equals g.ID
                //               join sg in acsEntities.SitesGroups
                //                 on g.ID equals sg.GroupID
                //               join s in acsEntities.Sites
                //                 on sg.SiteID equals s.ID
                //               join a in acsEntities.Addresses
                //                 on s.AddressID equals a.ID
                //               where u.Username == myAndromedaUserId
                //                 && u.IsEnabled == true
                //                 && s.ExternalId == externalSiteId
                //               select a;

                //Model.Address acsEntity = acsQuery.FirstOrDefault();

                //// Insert or update?
                //if (acsEntity == null)
                //{
                //    acsEntity = new Model.Address();
                //    acsEntity.ID = Guid.NewGuid();
                //}

                //// Make the changes
                //acsEntity.Country = address.Country;
                //acsEntity.County = address.County;
                //acsEntity.Locality = address.Locality;
                //acsEntity.Org1 = address.Org1;
                //acsEntity.Org2 = address.Org2;
                //acsEntity.Org3 = address.Org3;
                //acsEntity.PostCode = address.Postcode;
                //acsEntity.Prem1 = address.Prem1;
                //acsEntity.Prem2 = address.Prem2;
                //acsEntity.Prem3 = address.Prem3;
                //acsEntity.Prem4 = address.Prem4;
                //acsEntity.Prem5 = address.Prem5;
                //acsEntity.Prem6 = address.Prem6;
                //acsEntity.RoadName = address.RoadName;
                //acsEntity.RoadNum = address.RoadNum;
                //acsEntity.State = address.State;
                //acsEntity.Town = address.Town;
                //acsEntity.DPS = address.Dps;

                //// Add a new address
                //if (acsEntity == null)
                //{
                //    acsEntities.AddToAddresses(acsEntity);                
                //}

                //acsEntities.SaveChanges();
            }

            return "";
        }
    }
}
