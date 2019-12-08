using Element.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Validations.Merchant
{
    public class MerchantVaildation<T> : AbstractValidator<T> where T : MerchantCommands
    {
        protected void ValidateName()
        {
            RuleFor(c => c.MerchantName)
                .NotEmpty().WithMessage("姓名不能为空")//判断不能为空，如果为空则显示Message
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");//定义 Name 的长度
        }


        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)//Must 表示必须满足某一个条件，参数是一个bool类型的方法，更像是一个委托事件
                .WithMessage("商户应该20岁以上！");
        }

        protected void ValidatePhone()
        {
            RuleFor(c => c.Phone)
                .NotEmpty()
                .Must(HavePhone)
                .WithMessage("手机号应该为11位！");
        }

        protected void ValidateIdCard()
        {
            RuleFor(c => c.MerchantIdCard)
                .NotEmpty()
                .Must(CheckIDCard18)
                .WithMessage("请输入18位或15位正确身份证号码");
        }

        // 表达式
        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-14);
        }
        // 表达式
        protected static bool HavePhone(string phone)
        {
            return phone.Length == 11;
        }


        #region 认证身份证

        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
           // var flag = false;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return CheckIDCard15(Id);
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return CheckIDCard15(Id);
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
               return CheckIDCard15(Id);
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return CheckIDCard15(Id); ;
            }
            return true;//正确
        }

        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;
            }
            return true;//正确
        }


        #endregion

    }
}
