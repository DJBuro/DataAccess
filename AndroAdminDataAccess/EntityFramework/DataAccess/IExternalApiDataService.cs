using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public interface IExternalApiDataService
    {
        IEnumerable<ExternalApi> List();
        IEnumerable<ExternalApi> List(Expression<Func<ExternalApi, bool>> query);
        IEnumerable<ExternalApi> ListRemoved(Expression<Func<ExternalApi, bool>> query);

        void Update(ExternalApi api);
        void Delete(ExternalApi api);
        void Create(ExternalApi api);

        ExternalApi New();
        ExternalApi Get(Guid id);
    }
}
