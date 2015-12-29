﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class StoreAMSServerFtpSiteDAO : IStoreAMSServerFTPSiteDAO
    {
        public IList<Domain.StoreAMSServerFtpSite> GetAll()
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.StoreAMSServerFtpSites.Include("StoreAMSServer")
                            .Include("StoreAMSServer.Store")
                            .Include("StoreAMSServer.AMSServer")
                        select s;

            foreach (var entity in query)
            {
                Domain.FTPSite ftpSite = new Domain.FTPSite()
                {
                    Id = entity.FTPSite.Id,
                    Name = entity.FTPSite.Name,
                    Url = entity.FTPSite.Url,
                    Port = entity.FTPSite.Port,
                    Username = entity.FTPSite.Username,
                    Password = entity.FTPSite.Password,
                    FTPSiteType = new Domain.FTPSiteType() { Id = entity.FTPSite.FTPSiteType.Id, Name = entity.FTPSite.FTPSiteType.Name }
                };

                Domain.Store store = new Domain.Store()
                {
                    Id = entity.StoreAMSServer.Store.Id,
                    Name = entity.StoreAMSServer.Store.Name,
                    AndromedaSiteId = entity.StoreAMSServer.Store.AndromedaSiteId,
                    CustomerSiteId = entity.StoreAMSServer.Store.CustomerSiteId,
                    LastFTPUploadDateTime = entity.StoreAMSServer.Store.LastFTPUploadDateTime
                };

                Domain.AMSServer amsServer = new Domain.AMSServer()
                {
                    Id = entity.StoreAMSServer.AMSServer.Id,
                    Name = entity.StoreAMSServer.AMSServer.Name,
                    Description = entity.StoreAMSServer.AMSServer.Description
                };

                Domain.StoreAMSServer storeAMSServer = new Domain.StoreAMSServer()
                {
                    Id = entity.StoreAMSServer.Id,
                    Priority = entity.StoreAMSServer.Priority,
                    Store = store,
                    AMSServer = amsServer
                };

                Domain.StoreAMSServerFtpSite model = new Domain.StoreAMSServerFtpSite()
                {
                    Id = entity.Id,
                    FTPSite = ftpSite,
                    StoreAMSServer = storeAMSServer
                };

                models.Add(model);
            }

            return models;
        }

        public IList<Domain.StoreAMSServerFtpSite> GetBySiteId(int siteId)
        {
            IEnumerable<StoreAMSServerFtpSite> storeAMSServerFtpSites = null;

            using (ISession session = nHibernateHelper.SessionFactory.OpenSession())
            {
                storeAMSServerFtpSites = session.CreateQuery("from " + typeof(StoreAMSServerFtpSite) + " where StoreAMSServer.Store.Id=:siteId")
                    .SetParameter("siteId", siteId)
                    .List<StoreAMSServerFtpSite>();
            }

            return storeAMSServerFtpSites;
        }

        public void Add(Domain.StoreAMSServerFtpSite storeAMSServerFtpSite)
        {
            throw new NotImplementedException();
        }

        public Domain.StoreAMSServerFtpSite GetBySiteIdAMSServerIdFTPSiteId(int storeAMSServerId, int ftpSiteId)
        {
            throw new NotImplementedException();
        }

        public void DeleteByFTPSiteId(int ftpSiteId)
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.StoreAMSServerFtpSites
                        where s.FTPSiteId == ftpSiteId
                        select s;

            foreach (var entity in query)
            {
                androAdminEntities.StoreAMSServerFtpSites.DeleteObject(entity);

                androAdminEntities.SaveChanges();
            }
        }


        public void DeleteByAMSServerId(int amsServerId)
        {
            List<Domain.StoreAMSServerFtpSite> models = new List<Domain.StoreAMSServerFtpSite>();

            AndroAdminEntities androAdminEntities = new AndroAdminEntities();

            var query = from s in androAdminEntities.StoreAMSServerFtpSites
                        where s.StoreAMSServer.AMSServerId == amsServerId
                        select s;

            foreach (var entity in query)
            {
                androAdminEntities.StoreAMSServerFtpSites.DeleteObject(entity);

                androAdminEntities.SaveChanges();
            }
        }
    }
}