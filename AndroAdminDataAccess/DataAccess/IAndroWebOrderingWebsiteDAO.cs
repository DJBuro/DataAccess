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
        void Add(AndroWebOrderingWebsite webOrderingSite);
        void Update(AndroWebOrderingWebsite webOrderingSite);
    }
}
