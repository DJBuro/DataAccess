using System;
using System.Linq;
using MyAndromedaDataAccess.Domain.Marketing;
using Domain = MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccessEntityFramework
{
    public static class DomainModelUpdateExtensions
    {
        /// <summary>
        /// Toes the domain model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Domain.Marketing.Customer ToDomainModel(this Model.CustomerDataWarehouse.CustomerRecord entity)
        {
            var model = new Domain.Marketing.Customer()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                Surname = entity.Surname,
                Title = entity.Title,
                ContactDetails = entity.Contacts.Select(e => new Domain.Marketing.Contact() { ContactType = e.ContactTypeId, Value = e.Value }).ToList()
                //Email = entity.Contacts.Where(e=> e.ContactTypeId == MyAndromedaDataAccess.Domain.DataWarehouse.ContactType.Email)
            };

            return model;
        }

        /// <summary>
        /// Toes the domain model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Domain.Marketing.EmailCampaign ToDomainModel(this Model.MyAndromeda.EmailCampaign entity)
        {
            var model = new MyAndromedaDataAccess.Domain.Marketing.EmailCampaign()
            {
                Id = entity.Id,
                Reference = entity.Reference,
                Subject = entity.Title,
                EmailTemplate = entity.EmailTemplate,
                Created = entity.Created,
                Modified = entity.Modified,
                ChainId = entity.ChainId,
                ChainOnly = entity.ChainOnly
            };

            model.SiteIds = entity.EmailCampaignSites.Select(e => new EmailCampaignSitePart()
            {
                SiteId = e.SiteId,
                Editable = e.Editable,
                //EmailCampaign = model
            }).ToList();

            return model;
        }

        /// <summary>
        /// Updates the specified entity... with
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="domainModel">The domain model.</param>
        public static void Update(this Model.MyAndromeda.EmailCampaign entity, Domain.Marketing.EmailCampaign domainModel)
        {
            if (entity.Created == default(DateTime))
            {
                entity.Created = domainModel.Created;
            }

            entity.Modified = DateTime.Now;
            entity.Reference = domainModel.Reference;
            entity.Title = domainModel.Subject;
            entity.EmailTemplate = domainModel.EmailTemplate;
            entity.ChainId = domainModel.ChainId;
            entity.ChainOnly = domainModel.ChainOnly;

            //find items that are no longer in the model
            var removeLinks = entity.EmailCampaignSites.Select(e => e.SiteId).Where(e => !domainModel.SiteIds.Any(d => d.SiteId == e)).ToList();
            //find items that are not in the model
            var addLinks = domainModel.SiteIds.Where(d => !entity.EmailCampaignSites.Any(e => e.SiteId == d.SiteId)).ToList();

            //remove linked records.
            foreach (var id in removeLinks)
            {
                var item = entity.EmailCampaignSites.FirstOrDefault(e => e.SiteId == id);
                entity.EmailCampaignSites.Remove(item);
            }

            foreach (var id in addLinks)
            {
                //add in new linked records.
                entity.EmailCampaignSites.Add(new Model.MyAndromeda.EmailCampaignSite()
                {
                    SiteId = id.SiteId,
                    Editable = id.Editable
                });
            }
            //entity. = domainModel.SiteIds;
        }

        /// <summary>
        /// creates a domain model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Domain.Marketing.EmailSettings ToDomainModel(this Model.MyAndromeda.EmailCampaignSetting entity)
        {
            return new EmailSettings()
            {
                ChainId = entity.ChainId,
                SiteId = entity.SiteId,
                From = entity.FromEmail,
                Host = entity.Host,
                Id = entity.Id,
                Password = entity.Password,
                PickupFolder = entity.DropFolder,
                Port = entity.Port,
                SSL = entity.SSL,
                UserName = entity.UserName
            };
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="domainModel">The domain model.</param>
        public static void Update(this Model.MyAndromeda.EmailCampaignSetting entity, Domain.Marketing.EmailSettings domainModel)
        {
            entity.UserName = domainModel.UserName;
            entity.SSL = domainModel.SSL;
            entity.Port = domainModel.Port;
            entity.Password = domainModel.Password ?? string.Empty;
            entity.Id = domainModel.Id;
            entity.Host = domainModel.Host ?? string.Empty;
            entity.FromEmail = domainModel.From;
            entity.ChainId = domainModel.ChainId;
            entity.DropFolder = domainModel.PickupFolder ?? string.Empty;
        }

        /// <summary>
        /// Toes the domain model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Domain.Marketing.EmailCampaignTask ToDomainModel(this Model.MyAndromeda.EmailCampaignTask entity)
        {
            return new EmailCampaignTask()
            {
                Id = entity.Id,
                Completed = entity.Completed,
                Created = entity.CreatedOnUtc,
                RanAt = entity.RanAtUtc,
                RunLaterOnUtc = entity.RunLaterOnUtc,
                RetryAfter = entity.RetryAfter,
                Started = entity.Started,
                EmailSettings = entity.EmailCampaignSetting == null ? null : entity.EmailCampaignSetting.ToDomainModel(),
                EmailCampaign = entity.EmailCampaign == null ? null : entity.EmailCampaign.ToDomainModel(),
                CompletedAt = entity.CompletedAt
            };
        }

        /// <summary>
        /// Toes the domain model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static void Update(this Model.MyAndromeda.EmailCampaignTask entity, Domain.Marketing.EmailCampaignTask domainModel)
        {
            entity.Completed = domainModel.Completed;
            entity.Canceled = domainModel.Canceled;
            entity.CreatedOnUtc = domainModel.Created;
            entity.RanAtUtc = domainModel.RanAt;
            entity.RetryAfter = domainModel.RetryAfter;
            entity.RunLaterOnUtc = domainModel.RunLaterOnUtc;
            entity.Started = domainModel.Started;
            entity.CompletedAt = domainModel.CompletedAt;
        }

        /// <summary>
        /// Creates a domain model from the store db entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Domain.Site ToDomain(this Model.AndroAdmin.Store entity)
        {
            var site = new MyAndromedaDataAccess.Domain.Site()
            {
                Id = entity.Id,
                EstDelivTime = entity.EstimatedDeliveryTime.GetValueOrDefault(0),
                MenuVersion = 0,
                ClientSiteName = entity.ClientSiteName == null ? string.Empty : entity.ClientSiteName,
                CustomerSiteId = entity.CustomerSiteId == null ? string.Empty : entity.CustomerSiteId,
                LicenceKey = entity.LicenseKey,
                ExternalSiteId = entity.ExternalId,
                AndromediaSiteId = entity.AndromedaSiteId,
                ChainId = entity.ChainId,
                ExternalName = entity.ExternalSiteName,
                Longitude = entity.Address.Long,
                Latitude = entity.Address.Lat,
                PhoneNumber = entity.Telephone
            };

            return site;
        }
       
        
    }
}