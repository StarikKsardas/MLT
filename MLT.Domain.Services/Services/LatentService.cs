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
    public class LatentService : ILatentService
    {
        private readonly ILatentRepository latentRepository;
        private readonly IMapper mapper;
        private readonly IInformationRepository informationRepository;

        public LatentService(IMapper mapper, ILatentRepository latentRepository, IInformationRepository informationRepository)
        {
            this.latentRepository = latentRepository;
            this.mapper = mapper;
            this.informationRepository = informationRepository;
        }

        public void Add(LatentInfo latentInfo)
        {
            var currentDateTime = DateTime.Now;
            latentInfo.EditDate = currentDateTime;
            latentInfo.FirstEditDate = currentDateTime;
            latentInfo.UserchangeDate = currentDateTime;
            latentInfo.DactoMobileUserNumber = informationRepository.GetDactoMobileUserNumber();
            var sequanceValue = informationRepository.GetSequanceNextVal("DACTOCREATOR.DACTO_SL_SQ");
            latentInfo.FirstId = sequanceValue;
            latentInfo.Id = sequanceValue;

            var latent = mapper.Map<LatentInfo, Latent>(latentInfo);
            if (latentInfo.IsPalm)
            {
                latent.ObjectFlag = 2;
            }
            else
            {
                latent.ObjectFlag = 1;
            }

            latent.LatentImages = new List<LatentImage>();
            latent.LatentImages.Add(new LatentImage
            {
                Checksum = 0,
                DsId = latentInfo.FirstId,
                ImageId = (byte)latentInfo.LatentNumber,
                Image = latentInfo.WsqImage
            });

            latent.LatentEntrancePlaces = new List<LatentEntrancePlace>();
            byte current = 1;
            foreach (var latentEntrancePlace in latentInfo.EntrancePlace)
            {
                latent.LatentEntrancePlaces.Add(new LatentEntrancePlace
                {
                    DsId = latentInfo.FirstId,
                    EntrancePlaceId = current,
                    Text = latentEntrancePlace
                });
                current++;
            }

            latent.LatentCrimeTypes = new List<LatentCrimeType>();
            current = 1;
            foreach (var latentCrimeType in latentInfo.CrimeType)
            {
                latent.LatentCrimeTypes.Add(new LatentCrimeType
                {
                    DsId = latentInfo.FirstId,
                    CrimeTypeId = current,
                    Text1 = latentCrimeType
                });
                current++;
            }

            latent.LatentEntranceTypes = new List<LatentEntranceType>();
            current = 1;
            foreach (var latentEntranceType in latentInfo.EntranceType)
            {
                latent.LatentEntranceTypes.Add(new LatentEntranceType
                {
                    DsId = latentInfo.FirstId,
                    EntranceTypeId = current,
                    Text1 = latentEntranceType
                });
                current++;
            }

            latentRepository.Add(latent);
        }

        public IEnumerable<LatentInfo> GetAllUserLatents(UserInfo userInfo)
        {
            var latents = latentRepository.GetAllByUserNameAndId(userInfo.Login, userInfo.Id);
            var result = mapper.Map<IEnumerable<Latent>, IEnumerable<LatentInfo>>(latents);
            return result;
        }

        public IEnumerable<int> GetDifferenceIds()
        {
            var acceptorList = latentRepository.GetUniqueIdentifiers(true);
            var donorList = latentRepository.GetUniqueIdentifiers(false);            
            var sDonorList = donorList.Select(p => $"{p.SAbrPlace}_{p.SDsId}_{p.SEditDate}_{p.SPlaceCode}_{p.SUsrId}").ToList();
            var sAcceptorList = acceptorList.Select(p => $"{p.SAbrPlace}_{p.SDsId}_{p.SEditDate}_{p.SPlaceCode}_{p.SUsrId}").ToList();
            var differenceList = sDonorList.Except(sAcceptorList).ToList();
            var result = donorList.Where(p => differenceList.Contains($"{p.SAbrPlace}_{p.SDsId}_{p.SEditDate}_{p.SPlaceCode}_{p.SUsrId}"))
                .Select(p => p.DsId);
            return result;
        }

        public void SyncLatent(IEnumerable<int> latentIds)
        {
            foreach (var id in latentIds)
            {
                var tempLatent = latentRepository.GetById(id);
                var currentLatent = mapper.Map<Latent, Latent>(tempLatent);

                var sequanceValue = informationRepository.GetSequanceNextVal("DACTOCREATOR.DACTO_SL_SQ", true);

                foreach (var current in currentLatent.LatentCrimeTypes)
                {
                    current.DsId = sequanceValue;
                }
                foreach (var current in currentLatent.LatentImages)
                {
                    current.DsId = sequanceValue;
                }
                foreach (var current in currentLatent.LatentEntrancePlaces)
                {
                    current.DsId = sequanceValue;
                }
                foreach (var current in currentLatent.LatentEntranceTypes)
                {
                    current.DsId = sequanceValue;
                }
                currentLatent.DsId = sequanceValue;
                currentLatent.UsrNumber = informationRepository.GetDactoMobileUserNumber(true);
                currentLatent.EditDate = DateTime.Now;              
                latentRepository.Add(currentLatent, true);
                
            }
        }


    }
}
