using Element.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Validations.User
{
   public class ChangePwdVaildation : UserChangeVaildation<UserChangePwdCommand>
    {

        public ChangePwdVaildation()
        {
            ValidateName();
            ValidatePwd();

        }



    }
}
