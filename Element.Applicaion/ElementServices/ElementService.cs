using AutoMapper;
using Element.Applicaion.IElementServices;
using Element.Applicaion.ViewModels;
using Element.Core.Bus;
using Element.Domain.Commands;
using Element.Domain.Events.Merchant;
using Element.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Applicaion.ElementServices
{
    public class ElementService : IElementService
    {
        private readonly IMapper _Mapper;
        // 中介者 总线
        private readonly IMediatorHandler _Bus;

        private readonly IMerchantRepository _MerchantRepository;

        public ElementService(IMapper Mapper, IMediatorHandler Bus, IMerchantRepository MerchantRepository)
        {
            _Mapper = Mapper;
            _Bus = Bus;
            _MerchantRepository = MerchantRepository;
        }

        public IEnumerable<MerchantViewModel> GetAll()
        {
            return _Mapper.Map<IEnumerable<MerchantViewModel>>(_MerchantRepository.GetAll(o=>o.Id!=null));
        }

        public async Task<MerchantViewModel> GetMerchantViewModelById(Guid id)
        {
            return   _Mapper.Map<MerchantViewModel>( await _MerchantRepository.GetModelAsync(o => o.Id != null));
        }

        public async Task ResiterMerchant(MerchantViewModel merchantViewModel)
        {
            var registerCommand = _Mapper.Map<MerchantCommands>(merchantViewModel);
            await _Bus.SendCommand(registerCommand);

        }
    }
}
