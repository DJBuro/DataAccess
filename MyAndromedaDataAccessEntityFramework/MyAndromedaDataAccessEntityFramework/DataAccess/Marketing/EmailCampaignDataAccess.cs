using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyAndromedaDataAccess.DataAccess;
using Domain =  MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class EmailCampaignDataAccess : IEmailCampaignDataAccess 
    {
        public EmailCampaignDataAccess()
        {
        }

        /// <summary>
        /// Destroys the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Destroy(int id)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
            {
                var entity = dbContext.EmailCampaigns.Find(id);

                if (entity == null)
                    throw new ArgumentException("Id doesnt exist");

                entity.Removed = true;
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Domain.EmailCampaign> List()
        {
            using (var dbContext = new Model.MyAndromedaEntities())
            {
                var entities = dbContext.EmailCampaigns;

                return entities
                    .Where(e=> !e.Removed)
                    .Select(e => e.ToDomainModel());
            }
        }

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query. Domain and db model must be equal</param>
        /// <returns></returns>
        public IEnumerable<Domain.EmailCampaign> List(Expression<Func<Domain.EmailCampaign, bool>> query)
        {
            using(var dbContext = new Model.MyAndromedaEntities())
            {
                var dbQuery = ExpressionRewriter.CastParam<Domain.EmailCampaign, Model.EmailCampaign>(query);

                var entities = dbContext.EmailCampaigns.Where(dbQuery);

                return entities.ToList().Select(e => e.ToDomainModel()).ToList();
            }
        }

        /// <summary>
        /// Gets the specified EmailCampaign by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public Domain.EmailCampaign Get(int id)
        {
            using (var dbContext = new Model.MyAndromedaEntities()) 
            {
                var entity = dbContext.EmailCampaigns.Find(id);

                if (entity == null)
                    return null;

                return entity.ToDomainModel();
            }
        }

        /// <summary>
        /// Saves the specified campaign.
        /// </summary>
        /// <param name="campaign">The campaign.</param>
        public void Save(Domain.EmailCampaign campaign)
        {
            if (campaign.Id == 0)
                Create(campaign);
            else
                Update(campaign);
        }

        /// <summary>
        /// Creates the specified campaign.
        /// </summary>
        /// <param name="campaign">The campaign.</param>
        private void Create(Domain.EmailCampaign campaign) 
        {
            using (var dbContext = new Model.MyAndromedaEntities())
            {
                var entity = dbContext.EmailCampaigns.Create();//new Model.EmailCampaign();
                entity.Update(campaign);
                
                dbContext.EmailCampaigns.Add(entity);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the specified campaign.
        /// </summary>
        /// <param name="campaign">The campaign.</param>
        private void Update(Domain.EmailCampaign campaign) 
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