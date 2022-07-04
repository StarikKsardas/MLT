using MLT.Web.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Web.Contracts.WebModels
{
    public class AnswerWeb
    {
        public string RegistrationNumber { get; set; }
        public int LatentNumber { get; set; }
        public bool IsPalm { get; set; }
        public IEnumerable<QueryStatusWeb> Queries { get; set; }
    }
}
