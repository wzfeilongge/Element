using AutoMapper;
using Element.Applicaion.ViewModels;
using Element.Domain.Commands;
using Element.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Applicaion.AutoMapper
{
    public class ViewModelToDomain : Profile
    {

        public ViewModelToDomain()
        {

            CreateMap<MerchantViewModel, Merchant>()
                .ForPath(v => v.Address.Province, o => o.MapFrom(m => m.Province))
                .ForPath(v => v.Address.City, o => o.MapFrom(m => m.City))
                .ForPath(v => v.Address.County, o => o.MapFrom(m => m.County))
                .ForPath(v => v.Address.Street, o => o.MapFrom(m => m.Street));
            CreateMap<MerchantViewModel, MerchantCommands>().ConstructUsing
                (c => new MerchantCommands(c.Name, c.Phone, c.Province, c.Street, c.MerchantIdCard, c.County, c.City, c.BirthDate,c.Password));


            CreateMap<UserViewModel, User>()
                .ForMember(v => v.Address, o => o.MapFrom(m => m.Address));
            CreateMap<UserViewModel, UserCommand>().ConstructUsing
                (c=> new UserCommand(c.UserName,c.IdCard,c.Address,c.Phone,c.Password,c.Email));


            CreateMap<UserViewModel, UserChangePwdCommand>().ConstructUsing
                (c => new UserChangePwdCommand(c.UserName, c.SecendPassword, c.NewPasswords, c.Password));


        }
    }
}
