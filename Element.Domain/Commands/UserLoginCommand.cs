using Element.Core.Commands;
using Element.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Commands
{
    public class UserLoginCommand : Command
    {
        public override bool IsValid()
        {
            //UserLoggingVaildation
            ValidationResult = new UserLoggingVaildation().Validate(this);

            return ValidationResult.IsValid;
        }


        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get;  private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }


        public UserLoginCommand(string UserName, string Password)
        {
            this.Password = Password;
            this.UserName = UserName;

        }



    }
}
