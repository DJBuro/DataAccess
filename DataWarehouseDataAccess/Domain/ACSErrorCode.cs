using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataWarehouseDataAccess.Domain
{
    public class ACSErrorCode
    {
        public int ErrorCode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}
