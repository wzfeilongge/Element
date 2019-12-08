using Element.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Validations.Merchant
{
   public class RegisterMerchantCommandValidation:MerchantVaildation<MerchantCommands>
    {



        public RegisterMerchantCommandValidation()
        {
            ValidateName();//验证姓名
            ValidateBirthDate();//验证年龄
            ValidateIdCard();//验证身份证号
            ValidatePhone();//验证手机号
        }
    }
}
