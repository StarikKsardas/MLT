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
    public class AnswerMobileRepository : IAnswerMobileRepository
    {
        private readonly MLTDbContext mLTDbContext;

        public AnswerMobileRepository(MLTDbContext mLTDbContext)
        {
            this.mLTDbContext = mLTDbContext;
        }

        public void AddRange(IEnumerable<AnswerMobile> answerMobiles)
        {
            foreach (var current in answerMobiles)
            {
                mLTDbContext.AnswerMobiles.Add(current);
            }
            mLTDbContext.SaveChanges();
        }

        public IEnumerable<AnswerMobile> GetAll()
        {
            var result = mLTDbContext.AnswerMobiles.AsNoTracking().ToList();
            return result;
        }

        public IEnumerable<AnswerMobile> GetById(int id)
        {
            var result = mLTDbContext.AnswerMobiles.Where(p => p.DsId == id);
            return result;
        }

        public void RemoveRange(IEnumerable<int> queryIds)
        {
            var entities = mLTDbContext.AnswerMobiles.Where(p => queryIds.Contains(p.QueryId));
            mLTDbContext.AnswerMobiles.RemoveRange(entities);
            mLTDbContext.SaveChanges();

        }
    }
}
