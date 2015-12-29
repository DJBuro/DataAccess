using AndroAdminDataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IAndroWebOrderingThemeDAO
    {
        string ConnectionStringOverride { get; set; }
        IList<Domain.AndroWebOrderingTheme> GetAll();
        Domain.AndroWebOrderingTheme GetAndroWebOrderingThemeById(int id);
        void Add(Domain.AndroWebOrderingTheme webOrderingTheme);
        void Update(Domain.AndroWebOrderingTheme webOrderingTheme);
        void Delete(Domain.AndroWebOrderingTheme webOrderingTheme);
    }
}
