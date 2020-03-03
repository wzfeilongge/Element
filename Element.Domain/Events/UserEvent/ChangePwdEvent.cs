using Element.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Events.UserEvent
{
  public  class ChangePwdEvent:Event
    {
        public string Name { get; private set; }

        public string Email { get;  private set; }

        public string Ip { get; private set; }

        public ChangePwdEvent(Guid Id,string Name,string Email, string Ip)
        {
            this.Name = Name;
            this.Email = Email;
            this.AggregateId = Id;
            this.Ip = Ip;
        }





    }
}
