using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IEmailCampaignDataAccess 
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

        IEnumerable<EmailCampaign> List();
        IEnumerable<EmailCampaign> List(Expression<Func<EmailCampaign, bool>> query);

        void Destroy(int id);
    }
}