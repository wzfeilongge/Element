using Element.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Events.UserEvent
{
    public class RegisterUserEvent : Event
    {

        public RegisterUserEvent(Guid Id,string IdCard, string Phone, string Address, string Name)
        {
            this.IdCard = IdCard;
            this.Phone = Phone;
            this.Address = Address;
            this.Name = Name;
            this.AggregateId = Id;
        }

        public string IdCard { get; private set; }

        public string Phone { get; private set; }

        public string Address { get; private set; }

        public string Name { get; private set; }



    }
}
