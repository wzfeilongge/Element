using Element.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Events.UserEvent
{
  public  class ChangePwd:Event
    {

        public string Name { get; private set; }

        public string Email { get; set; }

        public ChangePwd(string Name,string Email)
        {
            this.Name = Name;
            this.Email = Email;

        }





    }
}
