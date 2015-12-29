using System;
using MyAndromedaDataAccess.DataAccess;
using MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class EmailCampaignDataAccess : IEmailCampaignDataAccess 
    {
        public EmailCampaignDataAccess()
        {
        }

        public EmailCampaign Get(int id)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
            {
                var entity = dbContext.EmailCampaigns.Find(id);

                if (entity == null)
                    return null;

                return entity.ToDomainModel();
            }
        }

        public void Save(EmailCampaign campaign)
        {
            if (campaign.Id == 0)
                Create(campaign);
            else
                Update(campaign);
        }

        private void Create(EmailCampaign campaign) 
        {
            using (var dbContext = new Model.MyAndromedaEntities())
            {
                var entity = dbContext.EmailCampaigns.Create();//new Model.EmailCampaign();
                entity.Update(campaign);

                dbContext.SaveChanges();
            }
        }

        private void Update(EmailCampaign campaign) 
        {
            using (var dbContext = new Model.MyAndromedaEntities())
            {
                var entity = dbContext.EmailCampaigns.Find(campaign.Id);//new Model.EmailCampaign();
                entity.Update(campaign);

                dbContext.SaveChanges();
            }
        }
    }
}