using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IAddressDAO
    {
        string ConnectionStringOverride { get; set; }

        int Add(Address address);
    }
}
