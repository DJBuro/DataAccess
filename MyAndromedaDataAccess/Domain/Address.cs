using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyAndromedaDataAccess.Domain
{
    public class Address
    {
        public string Long { get; set;}

        public string Lat { get; set;}

        [StringLength(64, ErrorMessage = "Prem1 cannot be longer than 64 characters.")]
        public string Prem1 { get; set;}

        [StringLength(64, ErrorMessage = "Prem2 cannot be longer than 64 characters.")]
        public string Prem2 { get; set;}

        [StringLength(64, ErrorMessage = "Prem3 cannot be longer than 64 characters.")]
        public string Prem3 { get; set; }

        [StringLength(64, ErrorMessage = "Prem4 cannot be longer than 64 characters.")]
        public string Prem4 { get; set; }

        [StringLength(64, ErrorMessage = "Prem5 cannot be longer than 64 characters.")]
        public string Prem5 { get; set; }

        [StringLength(64, ErrorMessage = "Prem6 cannot be longer than 64 characters.")]
        public string Prem6 { get; set; }

        [StringLength(64, ErrorMessage = "Org1 cannot be longer than 64 characters.")]
        public string Org1 { get; set;}

        [StringLength(64, ErrorMessage = "Org2 cannot be longer than 64 characters.")]
        public string Org2 { get; set;}

        [StringLength(64, ErrorMessage = "Org3 cannot be longer than 64 characters.")]
        public string Org3 { get; set; }

        [StringLength(16, ErrorMessage = "RoadNum cannot be longer than 16 characters.")]

        public string RoadNum { get; set;}

        [StringLength(64, ErrorMessage = "RoadName cannot be longer than 64 characters.")]
        public string RoadName { get; set;}

        [Required(ErrorMessage = "Please enter the town")]
        [StringLength(64, ErrorMessage = "Town cannot be longer than 64 characters.")]
        public string Town { get; set;}

        [Required(ErrorMessage = "Please enter the postcode")]
        [StringLength(16, ErrorMessage = "Postcode cannot be longer than 16 characters.")]
        public string Postcode { get; set;}

        [StringLength(4, ErrorMessage = "Dps cannot be longer than 4 characters.")]
        public string Dps { get; set;}

        [StringLength(64, ErrorMessage = "County cannot be longer than 64 characters.")]
        public string County { get; set;}

        [StringLength(64, ErrorMessage = "State cannot be longer than 64 characters.")]
        public string State { get; set; }

        [StringLength(64, ErrorMessage = "Locality cannot be longer than 64 characters.")]
        public string Locality { get; set;}

        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
