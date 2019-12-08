using AutoMapper;
using Element.Applicaion.IElementServices;
using Element.Applicaion.ViewModels;
using Element.Core.Bus;
using Element.Domain.Commands;
using Element.Domain.Interface;
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

        public async Task Register(UserViewModel userViewModel)
        {
            var registerCommand = _Mapper.Map<UserCommand>(userViewModel);

           await _Bus.SendCommand(registerCommand);

        }
    }
}
