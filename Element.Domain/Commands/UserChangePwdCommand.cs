using Element.Core.Commands;
using Element.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Commands
{
    public class UserChangePwdCommand : Command
    {   
        public string OldPassword { get; private set; }

        public string NewPasswords { get; private set; }

        public string NewPassword { get; private set; }

        public string UserName { get;  private set; }

        public string Ip { get;  private set; }


        public UserChangePwdCommand(string UserName, string NewPassword, string NewPasswords, string OldPassword, string Ip)
        {
            this.UserName = UserName;
            this.NewPassword = NewPassword;
            this.NewPasswords = NewPasswords;
            this.OldPassword = OldPassword;
            this.Ip = Ip;
        }

        public override bool IsValid()
        {
            ValidationResult = new ChangePwdVaildation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
