using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface ILogDAO
    {
        IEnumerable<Log> GetAll();
        void Add(Log amsServer);
    }
}