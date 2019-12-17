using Element.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Validations.User
{
   public class UserRegisterCardVaildation: UserVaildation<UserCommand>
    {

        public UserRegisterCardVaildation()
        {
            //认证身份证号码,手机号码，用户名,邮箱,密码
            ValidateIdCard();
            ValidatePhone();
            ValidateName();
            ValidateEmail();
            ValidatePwd();
        }




    }
}
