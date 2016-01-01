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

        

        

        public Domain.EmailSettings GetEmailSettings(int chainId)
        {
            using (var dbContext = new Model.MyAndromedaEntities())
            {
                var entity = dbContext.EmailCampaignSettings.FirstOrDefault(e=> e.ChainId == chainId);

                if (entity == null)
                    return null;

                return new Domain.EmailSettings() { 
                    Host = entity.Host,
                    Port = entity.Port,
                    Password = entity.Password,
                    UserName = entity.UserName
                };
            } 
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
                    throw new ArgumentException("Id is required");

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

        public IEnumerable<Domain.EmailCampaign> ListByChain(int chainId)
        {
            using(var dbContext = new Model.MyAndromedaEntities())
            {
                var entities = dbContext.EmailCampaigns
                    .Where(e => e.ChainId == chainId)
                    .ToArray()
                    .Select(e=> e.ToDomainModel())
                    .ToList();

                return entities;
            }
        }

        public IEnumerable<Domain.EmailCampaign> ListBySite(int siteId)
        {
            using (var dbContext = new Model.MyAndromedaEntities())
            {
                var entities = dbContext
                    .EmailCampaigns
                    .Where(e => e.EmailCampaignSites.Any(site => site.SiteId == siteId))
                    .ToArray()
                    .Select(e=> e.ToDomainModel())
                    .ToList();

                return entities;
            }
        }

        public IEnumerable<Domain.EmailCampaign> ListByChainAndSite(int chainId, int siteId)
        {
            using (var dbContext = new Model.MyAndromedaEntities())
            {
                var entities = dbContext.EmailCampaigns
                    .Where(e => e.ChainId == chainId)
                    .Where(e => e.EmailCampaignSites.Any(availableInSite => availableInSite.SiteId == siteId))
                    .ToArray()
                    .Select(e => e.ToDomainModel())
                    .ToList();

                return entities;
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
            this.Ensure(campaign);

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
            this.Ensure(campaign);

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


        private bool Ensure(Domain.EmailCampaign campaign) 
        {
            if (campaign.ChainId == 0)
                throw new ArgumentException("Chain Id is required");
            //if (campaign.SiteId == 0)
            //    throw new ArgumentException("Site Id is required");

            return true;
        }

        
    }
}