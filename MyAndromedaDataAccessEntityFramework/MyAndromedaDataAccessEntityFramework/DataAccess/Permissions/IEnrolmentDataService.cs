using MyAndromeda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromeda.Core.Site;
using System.Linq.Expressions;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Permissions
{
    public interface IEnrolmentDataService : IDependency 
    {
        IEnumerable<IEnrolmentLevel> List();
        IEnumerable<IEnrolmentLevel> Query(Expression<Func<Model.MyAndromeda.EnrolmentLevel, bool>> query);
    }
}
