using Element.Applicaion.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Applicaion.IElementServices
{
   public interface IUserService
    {
        Task Register(UserViewModel userViewModel);



    }
}
