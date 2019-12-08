using Element.Core.Bus;
using Element.Core.Events;
using Element.Core.Notifications;
using Element.Domain.Commands;
using Element.Domain.Interface;
using Element.Domain.Models;
using Element.Infra.Data.UnitofWorkDB;
using MediatR;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Element.Domain.CommandHandler
{
    public class UserCommandHandlers : CommandHandlers, IRequestHandler<UserCommand, Unit>
    {

        private readonly IUserRepository _UserRepository;
        private readonly IMediatorHandler _Bus;

        public UserCommandHandlers(IUnitOfWork uow, IMediatorHandler bus, IUserRepository UserRepository) :base(uow,bus)
        {
            _Bus = bus;
            _UserRepository = UserRepository;
        }

        public  async Task<Unit> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return await Task.FromResult(new Unit());
            }

            var user = new User(Guid.NewGuid(),request.IdCard,request.Name,request.Address,request.Phone);
            var flag = await _UserRepository.GetModelAsync(u => u.IdCard == request.IdCard);
            if (flag!=null) {

                await _Bus.RaiseEvent(new DomainNotification("", "用户已经创建过档案！"));
                //用户已经建过档案
                return await Task.FromResult(new Unit());
            }
            var model = await _UserRepository.Add(user);
            if (model!=null)
            {
                await _Bus.RaiseEvent(new UserRegisterEvent(model.Id, model.Name, model.Phone, model.Address, model.IdCard));
                return await Task.FromResult(new Unit());
            }
            await _Bus.RaiseEvent(new DomainNotification("", "插入数据库失败"));
            return   await Task.FromResult(new Unit());
        }
    }
}
