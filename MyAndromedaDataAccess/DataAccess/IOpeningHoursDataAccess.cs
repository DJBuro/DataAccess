using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IOpeningHoursDataAccess
    {
        string DeleteById(int siteId, int openingHoursId);
        string DeleteBySiteIdDay(int siteId, string day);
        string Add(int siteId, TimeSpanBlock timeSpanBlock);
    }
}
