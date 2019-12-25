using AutoMapper;
using Element.Applicaion.ViewModels;
using Element.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Applicaion.AutoMapper
{
    public class DomainToViewModel : Profile
    {

        public DomainToViewModel()
        {
            CreateMap<Merchant, MerchantViewModel>()
            .ForMember(m => m.County, o => o.MapFrom(v => v.Address.County))
            .ForMember(m => m.Province, o => o.MapFrom(v => v.Address.Province))
            .ForMember(m => m.City, o => o.MapFrom(v => v.Address.City))
            .ForMember(m => m.Street, o => o.MapFrom(v => v.Address.Street))
            .ForMember(m => m.Password, o => o.MapFrom(v => v.Password));

            CreateMap<User, UserViewModel>()
           .ForMember(m => m.IdCard, o => o.MapFrom(v => v.IdCard))
           .ForMember(m => m.Phone, o => o.MapFrom(v => v.Phone))
           .ForMember(m => m.UserName, o => o.MapFrom(v => v.Name));

            CreateMap<User, UserDto>()
           .ForMember(m => m.UserName, o => o.MapFrom(v => v.Name))
           .ForMember(m => m.Email, o => o.MapFrom(v => v.Email))
           .ForMember(m => m.Phone, o => o.MapFrom(v => v.Phone));






        }
    }
}
