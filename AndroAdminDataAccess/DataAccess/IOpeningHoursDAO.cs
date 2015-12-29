using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IOpeningHoursDAO
    {
        string ConnectionStringOverride { get; set; }
        string DeleteById(int siteId, int openingHoursId);
        string DeleteBySiteIdDay(int siteId, string day);
        string Add(int siteId, TimeSpanBlock timeSpanBlock);
    }
}