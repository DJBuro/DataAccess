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
        List<string> Add(AndroWebOrderingWebsite webOrderingSite);
        List<string> Update(AndroWebOrderingWebsite webOrderingSite);
    }
}
