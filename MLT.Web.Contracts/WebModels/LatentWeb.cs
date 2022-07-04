using MLT.Web.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Web.Contracts.WebModels
{
    public class LatentWeb
    {       
        public bool IsPalm { get; set; }
        public string RegistrationNumber { get; set; }
        public int LatentNumber { get; set; }
        public DateTime CrimeDate { get; set; }
        public string CrimePlace { get; set; }
        public string InjuredLastnames { get; set; }        
        public string LatentPlace { get; set; }
        public string LatentMethod { get; set; }
        public string CheckedLastnames { get; set; }
        public string ImageBase64 { get; set; }
        public IEnumerable<SingleClassifierWeb> EntrancePlace { get; set; }
        public IEnumerable<DualClassifierWeb> EntranceType { get; set; }
        public IEnumerable<DualClassifierWeb> CrimeType { get; set; }                
    }
}
