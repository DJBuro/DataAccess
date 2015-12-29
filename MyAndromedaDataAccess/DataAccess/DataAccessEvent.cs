using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccess.DataAccess
{
    public interface IDataAccessOptions
    {
        Action<IEnumerable<string>> OnValidationErrors { get; set; }
    }
}
