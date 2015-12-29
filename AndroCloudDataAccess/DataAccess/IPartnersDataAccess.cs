using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccess.DataAccess
{
    public interface IPartnersDataAccess
    {
        string ConnectionStringOverride { get; set; }
        string GetById(int id, out Partner partner);
    }
}
