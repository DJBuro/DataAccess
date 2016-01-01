using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.Comparer
{
    public class StoreComparer: IEqualityComparer<MyAndromedaDataAccessEntityFramework.Model.Store>
    {
        public bool Equals(
            MyAndromedaDataAccessEntityFramework.Model.Store x, 
            MyAndromedaDataAccessEntityFramework.Model.Store y)
        {
            return x.Id.Equals(y.Id);
        }

        public int GetHashCode(MyAndromedaDataAccessEntityFramework.Model.Store obj)
        {
            return obj.Id.GetHashCode();
        }

    }
}
