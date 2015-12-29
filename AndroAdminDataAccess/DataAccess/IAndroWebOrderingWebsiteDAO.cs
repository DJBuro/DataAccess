using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IAndroWebOrderingWebsiteDAO
    {
        string ConnectionStringOverride { get; set; }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<AndroWebOrderingWebsite> GetAll();

        /// <summary>
        /// Gets the andro web ordering website by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        AndroWebOrderingWebsite GetAndroWebOrderingWebsiteById(int id);

        /// <summary>
        /// Adds the specified web ordering site.
        /// </summary>
        /// <param name="webOrderingSite">The web ordering site.</param>
        /// <returns></returns>
        List<string> Add(AndroWebOrderingWebsite webOrderingSite);

        /// <summary>
        /// Updates the specified web ordering site.
        /// </summary>
        /// <param name="webOdrderingSite">The web ordering site.</param>
        /// <returns></returns>
        List<string> Update(AndroWebOrderingWebsite webOdrderingSite);
    }

    public interface IAndroWebOrderingSubscriptionDAO 
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IList<AndroWebOrderingSubscriptionType> GetAll();


    }
}
