using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Domain = MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework
{
    public static class DomainModelUpdateExtensions
    {
        public static Domain.EmailCampaign ToDomainModel(this Model.EmailCampaign entity)
        {
            var model = new MyAndromedaDataAccess.Domain.Marketing.EmailCampaign()
            {
                Id = entity.Id,
                Title = entity.Title,
                EmailTemplate = entity.EmailTemplate,
                Created = entity.Created,
                Modified = entity.Modified,
                SiteId = entity.SiteId
            };

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
            entity.Title = domainModel.Title;
            entity.EmailTemplate = domainModel.EmailTemplate;
            entity.SiteId = domainModel.SiteId;
        }
    }
}
