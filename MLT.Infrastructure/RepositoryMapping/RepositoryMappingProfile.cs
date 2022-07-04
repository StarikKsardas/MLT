using AutoMapper;
using MLT.Data.Contracts.Entitys;
using MLT.Domain.Contracts.InfoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Infrastructure.RepositoryMapping
{
    public class RepositoryMappingProfile : Profile
    {
        public RepositoryMappingProfile()
        {
            CreateMap<Atd, AtdInfo>()
                .ForMember(info => info.Code, x => x.MapFrom(ent => ent.Code))
                .ForMember(info => info.Lex, x => x.MapFrom(ent => ent.Lex))
                .ReverseMap()
                .ForAllOtherMembers(info => info.Ignore()); 

            CreateMap<UserInfo, User>()
                .ForMember(ent => ent.AbrPlace, x => x.MapFrom(info => info.AbrPlace))
                .ForMember(ent => ent.FirstName, x => x.MapFrom(info => info.FirstName))
                .ForMember(ent => ent.Id, x => x.MapFrom(info => info.Id))
                .ForMember(ent => ent.LastName, x => x.MapFrom(info => info.LastName))
                .ForMember(ent => ent.Login, x => x.MapFrom(info => info.Login))
                .ForMember(ent => ent.MidName, x => x.MapFrom(info => info.MidName))
                .ForMember(ent => ent.PlaceCodeLex, x => x.MapFrom(info => info.PlaceCodeLex))
                .ForMember(ent => ent.Phone, x => x.MapFrom(info => info.Phone))
                .ForMember(ent => ent.PhoneId, x => x.MapFrom(info => info.PhoneId))
                .ForMember(ent => ent.PlaceCode, x => x.MapFrom(info => long.Parse(info.PlaceCode)))
                .ForMember(ent => ent.RegistrationDate, x => x.MapFrom(info => info.RegistrationDate))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<User, UserInfo>()
                .ForMember(info => info.AbrPlace, x => x.MapFrom(ent => ent.AbrPlace))
                .ForMember(info => info.FirstName, x => x.MapFrom(ent => ent.FirstName))
                .ForMember(info => info.Id, x => x.MapFrom(ent => ent.Id))
                .ForMember(info => info.LastName, x => x.MapFrom(ent => ent.LastName))
                .ForMember(info => info.Login, x => x.MapFrom(ent => ent.Login))
                .ForMember(info => info.MidName, x => x.MapFrom(ent => ent.MidName))
                .ForMember(info => info.PlaceCodeLex, x => x.MapFrom(ent => ent.PlaceCodeLex))
                .ForMember(info => info.Phone, x => x.MapFrom(ent => ent.Phone))
                .ForMember(info => info.PhoneId, x => x.MapFrom(ent => ent.PhoneId))
                .ForMember(info => info.PlaceCode, x => x.MapFrom(ent => ent.PlaceCode.ToString()))
                .ForMember(info => info.RegistrationDate, x => x.MapFrom(ent => ent.RegistrationDate))
                .ForMember(info => info.UpdateDate, x => x.MapFrom(ent => ent.UpdateDate))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<EntrancePlace, EntrancePlaceInfo>()
                .ForMember(info => info.EntrancePlace, x => x.MapFrom(ent => ent.EntrancePlaceLex))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<EntranceType, EntranceTypeInfo>()
                .ForMember(info => info.MainType, x => x.MapFrom(ent => ent.ParentEntranceTypeLex))
                .ForMember(info => info.AdditionalType, x => x.MapFrom(ent => ent.EntranceTypeLex))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<CrimeType, CrimeTypeInfo>()
               .ForMember(info => info.MainType, x => x.MapFrom(ent => ent.ParentCrimeTypeLex))
               .ForMember(info => info.AdditionalType, x => x.MapFrom(ent => ent.CrimeTypeLex))
               .ForAllOtherMembers(info => info.Ignore());

            CreateMap<LatentInfo, Latent>()
                .ForMember(ent => ent.UninvolvedLastname, x => x.MapFrom(info => info.CheckedLastnames))
                .ForMember(ent => ent.CrimeDate, x => x.MapFrom(info => info.CrimeDate))
                .ForMember(ent => ent.CrimePlace, x => x.MapFrom(info => info.CrimePlace))
                .ForMember(ent => ent.UsrNumber, x => x.MapFrom(info => info.DactoMobileUserNumber))
                .ForMember(ent => ent.EditDate, x => x.MapFrom(info => info.EditDate))
                .ForMember(ent => ent.SAbrPlace, x => x.MapFrom(info => info.FirstAbrPlace))
                .ForMember(ent => ent.SEditDate, x => x.MapFrom(info => info.FirstEditDate))
                .ForMember(ent => ent.SDsId, x => x.MapFrom(info => info.FirstId))
                .ForMember(ent => ent.SPlaceCode, x => x.MapFrom(info => Convert.ToInt64(info.FirstPlaceCode)))
                .ForMember(ent => ent.SUsrId, x => x.MapFrom(info => info.FirstUserName))
                .ForMember(ent => ent.Injured, x => x.MapFrom(info => info.InjuredLastnames))
                .ForMember(ent => ent.TakeType, x => x.MapFrom(info => info.LatentMethod))
                .ForMember(ent => ent.NumSl, x => x.MapFrom(info => info.LatentNumber))
                .ForMember(ent => ent.TakePlace, x => x.MapFrom(info => info.LatentPlace))
                .ForMember(ent => ent.Regnum, x => x.MapFrom(info => info.RegistrationNumber))
                .ForMember(ent => ent.UserchangeDate, x => x.MapFrom(info => info.UserchangeDate))
                .ForMember(ent => ent.DsId, x => x.MapFrom(info => info.Id))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<Latent, LatentInfo>()
                .ForMember(info => info.RegistrationNumber, x => x.MapFrom(ent => ent.Regnum))
                .ForMember(info => info.LatentNumber, x => x.MapFrom(ent => ent.NumSl))
                .ForMember(info => info.IsPalm, x => x.MapFrom(ent => ent.ObjectFlag == 1 ? false : true))
                .ForMember(info => info.QueryStatusInfos, x => x.MapFrom(ent => ent.AnswerMobiles))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<AnswerMobile, QueryStatusInfo>()
                .ForMember(info => info.LocalStatus, x => x.MapFrom(ent => ent.LocalStatus))
                .ForMember(info => info.LocalStatusLex, x => x.MapFrom(ent => ent.QueryStatus.StatusLex))
                .ForMember(info => info.QueryId, x => x.MapFrom(ent => ent.QueryId))
                .ForAllOtherMembers(info => info.Ignore());


            CreateMap<AnswerMobile, AnswerMobile>();
            CreateMap<LatentEntrancePlace, LatentEntrancePlace>();
            CreateMap<LatentImage, LatentImage>();
            CreateMap<LatentEntranceType, LatentEntranceType>();
            CreateMap<LatentEntranceType, LatentEntranceType>();
            CreateMap<LatentCrimeType, LatentCrimeType>();       
            CreateMap<Latent, Latent>();


        }
    }

}
