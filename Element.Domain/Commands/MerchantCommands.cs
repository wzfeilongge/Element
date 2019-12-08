using Element.Core.Commands;
using Element.Domain.Validations.Merchant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Commands
{
    public class MerchantCommands : Command
    {
        public DateTime BirthDate { get; private set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; private set; }

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
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; private set; }


        public string Password { get;  private set; }


        public MerchantCommands(string MerchantName, string Phone, string Province, string Street, string MerchantIdCard, string County, string City, DateTime BirthDate,string Password)
        {

            this.MerchantIdCard = MerchantIdCard;
            this.MerchantName = MerchantName;
            this.Phone = Phone;
            this.Province = Province;
            this.Street = Street;
            this.County = County;
            this.City = City;
            this.BirthDate = BirthDate;
            this.Password = Password;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterMerchantCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
