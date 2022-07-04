using Microsoft.EntityFrameworkCore;
using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Repositories;
using MLT.Data.Repositories.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLT.Data.Repositories.Repositories
{
    public class QueryRepository : IQueryRepository
    {
        private readonly MLTDbContext mLTDbContext;
        private readonly IServiceProvider serviceProvider;

        public QueryRepository(MLTDbContext mLTDbContext, IServiceProvider serviceProvider)
        {
            this.mLTDbContext = mLTDbContext;
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<Query> GetAllQueries(bool isAcceptor)
        {
            if (!isAcceptor)
            {
                throw new NotImplementedException();
            }
            var dactoDbContext = (DactoDbContext)serviceProvider.GetService(typeof(DactoDbContext));
            var result = dactoDbContext.Queries.Where(p => p.QueryId > 0).AsNoTracking().ToList();
            return result;
        }
    }
}
