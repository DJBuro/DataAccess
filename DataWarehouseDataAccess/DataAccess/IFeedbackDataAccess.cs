using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface IFeedbackDataAccess
    {
        string ConnectionStringOverride { get; set; }

        string AddFeedback
        (
            int applicationId,
            DataWarehouseDataAccess.Domain.Feedback feedback,
            string externalSiteName
        );
    }
}
