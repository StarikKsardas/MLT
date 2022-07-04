using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MLT.Data.Contracts.Helpers
{
  
    public class LatentUnique
    {
        public LatentUnique(int dsId, int sDsId, string sUsrId, long sPlaceCode, string sAbrPlace, DateTime sEditDate)
        {
            this.DsId = dsId;
            this.SAbrPlace = sAbrPlace;
            this.SDsId = sDsId;
            this.SEditDate = sEditDate;
            this.SPlaceCode = sPlaceCode;
            this.SUsrId = sUsrId;                
        }

        public int DsId { get; private set; }
        public int SDsId { get; private set; }
        public string SUsrId { get; private set; }
        public long SPlaceCode { get; private set; }
        public string SAbrPlace { get; private set; }
        public DateTime SEditDate { get; private set; }
    }
}
