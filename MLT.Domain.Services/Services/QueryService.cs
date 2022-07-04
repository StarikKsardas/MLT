using AutoMapper;
using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Helpers;
using MLT.Data.Contracts.Repositories;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using MLT.Domain.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLT.Domain.Services.Services
{
    public class QueryService : IQueryService
    {
        private readonly IQueryRepository queryRepository;
        private readonly IAnswerMobileRepository answerMobileRepository;
        private readonly ILatentRepository latentRepository;

        public IEnumerable<int> IdsToAdd { get; private set; }
        public IEnumerable<int> IdsToDelete { get; private set; }

        private IEnumerable<Query> fullQueries;
        private IEnumerable<LatentUnique> fullUniqueLatents;    

        public QueryService(IQueryRepository queryRepository, IAnswerMobileRepository answerMobileRepository,
            ILatentRepository latentRepository)
        {
            this.queryRepository = queryRepository;
            this.answerMobileRepository = answerMobileRepository;
            this.latentRepository = latentRepository;         
        }

        public void DeleteDifference()
        {
            if (IdsToDelete.Count() > 0)
            {
                answerMobileRepository.RemoveRange(IdsToDelete);
            }
        }
        
        public void AddDifference()
        {
            var workQuery = fullQueries.Where(p => IdsToAdd.Contains(p.QueryId)).ToList();
            var answersMobile = new List<AnswerMobile>();
            foreach (var current in workQuery)
            {
                var answerMobile = new AnswerMobile
                {
                    LocalStatus = current.LocalStatus,
                    QueryId = current.QueryId
                };
                answerMobile.DsId = fullUniqueLatents.FirstOrDefault(p => p.SAbrPlace == current.SAbrPlace && p.SDsId == current.SDsId &&
                    p.SEditDate == current.SEditDate && p.SPlaceCode == current.SPlaceCode && p.SUsrId == current.SUsrId).DsId;
                answersMobile.Add(answerMobile);
            }
            answerMobileRepository.AddRange(answersMobile);
        }

        public bool CalculateChanges()
        {
            IdsToAdd = null;
            IdsToDelete = null;
            fullQueries = null;
            fullUniqueLatents = null;

            var queries = queryRepository.GetAllQueries(true).ToList();
            var latents = latentRepository.GetUniqueIdentifiers(false).ToList();
            var answers = answerMobileRepository.GetAll().Select(p => new QueryStatusInfo(p.QueryId, p.LocalStatus)).ToList();           
            var sLatents = latents.Select(p => $"{p.SAbrPlace}_{p.SDsId}_{p.SEditDate}_{p.SPlaceCode}_{p.SUsrId}").ToList();
            var syncQueries = queries.Where(p => sLatents.Contains($"{p.SAbrPlace}_{p.SDsId}_{p.SEditDate}_{p.SPlaceCode}_{p.SUsrId}"))
                .Select(p => new QueryStatusInfo(p.QueryId, p.LocalStatus)).ToList();
            var differentToAdd = syncQueries.Except(answers).Select(p => p.QueryId).ToList();
            var differentToDelete = answers.Except(syncQueries).ToList();

            IdsToAdd = differentToAdd;
            IdsToDelete = differentToDelete.Select(p => p.QueryId).ToList();
            fullQueries = queries;
            fullUniqueLatents = latents;

            var result = false;
            if ((differentToAdd.Count > 0) || (differentToDelete.Count > 0))
            {
                result = true;
            }
            return result;
                    
        }
    }
}
