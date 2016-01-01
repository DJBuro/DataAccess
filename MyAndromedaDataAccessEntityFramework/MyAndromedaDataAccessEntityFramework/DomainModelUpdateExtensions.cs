using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccessEntityFramework
{
    public static class DomainModelUpdateExtensions
    {
        public static MyAndromedaDataAccess.Domain.Marketing.EmailCampaign ToDomainModel(this Model.EmailCampaign entity)
        {
            var model = new MyAndromedaDataAccess.Domain.Marketing.EmailCampaign()
            {
                Id = entity.Id,
                Title = entity.Title,
                EmailTemplate = entity.EmailTemplate,
                Created = entity.Created,
                Modified = entity.Modified
            };

            return model;
        }

        public static void Update(this Model.EmailCampaign entity, MyAndromedaDataAccess.Domain.Marketing.EmailCampaign domainModel)
        {
            entity.Modified = DateTime.Now;
            entity.Title = domainModel.Title;
            entity.EmailTemplate = domainModel.EmailTemplate;
        }
    }
}
