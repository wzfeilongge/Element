using Element.Common;
using Element.Common.Common;
using Element.Core.Bus;
using Element.Core.Events;
using Element.Core.Notifications;
using Element.Domain.Commands;
using Element.Domain.Events.UserEvent;
using Element.Domain.Interface;
using Element.Domain.Models;
using Element.Infra.Data.UnitofWorkDB;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Element.Domain.CommandHandler
{
    public class UserCommandHandlers : CommandHandlers,
        IRequestHandler<UserCommand, Unit>,
        IRequestHandler<UserChangePwdCommand, Unit>,
        IRequestHandler<UserLoginCommand, Unit>


    {

        private readonly IMediatorHandler _Bus;
        private readonly IJwtInterface _JwtInterface;
        private readonly IRoleManngeRepository _RoleManngeRepository;
        private readonly IUserRepository _UserRepository;
        public UserCommandHandlers(IUnitOfWork uow,
            IMediatorHandler bus,
            IUserRepository UserRepository,
            IRoleManngeRepository RoleManngeRepository,
            IJwtInterface JwtInterface
            ) : base(uow, bus)
        {
            _Bus = bus;
            _UserRepository = UserRepository;
            _RoleManngeRepository = RoleManngeRepository;
            _JwtInterface = JwtInterface;
        }

        public async Task<Unit> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return await Task.FromResult(new Unit());
            }

            var pwd = request.Password;
            var user = new User(Guid.NewGuid(), request.IdCard, request.Name, request.Address, request.Phone, Encrypt.EncryptPassword(request.Password), request.Email);
            var flag = await _UserRepository.GetModelAsync(u => u.IdCard == request.IdCard || u.Name == request.Name || u.Email == request.Email);
            if (flag != null)
            {
                await _Bus.RaiseEvent(new DomainNotification("", "身份证或者用户名或邮箱已经创建过档案！"));
                //用户已经建过档案
                return await Task.FromResult(new Unit());
            }
            var model = await _UserRepository.Add(user);
            var rolemodel = await _RoleManngeRepository.AddRole(model.Id, "Permission");
            if (model != null && rolemodel == true)
            {
                await _Bus.RaiseEvent(new UserRegisterEvent(model.Id, model.Name, model.Phone, model.Address, model.IdCard, model.Email, model.Password));
                return await Task.FromResult(new Unit());
            }
            await _Bus.RaiseEvent(new DomainNotification("", $"插入数据库失败"));
            return await Task.FromResult(new Unit());
        }

        public async Task<Unit> Handle(UserChangePwdCommand request, CancellationToken cancellationToken)
        {
            if (!(request.NewPassword.Equals(request.NewPasswords)))
            {
                await _Bus.RaiseEvent(new DomainNotification("", "两次密码不相同"));
                return await Task.FromResult(new Unit());
            }

            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return await Task.FromResult(new Unit());
            }
            var model = await _UserRepository.GetModelAsync(u => u.Name == request.UserName && u.Password == Encrypt.EncryptPassword(request.OldPassword));
            if (model != null)
            {
                model.Password = Encrypt.EncryptPassword(request.NewPassword);
                var count = await _UserRepository.Modify(model);
                if (count > 0)
                {
                    await _Bus.RaiseEvent(new ChangePwdEvent(model.Id, model.Name, model.Email, request.Ip));
                    return await Task.FromResult(new Unit());

                }
                await _Bus.RaiseEvent(new DomainNotification("", "密码修改失败"));
                return await Task.FromResult(new Unit());
            }
            await _Bus.RaiseEvent(new DomainNotification("", "用户名或者密码输入错误"));
            return await Task.FromResult(new Unit());
        }

        public async Task<Unit> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                await _Bus.RaiseEvent(new DomainNotification("Sucess", "false"));
                return await Task.FromResult(new Unit());
            }
            var model = await _UserRepository.GetModelAsync(u => u.Name == request.UserName && u.Password == Encrypt.EncryptPassword(request.Password));
            if (model != null)
            {
                var role = await _RoleManngeRepository.GetModelAsync(u => u.Id.Equals(model.Id));
                if (role != null)
                {
                    TokenModelJwt t = new TokenModelJwt
                    {
                        Role = role.RoleName,
                        Uid = ((role.Id)),
                        Name = model.Name,
                    };
                    var token = _JwtInterface.IssueJwt(t);
                    await _Bus.RaiseEvent(new DomainNotification("Sucess", "true"));
                    await _Bus.RaiseEvent(new DomainNotification("data", token));
                    return await Task.FromResult(new Unit());
                }
            }
            await _Bus.RaiseEvent(new DomainNotification("Sucess", "false"));
            await _Bus.RaiseEvent(new DomainNotification("data", "登录失败"));

            return await Task.FromResult(new Unit());
        }
    }
}
