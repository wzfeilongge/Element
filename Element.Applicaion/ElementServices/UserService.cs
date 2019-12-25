using AutoMapper;
using Element.Applicaion.EventSourcedNormalizers;
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


        private readonly IEventStoreRepository _EventStoreRepository;
        public UserService(IMapper Mapper, IMediatorHandler Bus, IUserRepository UserRepository, IEventStoreRepository EventStoreRepository)
        {
            _Mapper = Mapper;
            _Bus = Bus;
            _UserRepository = UserRepository;
            _EventStoreRepository = EventStoreRepository;
        }

        public async Task ChangePwd(UserEditViewModel userViewModel)
        {
            var EditCommand = _Mapper.Map<UserChangePwdCommand>(userViewModel);

            await _Bus.SendCommand(EditCommand);

            return;
        }

        public async Task<IList<UserHistoryData>> GetAllHistory(Guid id)
        {


            return UserHistory.ToJavaScriptUserHistory(await _EventStoreRepository.All(id));
        }

        public List<UserDto> GetDto(List<User> users)
        {
            return _Mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<User>> GetUserAll()
        {
            return await _UserRepository.GetAll(u => u.Id != null).ToListAsync();
        }

        public async Task<User> GetUserById(UserLoginModel userViewModel)
        {
            userViewModel.Password = Encrypt.EncryptPassword(userViewModel.Password);
            return await _UserRepository.GetModelAsync(u => u.Name == userViewModel.UserName && u.Password == userViewModel.Password);
        }

        public async Task Login(UserLoginModel loginModel)
        {
            UserLoginCommand command = _Mapper.Map<UserLoginCommand>(loginModel);

            await _Bus.SendCommand(command);

        }

        public async Task Register(RegisterViewModel userViewModel)
        {
            var registerCommand = _Mapper.Map<UserCommand>(userViewModel);

            await _Bus.SendCommand(registerCommand);

        }

     
    }
}
