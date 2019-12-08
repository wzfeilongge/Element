using Element.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Events.Merchant
{
    public class RegisterMerchant : Event
    {
        public RegisterMerchant(Guid id, string name, string email, DateTime birthDate, string phone)
        {
            this.MerchantIdCard = MerchantIdCard;
            this.Phone = Phone;
            this.BirthDate = BirthDate;
            this.MerchantName = MerchantName;
            AggregateId = id;
        }

        public DateTime BirthDate { get; private set; }

        //商户身份证号
        public string MerchantIdCard { get; private set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string MerchantName { get; private set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; private set; }




    }
}
