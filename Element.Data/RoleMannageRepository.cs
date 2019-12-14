using Element.Data.EntityFrameworkCores;
using Element.Domain.Interface;
using Element.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Data
{
   public class RoleMannageRepository: BaseRepository<RoleMannage>,IRoleManngeRepository
    {
        public RoleMannageRepository(ILogger<BaseRepository<RoleMannage>> logger):base(logger)
        {

        }

        public async Task<bool> AddRole(Guid id,string RoleName)
        {
            var role=await base.GetModelAsync(r=>r.Id==id);
            if (role==null) 
            {
                
             var result=  await   base.AddModel(new RoleMannage(id, RoleName, true));
                if (result>0)
                {
                    return true;

                }           
            }

            return false;
        }
    }
}
