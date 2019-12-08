using System;
using System.Collections.Generic;
using System.Text;
using Element.Core.Events;

namespace Element.Core.Events
{
    public class MerchantRegisteredEvent : Event
    {
        public MerchantRegisteredEvent(Guid id, string name, DateTime birthDate, string phone, string MerchantIdCard)
        {
            Id = id;
            Name = name;

            BirthDate = birthDate;
            Phone = phone;
            AggregateId = id;

            this.MerchantIdCard = MerchantIdCard;
        }

        public string MerchantIdCard { get; private set; }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string Phone { get; private set; }
    }
}
