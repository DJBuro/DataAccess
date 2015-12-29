using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDataAccess.Domain
{
    public class Log
    {
        public virtual int Id { get; set; }
        public virtual string StoreId {get; set;}
        public virtual string Severity {get; set;}
        public virtual string Message {get; set;}
        public virtual string Method{get; set;}
        public virtual string Source{get; set;}
        public virtual DateTime Created { get; set; }
    }
}
