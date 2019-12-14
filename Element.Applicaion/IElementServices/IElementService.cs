using Element.Applicaion.ViewModels;
using Element.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Applicaion.IElementServices
{
    public interface IElementService
    {
        /// <summary>
        /// 注册商户
        /// </summary>
        /// <param name="merchantViewModel"></param>
        Task ResiterMerchant(MerchantViewModel merchantViewModel);

        /// <summary>
        /// 查询所有商户
        /// </summary>
        /// <returns></returns>
        IEnumerable<MerchantViewModel> GetAll();

        /// <summary>
        /// 根据ID查询商户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MerchantViewModel> GetMerchantViewModelById(Guid id);

        Task<RoleMannage> GetRoleModel(Guid id);
    }
}
