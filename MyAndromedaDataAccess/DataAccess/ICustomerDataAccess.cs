using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyAndromedaDataAccess.Domain.Marketing;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface ICustomerDataAccess
    {
        IEnumerable<Customer> ListByChain(int p);

        IEnumerable<Customer> ListBySite(int p1, int p2);
    }
}
