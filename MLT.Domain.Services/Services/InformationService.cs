using AutoMapper;
using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Repositories;
using MLT.Domain.Contracts.InfoModels;
using MLT.Domain.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLT.Domain.Services.Services
{
    public class InformationService : IInformationService
    {
        private readonly IInformationRepository informationRepository;
        private readonly IMapper mapper;

        public InformationService(IInformationRepository informationRepositor, IMapper mapper)
        {
            this.informationRepository = informationRepositor;
            this.mapper = mapper;
        }

        public void DeatachedAll()
        {
            informationRepository.DeatachAllEntities();
        }

        public IEnumerable<string> GetAllAbrPlaces()
        {
            var result = informationRepository.GetAllAbrPlaces();
            return result.Select(x => x.Place).ToList();
        }

        public IEnumerable<AtdInfo> GetAllAtds()
        {
            var atds = informationRepository.GetAllAtds();
            var result = mapper.Map<IEnumerable<Atd>, IEnumerable<AtdInfo>>(atds);
            return result;
        }

        public IEnumerable<CrimeTypeInfo> GetAllCrimeTypes()
        {
            var crimeTypes = informationRepository.GetAllCrimeTypes();
            var result = mapper.Map<IEnumerable<CrimeType>, IEnumerable<CrimeTypeInfo>>(crimeTypes);
            return result;
        }

        public IEnumerable<EntrancePlaceInfo> GetAllEntrancePlace()
        {
            var entrancePlace = informationRepository.GetAllEntrancePlaces();
            var result = mapper.Map<IEnumerable<EntrancePlace>, IEnumerable<EntrancePlaceInfo>>(entrancePlace);
            return result;
        }

        public IEnumerable<EntranceTypeInfo> GetAllEntranceTypes()
        {
            var entranceTypes = informationRepository.GetAllEntranceTypes();
            var result = mapper.Map<IEnumerable<EntranceType>, IEnumerable<EntranceTypeInfo>>(entranceTypes);
            return result;
        }
    }
}
