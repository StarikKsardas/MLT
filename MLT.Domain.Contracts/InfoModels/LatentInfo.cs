using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Contracts.InfoModels
{
    public class LatentInfo
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public int LatentNumber { get; set; }
        public DateTime CrimeDate { get; set; }
        public string CrimePlace { get; set; }
        public string InjuredLastnames { get; set; }
        public string LatentPlace { get; set; }
        public string LatentMethod { get; set; }
        public string CheckedLastnames { get; set; }
        public bool IsPalm { get; set; }

        public IEnumerable<string> EntrancePlace { get; set; }
        public IEnumerable<string> EntranceType { get; set; }
        public IEnumerable<string> CrimeType { get; set; }
        public IEnumerable<QueryStatusInfo> QueryStatusInfos { get; set; }

        public int FirstId { get; set; }
        public string FirstUserName { get; set; }
        public string FirstPlaceCode { get; set; }
        public string FirstAbrPlace { get; set; }
        public DateTime FirstEditDate { get; set; }

        public DateTime EditDate { get; set; }
        public DateTime UserchangeDate { get; set; }        
        public int DactoMobileUserNumber { get; set; }

        public byte[] WsqImage { get; set; }        
    }
}
