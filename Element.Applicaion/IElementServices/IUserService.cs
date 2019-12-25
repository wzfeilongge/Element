using Element.Applicaion.EventSourcedNormalizers;
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
        Task Register(RegisterViewModel userViewModel);


        Task Login(UserLoginModel loginModel);


        Task<User> GetUserById(UserLoginModel userViewModel);

        Task<List<User>> GetUserAll();

        List<UserDto> GetDto(List<User> users);


        Task ChangePwd(UserEditViewModel userViewModel);



        Task<IList<UserHistoryData>> GetAllHistory(Guid id);



    }
}
