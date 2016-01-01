using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyAndromedaDataAccess.Domain
{
    public class Country
    {
        public virtual int Id { get; set; }
        public virtual string CountryName { get; set; }
        public virtual string ISO3166_1_alpha_2 { get; set; }
        public virtual int ISO3166_1_numeric { get; set; }
    }
}
