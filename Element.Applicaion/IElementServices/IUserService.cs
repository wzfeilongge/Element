using Element.Applicaion.ViewModels;
using Element.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Applicaion.IElementServices
{
   public interface IUserService
    {
        Task Register(UserViewModel userViewModel);


        Task<User> GetUserById(UserViewModel userViewModel);



    }
}
