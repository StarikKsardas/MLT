using MLT.Data.Contracts.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Data.Contracts.Repositories
{
    public interface IAnswerMobileRepository
    {
        IEnumerable<AnswerMobile> GetById(int id);
        void AddRange(IEnumerable<AnswerMobile> answerMobiles);
        IEnumerable<AnswerMobile> GetAll();
        void RemoveRange(IEnumerable<int> queryIds);
    }
}
