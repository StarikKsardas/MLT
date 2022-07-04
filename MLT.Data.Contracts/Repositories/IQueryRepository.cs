using MLT.Data.Contracts.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Data.Contracts.Repositories
{
    public interface IQueryRepository
    {
        IEnumerable<Query> GetAllQueries(bool isAcceptor);
    }
}
