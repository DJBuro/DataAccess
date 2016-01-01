using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IChainDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<Chain> GetAll();
        Chain GetChainById(int id);
        int Save(Domain.Chain chain);
    }
}