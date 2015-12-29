using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.DataAccess;

namespace AndroCloudDataAccess
{
    public interface IDataAccessFactory
    {
        IMenuDataAccess MenuDataAccess { get; set; }
        ISiteDataAccess SiteDataAccess { get; set; }
        IOrderDataAccess OrderDataAccess { get; set; }
    }
}
