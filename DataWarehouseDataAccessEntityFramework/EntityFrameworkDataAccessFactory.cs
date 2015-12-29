using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataWarehouseDataAccess;
using DataWarehouseDataAccess.DataAccess;
using DataWarehouseDataAccessEntityFramework.DataAccess;

namespace DataWarehouseDataAccessEntityFramework
{
    public class EntityFrameworkDataAccessFactory : IDataAccessFactory
    {
        public string ConnectionStringOverride { get; set; }

        public ICustomerDataAccess CustomerDataAccess
        {
            get { return new CustomerDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public IPasswordResetRequestDataAccess PasswordResetRequestDataAccess
        {
            get { return new PasswordResetRequestDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }

        public IOrderDataAccess OrderDataAccess
        {
            get { return new OrderDataAccess() { ConnectionStringOverride = this.ConnectionStringOverride }; }
            set { throw new NotImplementedException(); }
        }
    }
}
