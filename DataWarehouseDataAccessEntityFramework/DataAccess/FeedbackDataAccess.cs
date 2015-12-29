using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccessEntityFramework.Model;
using DataWarehouseDataAccess.Domain;
using System.Collections.Generic;
using System.Transactions;

namespace DataWarehouseDataAccessEntityFramework.DataAccess
{
    public class FeedbackDataAccess : IFeedbackDataAccess
    {
        public string ConnectionStringOverride { get; set; }

        public string AddFeedback
        (
            int applicationId,
            DataWarehouseDataAccess.Domain.Feedback feedback,
            string externalSiteId
        )
        {
            using (System.Transactions.TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Suppress))
            {
                using (DataWarehouseEntities dataWarehouseEntities = new DataWarehouseEntities())
                {
                    DataAccessHelper.FixConnectionString(dataWarehouseEntities, this.ConnectionStringOverride);

                    // Create a customer feedback entity
                    Model.CustomerFeedback customerFeedbackEntity = new Model.CustomerFeedback()
                    {
                        Id = Guid.NewGuid(),
                        ACSApplicationId = applicationId,
                        CustomerEmailAddress = feedback.Email,
                        CustomerFeedbackCategoryId = feedback.FeedbackCategory,
                        CustomerName = feedback.Name,
                        DateTimeCreated = DateTime.UtcNow,
                        ExternalSiteId = externalSiteId,
                        Feedback = feedback.FeedbackText
                    };

                    // Add the customer feedback to the database
                    dataWarehouseEntities.CustomerFeedbacks.Add(customerFeedbackEntity);

                    // Commit the customer feedback
                    dataWarehouseEntities.SaveChanges();
                }
            }

            return "";
        }
    }
}
