using Element.Core.CoreModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Element.Domain.Models
{
    public class Merchant : Entity
    {
        public Merchant()
        {

        }

        public Merchant(Guid Id, string MerchantName, string Phone, DateTime BirthDate, string MerchantIdCard, Address Address,string Password)
        {
            this.Id = Id;
            this.MerchantIdCard = MerchantIdCard;
            this.Phone = Phone;
            this.BirthDate = BirthDate;
            this.MerchantName = MerchantName;
            this.Address = Address;
            this.Password = Password;
        }

        public Address Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
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


        public string Password { get; private set; }
    }
}
