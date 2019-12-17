using AutoMapper;
using Element.Applicaion.IElementServices;
using Element.Applicaion.ViewModels;
using Element.Common.Common;
using Element.Core.Bus;
using Element.Domain.Commands;
using Element.Domain.Interface;
using Element.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Applicaion.ElementServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _Mapper;
        // 中介者 总线
        private readonly IMediatorHandler _Bus;

        private readonly IUserRepository _UserRepository;
        public UserService(IMapper Mapper, IMediatorHandler Bus, IUserRepository UserRepository)
        {
            _Mapper = Mapper;
            _Bus = Bus;
            _UserRepository = UserRepository;
        }

        public async Task ChangePwd(UserViewModel userViewModel)
        {
            var EditCommand = _Mapper.Map<UserChangePwdCommand>(userViewModel);

            await  _Bus.SendCommand(EditCommand);

             return  ;
        }

        public List<UserDto> GetDto(List<User> users)
        {        
            return _Mapper.Map<List<UserDto>>(users);
        }

        public  async Task<List<User>> GetUserAll()
        {
            return   await _UserRepository.GetAll(u=>u.Id!=null).ToListAsync();
        }

        public  async Task<User> GetUserById(UserViewModel userViewModel)
        {
            userViewModel.Password = Encrypt.EncryptPassword(userViewModel.Password);
           return await _UserRepository.GetModelAsync(u=>u.Name==userViewModel.UserName&&u.Password==userViewModel.Password);
        }

        public async Task Register(UserViewModel userViewModel)
        {
           var registerCommand = _Mapper.Map<UserCommand>(userViewModel);

           await _Bus.SendCommand(registerCommand);

        }
    }
}
