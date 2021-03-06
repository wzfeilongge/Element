﻿using Element.Domain.Models;
using Element.Infra.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Domain.Interface
{
    public interface IRoleManngeRepository : IBaseRepository<RoleMannage>
    {

        Task<bool> AddRole(Guid id,string RoleName);


    }
}
