using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Core.Events
{
   public class UserRegisterEvent : Event
    {

        public UserRegisterEvent(Guid id, string name, string phone, string address,string IdCard)
        {
            this.Id = id;
            this.Name = name;
            this.Phone = phone;
            this.AggregateId = id;
            this.IdCard = IdCard;
            this.Address = address;
        }

        public Guid Id { get; private set; }
        
        public string Name { get; private set; }
        public  string Phone { get; private set; }
        public  string Address { get; private set; }
        public  string IdCard { get; private set; }






    }
}
