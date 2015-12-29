using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AndroAdminDataAccess.Domain
{
    public class Address
    {
        public virtual int Id { get; set; }
        public virtual string Org1 { get; set; }
        public virtual string Org2 { get; set; }
        public virtual string Org3 { get; set; }
        public virtual string Prem1 { get; set; }
        public virtual string Prem2 { get; set; }
        public virtual string Prem3 { get; set; }
        public virtual string Prem4 { get; set; }
        public virtual string Prem5 { get; set; }
        public virtual string Prem6 { get; set; }
        public virtual string RoadNum { get; set; }
        public virtual string RoadName { get; set; }
        public virtual string Locality { get; set; }
        public virtual string Town { get; set; }
        public virtual string County { get; set; }
        public virtual string State { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string DPS { get; set; }
        public virtual string Lat { get; set; }
        public virtual string Long { get; set; }
        public virtual Country Country { get; set; }

        public Address()
        {
            this.Id = -1;
            this.Org1 = "";
            this.Org2  = "";
            this.Org3 = "";
            this.Prem1 = "";
            this.Prem2 = "";
            this.Prem3 = "";
            this.Prem4 = "";
            this.Prem5 = "";
            this.Prem6 = "";
            this.RoadNum = "";
            this.RoadName = "";
            this.Locality = "";
            this.Town = "";
            this.County = "";
            this.State = "";
            this.PostCode = "";
            this.DPS = "";
            this.Lat = "";
            this.Long = "";
            this.Country = null;
        }
   }
}