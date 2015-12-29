using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess.Domain;

namespace DataWarehouseDataAccess.DataAccess
{
    public interface IPasswordResetRequestDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string RequestPasswordReset(string username, int applicationId, out string token, out bool isFacebook);
        string PasswordReset(string username, string token, string newPassword, out Guid? customerId);
    }
}
