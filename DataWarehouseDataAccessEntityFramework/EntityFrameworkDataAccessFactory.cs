using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess;
using DataWarehouseDataAccessEntityFramework.DataAccess;

namespace DataWarehouseDataAccessEntityFramework
{
    public class EntityFrameworkDataAccessFactory : IDataAccessFactory
    {
        public string ConnectionStringOverride { get; set; }

        public DataWarehouseDataAccess.DataAccess.ICustomerDataAccess CustomerDataAccess
        {
            get { return new CustomerDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }
    }
}
