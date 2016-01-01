using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreAMSServerDAO : IStoreAMSServerDAO
    {
        public string ConnectionStringOverride { get; set; }

        public void Add(Domain.StoreAMSServer storeAMSServer)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                StoreAMSServer entity = new StoreAMSServer()
                {
                    AMSServerId = storeAMSServer.AMSServer.Id,
                    Priority = storeAMSServer.Priority,
                    StoreId = storeAMSServer.Store.Id
                };

                entitiesContext.StoreAMSServers.Add(entity);
                entitiesContext.SaveChanges();

                storeAMSServer.Id = entity.Id;
            }
        }

        public Domain.StoreAMSServer GetByStoreIdAMServerId(int storeId, int amsServerId)
        {
            Domain.StoreAMSServer model = null;

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServers
                            where s.StoreId == storeId
                            && s.AMSServerId == amsServerId
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    model = new Domain.StoreAMSServer()
                    {
                        Id = entity.Id,
                        Priority = entity.Priority
                    };
                }
            }

            return model;
        }

        public void DeleteByAMSServerId(int amsServerId)
        {
             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServers
                            where s.AMSServerId == amsServerId
                            select s;

                foreach (var entity in query)
                {
                    entitiesContext.StoreAMSServers.Remove(entity);
                }

                entitiesContext.SaveChanges();
            }
        }

        public void DeleteById(int id)
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServers
                            where s.Id == id
                            select s;

                var entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entitiesContext.StoreAMSServers.Remove(entity);
                    entitiesContext.SaveChanges();
                }
            }
        }


        public IList<Domain.StoreAMSServer> GetByAMServerName(string amsServerName)
        {
            List<Domain.StoreAMSServer> model = new List<Domain.StoreAMSServer>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.StoreAMSServers
                            .Include("Store")
                            .Include("StoreAMSServerFtpSites")
                            .Include("StoreAMSServerFtpSites.FTPSite.FTPSiteType")
                            .Include("AMSServer")
                            .Include("Store.Address")
                            where s.AMSServer.Name == amsServerName
                            select s;

                foreach (var entity in query)
                {
                    List<Domain.FTPSite> ftpSites = new List<Domain.FTPSite>();
                    foreach (var subEntity in entity.StoreAMSServerFtpSites)
                    {
                        Domain.FTPSite ftpSite = new Domain.FTPSite()
                        {
                            FTPSiteType = new Domain.FTPSiteType() { Id = subEntity.FTPSite.FTPSiteType.Id, Name = subEntity.FTPSite.FTPSiteType.Name },
                            Name = subEntity.FTPSite.Name,
                            Password = subEntity.FTPSite.Password,
                            Port = subEntity.FTPSite.Port,
                            Url = subEntity.FTPSite.Url,
                            Username = subEntity.FTPSite.Username
                        };

                        ftpSites.Add(ftpSite);
                    }

                    Domain.Country country = null;

                    if (entity.Store.Address != null)
                    {
                        country = new Domain.Country()
                        {
                            Id = entity.Store.Address.CountryId,
                            CountryName = entity.Store.Address.Country.CountryName,
                            ISO3166_1_alpha_2 = entity.Store.Address.Country.ISO3166_1_alpha_2,
                            ISO3166_1_numeric = entity.Store.Address.Country.ISO3166_1_numeric
                        };
                    }

                    Domain.Store store = new Domain.Store()
                    {
                        AndromedaSiteId = entity.Store.AndromedaSiteId,
                        CustomerSiteId = entity.Store.CustomerSiteId,
                        LastFTPUploadDateTime = entity.Store.LastFTPUploadDateTime,
                        Name = entity.Store.Name,
                        StoreStatus = new Domain.StoreStatus() { Id = entity.Store.StoreStatu.Id, Status = entity.Store.StoreStatu.Status, Description = entity.Store.StoreStatu.Description },
                        Address = new Domain.Address()
                        {
                            Id = entity.Store.Address.Id,
                            County = entity.Store.Address.County,
                            DPS = entity.Store.Address.DPS,
                            Lat = entity.Store.Address.Lat,
                            Locality = entity.Store.Address.Locality,
                            Long = entity.Store.Address.Long,
                            Org1 = entity.Store.Address.Org1,
                            Org2 = entity.Store.Address.Org2,
                            Org3 = entity.Store.Address.Org3,
                            PostCode = entity.Store.Address.PostCode,
                            Prem1 = entity.Store.Address.Prem1,
                            Prem2 = entity.Store.Address.Prem2,
                            Prem3 = entity.Store.Address.Prem3,
                            Prem4 = entity.Store.Address.Prem4,
                            Prem5 = entity.Store.Address.Prem5,
                            Prem6 = entity.Store.Address.Prem6,
                            RoadName = entity.Store.Address.RoadName,
                            RoadNum = entity.Store.Address.RoadNum,
                            State = entity.Store.Address.State,
                            Town = entity.Store.Address.Town,
                            Country = country
                        }
                    };

                    Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                    {
                        Id = entity.Id,
                        Priority = entity.Priority,
                        FTPSites = ftpSites,
                        AMSServer = new Domain.AMSServer() { Name = entity.AMSServer.Name },
                        Store = store
                    };

                    model.Add(storeAMSServer);
                }
            }

            return model;
        }
    }
}