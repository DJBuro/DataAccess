using AndroAdminDataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IACSApplicationEFDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<ACSApplicationEF> GetAll();
    }
}
