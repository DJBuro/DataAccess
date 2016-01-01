using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.Comparer
{
    public class StoreComparer: IEqualityComparer<Store>
    {
        public bool Equals(
            Store x, 
            Store y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(Store obj)
        {
            return obj.Id.GetHashCode();
        }

    }
}
