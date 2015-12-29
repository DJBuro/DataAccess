using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardDataAccess.Domain
{
    public class HistoricalData
    {
        public virtual int Id { get; set; }
        public virtual int SiteId { get; set; }
        public virtual string Name { get; set; }
        public virtual int HeadOfficeId { get; set; }
        public virtual DateTime LastUpdated { get; set; }
        public virtual int Column_1 { get; set; }
        public virtual int Column_2 { get; set; }
        public virtual int Column_3 { get; set; }
        public virtual int Column_4 { get; set; }
        public virtual int Column_5 { get; set; }
        public virtual int Column_6 { get; set; }
        public virtual int Column_7 { get; set; }
        public virtual int Column_8 { get; set; }
        public virtual int Column_9 { get; set; }
        public virtual int Column_10 { get; set; }
        public virtual int Column_11 { get; set; }
        public virtual int Column_12 { get; set; }
        public virtual int Column_13 { get; set; }
        public virtual int Column_14 { get; set; }
        public virtual int Column_15 { get; set; }
        public virtual int Column_16 { get; set; }
        public virtual int Column_17 { get; set; }
        public virtual int Column_18 { get; set; }
        public virtual int Column_19 { get; set; }
        public virtual int Column_20 { get; set; }
        public virtual DateTime? TradingDay { get; set; }
    }
}
