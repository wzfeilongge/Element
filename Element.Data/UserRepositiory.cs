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
    public class UserRepositiory : BaseRepository<User>, IUserRepository
    {
        public UserRepositiory(ILogger<BaseRepository<User>> logger):base(logger)
        {

        }     
    }
}
