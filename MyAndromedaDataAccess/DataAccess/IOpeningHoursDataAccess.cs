using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain;
using MyAndromeda.Core;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IOpeningHoursDataAccess : IDependency
    {

        IEnumerable<TimeSpanBlock> ListBySiteId(int siteId);

        string DeleteById(int siteId, int openingHoursId);

        string DeleteBySiteIdDay(int siteId, string day);
        
        string Add(int siteId, TimeSpanBlock timeSpanBlock);
    }
}
