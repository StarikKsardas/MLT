using AutoMapper;
using MLT.Domain.Contracts.InfoModels;
using MLT.Web.Contracts.Helpers;
using MLT.Web.Contracts.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MLT.Infrastructure.ServiceMapping
{
    public class ServiceToWebMappingProfile: Profile
    {
        public ServiceToWebMappingProfile()
        {
            CreateMap<UserWeb, UserInfo>()
                .ForMember(info => info.Login, x => x.MapFrom(view => view.Login))
                .ForMember(info => info.Password, x => x.MapFrom(view => view.PasswordEncrypt))
                .ForMember(info => info.PhoneId, x => x.MapFrom(view => view.PhoneId))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<UserChangePasswordWeb, UserInfo>()
                .ForMember(info => info.Login, x => x.MapFrom(view => view.Login))
                .ForMember(info => info.Password, x => x.MapFrom(view => view.OldPasswordEncrypt))
                .ForMember(info => info.PhoneId, x => x.MapFrom(view => view.PhoneId))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<SingleClassifierWeb, EntrancePlaceInfo>()
                .ForMember(info => info.EntrancePlace, x => x.MapFrom(view => view.Lex))
                .ReverseMap()
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<DualClassifierWeb, EntranceTypeInfo>()
                .ForMember(info => info.MainType, x => x.MapFrom(view => view.Lex))
                .ForMember(info => info.AdditionalType, x => x.MapFrom(view => view.SecondLex))
                .ReverseMap()
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<DualClassifierWeb, CrimeTypeInfo>()
                .ForMember(info => info.MainType, x => x.MapFrom(view => view.Lex))
                .ForMember(info => info.AdditionalType, x => x.MapFrom(view => view.SecondLex))
                .ReverseMap()
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<LatentWeb, LatentInfo>()
                .ForMember(info => info.IsPalm, x => x.MapFrom(view => view.IsPalm))
                .ForMember(info => info.CheckedLastnames, x => x.MapFrom(view => view.CheckedLastnames))
                .ForMember(info => info.CrimeDate, x => x.MapFrom(view => view.CrimeDate))
                .ForMember(info => info.CrimePlace, x => x.MapFrom(view => view.CrimePlace))
                .ForMember(info => info.InjuredLastnames, x => x.MapFrom(view => view.InjuredLastnames))
                .ForMember(info => info.LatentMethod, x => x.MapFrom(view => view.LatentMethod))
                .ForMember(info => info.LatentNumber, x => x.MapFrom(view => view.LatentNumber))
                .ForMember(info => info.LatentPlace, x => x.MapFrom(view => view.LatentPlace))
                .ForMember(info => info.RegistrationNumber, x => x.MapFrom(view => view.RegistrationNumber))
                .ForMember(info => info.EntrancePlace, x => x.MapFrom(view => view.EntrancePlace.Select(p => p.Lex)))
                .ForMember(info => info.EntranceType, x => x.MapFrom(view => view.EntranceType.Select(p => p.Lex)))
                .ForMember(info => info.CrimeType, x => x.MapFrom(view => view.CrimeType.Select(p => p.Lex)))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<LatentInfo, AnswerWeb>()
                .ForMember(web => web.IsPalm, x => x.MapFrom(info => info.IsPalm))
                .ForMember(web => web.LatentNumber, x => x.MapFrom(info => info.LatentNumber))
                .ForMember(web => web.RegistrationNumber, x => x.MapFrom(info => info.RegistrationNumber))
                .ForMember(web => web.Queries, x => x.MapFrom(info => info.QueryStatusInfos))
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<QueryStatusInfo, QueryStatusWeb>()
                .ForMember(web => web.QueryId, x => x.MapFrom(info => info.QueryId))
                .ForMember(web => web.Status, x => x.MapFrom(info => info.LocalStatusLex))
                .ForAllOtherMembers(info => info.Ignore());
        }
    }
}
