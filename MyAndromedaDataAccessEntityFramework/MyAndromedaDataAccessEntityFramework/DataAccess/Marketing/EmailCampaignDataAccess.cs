using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyAndromedaDataAccess.DataAccess.MyAndromeda.Email;
using Domain = MyAndromedaDataAccess.Domain.Marketing;
using MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Marketing
{
    public class EmailCampaignDataAccess : IEmailCampaignDataAccess 
    {
        public EmailCampaignDataAccess()
        {
        }

        /// <summary>
        /// Gets or sets the on validation errors.
        /// </summary>
        /// <value>The on validation errors.</value>
        public Action<IEnumerable<string>> OnValidationErrors { get; set; }

        /// <summary>
        /// Creates the email settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void CreateEmailSettings(EmailSettings settings)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var dataModel = dbContext.EmailCampaignSettings.Create();
                dataModel.Update(settings);

                dbContext.EmailCampaignSettings.Add(dataModel);
                dbContext.SaveChanges();

                settings.Id = dataModel.Id;
            }
        }

        /// <summary>
        /// Gets the email settings by site id.
        /// </summary>
        /// <param name="siteId">The site id.</param>
        /// <returns></returns>
        public EmailSettings GetEmailSettingsBySiteId(int siteId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the email settings by chain id.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        public EmailSettings GetEmailSettingsByChainId(int chainId)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var dataModel = dbContext.EmailCampaignSettings.FirstOrDefault(e => e.ChainId == chainId);

                if (dataModel == null)
                    return null;

                return dataModel.ToDomainModel();
            }
        }

        /// <summary>
        /// Destroys the email settings.
        /// </summary>
        /// <param name="id">The id.</param>
        public void DestroyEmailSettings(int id)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var dataModel = dbContext.EmailCampaignSettings.Find(id);

                if (dataModel == null)
                    return;

                dbContext.EmailCampaignSettings.Remove(dataModel);
                dbContext.SaveChanges();
            }
        }

        public void SaveSettings(EmailSettings settings)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var entity = dbContext.EmailCampaignSettings.FirstOrDefault(e => e.Id == settings.Id);
                entity.Host = settings.Host;
                entity.ChainId = settings.ChainId;
                entity.Password = settings.Password;
                entity.UserName = settings.UserName;
                entity.Port = settings.Port;

                dbContext.SaveChanges();
            }
        }

        public Domain.EmailSettings GetEmailSettings(int chainId)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var entity = dbContext.EmailCampaignSettings.FirstOrDefault(e=> e.ChainId == chainId);

                if (entity == null)
                    return null;

                return new Domain.EmailSettings() { 
                    Id = entity.Id,
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
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
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
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
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
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var dbQuery = ExpressionRewriter.CastParam<Domain.EmailCampaign, Model.MyAndromeda.EmailCampaign>(query);

                var entities = dbContext.EmailCampaigns.Where(dbQuery);

                return entities.ToList().Select(e => e.ToDomainModel()).ToList();
            }
        }

        public IEnumerable<Domain.EmailCampaign> ListByChain(int chainId)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var entities = dbContext.EmailCampaigns
                    .Where(e => e.ChainId == chainId)
                    .Where(e => !e.Removed)
                    .ToArray()
                    .Select(e=> e.ToDomainModel())
                    .ToList();

                return entities;
            }
        }

        public IEnumerable<Domain.EmailCampaign> ListBySite(int siteId)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var entities = dbContext
                    .EmailCampaigns
                    .Where(e=> !e.Removed)
                    .Where(e => e.EmailCampaignSites.Any(site => site.SiteId == siteId))
                    .ToArray()
                    .Select(e=> e.ToDomainModel())
                    .ToList();

                return entities;
            }
        }

        public IEnumerable<Domain.EmailCampaign> ListByChainAndSite(int chainId, int siteId)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var entities = dbContext.EmailCampaigns
                    .Where(e => !e.Removed)
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
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
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

            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var entity = dbContext.EmailCampaigns.Create();//new Model.EmailCampaign();
                entity.Update(campaign);
                    
                dbContext.EmailCampaigns.Add(entity);
                
                var validate = dbContext.GetValidationErrors().ToArray();

                if(validate.Length > 0)
                {
                    if(this.OnValidationErrors != null)
                        this.OnValidationErrors(validate.Where(e=> !e.IsValid).SelectMany(e=> e.ValidationErrors.Select(v=> v.ErrorMessage)));
                }else
                {
                    dbContext.SaveChanges();
                }


                //try
                //{
                //    dbContext.SaveChanges();
                //}
                //catch (Exception e) 
                //{ 
                //    var validationErrors = dbContext.GetValidationErrors().Select(e=> e.v.ToArray();
                //    if (validationErrors. > 0) {
                //        this.OnValidationErrors(validationErrors);
                //    }
                //}
            }
        }

        /// <summary>
        /// Updates the specified campaign.
        /// </summary>
        /// <param name="campaign">The campaign.</param>
        private void Update(Domain.EmailCampaign campaign) 
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var entity = dbContext.EmailCampaigns.Find(campaign.Id);//new Model.EmailCampaign();
                entity.Update(campaign);

                dbContext.SaveChanges();
            }
        }

        private bool Ensure(Domain.EmailCampaign campaign) 
        {
            //may have settings that are either chain only or site specific

            //if (campaign.ChainId == 0)
            //    throw new ArgumentException("Chain Id is required");
            //if (campaign.SiteId == 0)
            //    throw new ArgumentException("Site Id is required");

            return true;
        }

        
    }
}