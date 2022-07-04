using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Contracts.InfoModels
{
    public class QueryStatusInfo
    {
        public QueryStatusInfo(int queryId, int localStatus)
        {
            this.QueryId = queryId;
            this.LocalStatus = localStatus;
        }
        public int QueryId { get; set; }
        public int LocalStatus { get; set; }
        public string LocalStatusLex { get; set; }

        public bool Equals(QueryStatusInfo other)
        {
            return (QueryId == other.QueryId) && (LocalStatus == other.LocalStatus);
        }

        public override bool Equals(object obj) => Equals(obj as QueryStatusInfo);
        public override int GetHashCode() => (QueryId, LocalStatus).GetHashCode();
    }
}
