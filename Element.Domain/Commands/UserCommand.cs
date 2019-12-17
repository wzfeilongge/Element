using Element.Core.Commands;
using Element.Domain.Validations.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Commands
{
    public class UserCommand : Command
    {
        public override bool IsValid()
        {
            ValidationResult = new UserRegisterCardVaildation().Validate(this);
            return ValidationResult.IsValid;
        }

        public string Name { get; private set; }


        public string IdCard { get; private set; }

        public string Address { get; private set; }

        public string Password { get; private set; }

        public string Phone { get; private set; }

        public string Email { get; private set; }


        public UserCommand(string Name, string idCard, string Address, string Phone,string Password,string Email)
        {

            this.Name = Name;
            this.IdCard = idCard;
            this.Address = Address;
            this.Phone = Phone;
            this.Password = Password;
            this.Email = Email;

        }

    }


    
}
