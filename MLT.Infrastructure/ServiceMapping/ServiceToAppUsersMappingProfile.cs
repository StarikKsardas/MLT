using AutoMapper;
using MLT.Desktop.AppUsers.Contracts.ViewModels;
using MLT.Domain.Contracts.InfoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Infrastructure.ServiceMapping
{
    public class ServiceToAppUsersMappingProfile: Profile
    {
        public ServiceToAppUsersMappingProfile()
        {
            CreateMap<UserView, UserInfo>()
                .ForMember(info => info.AbrPlace, x => x.MapFrom(view => view.Place))
                .ForMember(info => info.FirstName, x => x.MapFrom(view => view.FirstName))
                .ForMember(info => info.Id, x => x.MapFrom(view => view.Id))
                .ForMember(info => info.LastName, x => x.MapFrom(view => view.LastName))
                .ForMember(info => info.Login, x => x.MapFrom(view => view.Login))
                .ForMember(info => info.MidName, x => x.MapFrom(view => view.MidName))
                .ForMember(info => info.Password, x => x.MapFrom(view => view.Password))
                .ForMember(info => info.Phone, x => x.MapFrom(view => view.Phone))
                .ForMember(info => info.PhoneId, x => x.MapFrom(view => view.PhoneId))
                .ForMember(info => info.PlaceCode, x => x.MapFrom(view => view.PlaceCode))
                .ForMember(info => info.PlaceCodeLex, x => x.MapFrom(view => view.PlaceCodeLex))
                .ReverseMap()
                .ForAllOtherMembers(info => info.Ignore());

            CreateMap<LoginView, UserInfo>()
                .ForMember(info => info.Login, x => x.MapFrom(view => view.Login))
                .ForMember(info => info.Password, x => x.MapFrom(view => view.Password))
                .ForAllOtherMembers(info => info.Ignore());


        }
    }
}
