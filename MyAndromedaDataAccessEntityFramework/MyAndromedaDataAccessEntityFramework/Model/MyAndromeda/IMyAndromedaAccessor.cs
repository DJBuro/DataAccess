using MyAndromeda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.Model.MyAndromeda
{
    public interface IMyAndromedaDbWorkContextAccessor : IDependency
    {
        Model.MyAndromeda.MyAndromedaDbContext DbContext { get; set; }
    }

    public class MyAndromedaDbWorkContexAccessor : IMyAndromedaDbWorkContextAccessor 
    {
        private MyAndromedaDbContext dbContext; 
        public MyAndromedaDbContext DbContext
        {
            get 
            {
                return dbContext ?? (dbContext = new MyAndromedaDbContext());
            }
            set 
            {
                dbContext = value;
            }
        }
    }
}
