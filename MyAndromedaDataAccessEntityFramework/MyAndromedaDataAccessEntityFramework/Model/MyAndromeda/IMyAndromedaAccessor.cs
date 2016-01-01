using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromeda.Core;

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
                return this.dbContext ?? (this.dbContext = new MyAndromedaDbContext());
            }
            set 
            {
                this.dbContext = value;
            }
        }
    }
}
