using Element.Common.Common;
using Element.Core.Bus;
using Element.Core.Events;
using Element.Core.Notifications;
using Element.Domain.Commands;
using Element.Domain.Interface;
using Element.Domain.Models;
using Element.Infra.Data.UnitofWorkDB;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Element.Domain.CommandHandler
{
    public class MerchantCommandsHandlers : CommandHandlers, IRequestHandler<MerchantCommands, Unit>
    {
        private readonly IMediatorHandler Bus;

        private readonly IMerchantRepository _MerchantRepository;


        public MerchantCommandsHandlers(IUnitOfWork uow, IMediatorHandler bus, IMerchantRepository MerchantRepository) : base(uow, bus)
        {
            Bus = bus;
            _MerchantRepository = MerchantRepository;

        }


        public async Task<Unit> Handle(MerchantCommands request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
            {
                // 错误信息收集
                NotifyValidationErrors(request);
                // 返回，结束当前线程
                return await Task.FromResult(new Unit());
            }

            var address = new Address(request.Province, request.City, request.County, request.Street);
            var Merchant = new Merchant(Guid.NewGuid(), request.MerchantName, request.Phone, request.BirthDate, request.MerchantIdCard, address, Encrypt.EncryptPassword(request.Password));
            var iserror = await _MerchantRepository.GetByCardIdorName(Merchant.MerchantIdCard,Merchant.MerchantName);
            if (iserror != null)
            {
                await Bus.RaiseEvent(new DomainNotification("", "该身份证号或者用户名已经被使用！"));
                return await Task.FromResult(new Unit());
            }
            var count = await _MerchantRepository.AddModel(Merchant);
            if (count > 0)
            {
                if (Commit())
                {
                    // 提交成功后，这里需要发布领域事件
                    // 比如欢迎用户注册邮件呀，短信呀等
                    await Bus.RaiseEvent(new MerchantRegisteredEvent(Merchant.Id, Merchant.MerchantName, Merchant.BirthDate, Merchant.Phone, Merchant.MerchantIdCard));
                }
            }
            return await Task.FromResult(new Unit());
        }
    }
}
