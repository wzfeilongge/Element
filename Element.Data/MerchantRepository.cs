using Element.Data.EntityFrameworkCores;
using Element.Domain.Interface;
using Element.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Data
{
    public class MerchantRepository : BaseRepository<Merchant>, IMerchantRepository
    {

        public MerchantRepository(ILogger<BaseRepository<Merchant>> logger):base(logger)
        {

        }

        public  async Task<Merchant> GetByCardIdorName(string MerchantId,string Name)
        {
            return  await base.GetModelAsync(model=>model.MerchantIdCard==MerchantId||model.MerchantName==Name);
        }
    }
}
