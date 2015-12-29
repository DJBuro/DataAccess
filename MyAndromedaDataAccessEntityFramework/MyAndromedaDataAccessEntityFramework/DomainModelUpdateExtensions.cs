﻿using MyAndromedaDataAccess.Domain.Marketing;
using System;
using System.Linq;
using Domain = MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework
{
    public static class DomainModelUpdateExtensions
    {
        /// <summary>
        /// Toes the domain model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Domain.Customer ToDomainModel(this Model.CustomerRecord entity) 
        {
            var model = new Domain.Customer() 
            { 
                Id = entity.Id,
                FirstName = entity.FirstName,
                Surname = entity.Surname,
                Title = entity.Title,
                Email = entity.Email
            };

            return model;
        }

        /// <summary>
        /// Toes the domain model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Domain.EmailCampaign ToDomainModel(this Model.EmailCampaign entity)
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
                EmailCampaign = model
            }).ToList();

            return model;
        }

        /// <summary>
        /// Updates the specified entity... with
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="domainModel">The domain model.</param>
        public static void Update(this Model.EmailCampaign entity, Domain.EmailCampaign domainModel)
        {
            if (entity.Created == default(DateTime)) {
                entity.Created = domainModel.Created;
            }

            entity.Modified = DateTime.Now;
            entity.Reference = domainModel.Reference;
            entity.Title = domainModel.Subject;
            entity.EmailTemplate = domainModel.EmailTemplate;
            entity.ChainId = domainModel.ChainId;
            entity.ChainOnly = domainModel.ChainOnly;

            //find items that are no longer in the model
            var removeLinks = entity.EmailCampaignSites.Select(e => e.SiteId).Where(e=> !domainModel.SiteIds.Any(d=> d.SiteId == e)).ToList();
            //find items that are not in the model
            var addLinks = domainModel.SiteIds.Where(d => !entity.EmailCampaignSites.Any(e => e.SiteId == d.SiteId)).ToList();

            //remove linked records.
            foreach(var id in removeLinks)
            {
                var item = entity.EmailCampaignSites.FirstOrDefault(e=> e.SiteId == id);
                entity.EmailCampaignSites.Remove(item);
            }

            foreach (var id in addLinks) 
            {
                //add in new linked records.
                entity.EmailCampaignSites.Add(new Model.EmailCampaignSite() { 
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
        public static Domain.EmailSettings ToDomainModel(this Model.EmailCampaignSetting entity) 
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
        public static void Update(this Model.EmailCampaignSetting entity, Domain.EmailSettings domainModel)
        {
            entity.UserName = domainModel.UserName;
            entity.SSL = domainModel.SSL;
            entity.Port = domainModel.Port;
            entity.Password = domainModel.Password ?? string.Empty;
            entity.Id = domainModel.Id;
            entity.Host = domainModel.Host ?? string.Empty;;
            entity.FromEmail = domainModel.From;
            entity.ChainId = domainModel.ChainId;
            entity.DropFolder = domainModel.PickupFolder ?? string.Empty;
        }
    }
}
