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
        IList<AndroWebOrderingWebsite> GetAll();
        AndroWebOrderingWebsite GetAndroWebOrderingWebsiteById(int id);
        int Add(AndroWebOrderingWebsite webOrderingSite);
        int Update(AndroWebOrderingWebsite webOrderingSite);
    }
}
