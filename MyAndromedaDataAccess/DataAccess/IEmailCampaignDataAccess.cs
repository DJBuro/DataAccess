using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IEmailCampaignDataAccess : IDataAccessOptions
    { 
        /// <summary>
        /// Gets the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        EmailCampaign Get(int id);

        /// <summary>
        /// Saves the specified campaign.
        /// </summary>
        /// <param name="campaign">The campaign.</param>
        void Save(EmailCampaign campaign);

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmailCampaign> List();

        /// <summary>
        /// Lists the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<EmailCampaign> List(Expression<Func<EmailCampaign, bool>> query);

        IEnumerable<EmailCampaign> ListByChain(int chainId);
        IEnumerable<EmailCampaign> ListByChainAndSite(int chainId, int siteId);
        IEnumerable<EmailCampaign> ListBySite(int siteId);

        void Destroy(int id);

        /// <summary>
        /// Gets the email settings.
        /// </summary>
        /// <param name="chainId">The chain id.</param>
        /// <returns></returns>
        EmailSettings GetEmailSettings(int id);
        EmailSettings GetEmailSettingsBySiteId(int siteId);
        EmailSettings GetEmailSettingsByChainId(int chainId);

        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        void SaveSettings(EmailSettings settings);

        void DestroyEmailSettings(int id);

        void CreateEmailSettings(EmailSettings settings);
    }
}