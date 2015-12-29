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
        IList<Domain.ThemeAndroWebOrdering> GetAll();
        Domain.ThemeAndroWebOrdering GetAndroWebOrderingThemeById(int id);
        void Add(Domain.ThemeAndroWebOrdering webOrderingTheme);
        void Update(Domain.ThemeAndroWebOrdering webOrderingTheme);
        void Delete(Domain.ThemeAndroWebOrdering webOrderingTheme);
    }
}
