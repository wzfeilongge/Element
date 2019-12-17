using Element.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace Element.Domain.Validations.User
{
   public class UserChangeVaildation<T> : AbstractValidator<T> where T : UserChangePwdCommand
    {
        /// <summary>
        /// 包含小写字母
        /// </summary>
        private const string REG_CONTAIN_LOWERCASE_ASSERTION =
            @"(?=.*[a-z])";

        /// <summary>
        /// 包含大写字母
        /// </summary>
        private const string REG_CONTAIN_UPPERCASE_ASSERTION =
            @"(?=.*[A-Z])";

        /// <summary>
        /// 包含数字
        /// </summary>
        private const string REG_CONTAIN_DIGIT_ASSERTION =
            @"(?=.*\d)";


        protected void ValidateName()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("姓名不能为空")//判断不能为空，如果为空则显示Message
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");//定义 Name 的长度
        }

        protected void ValidatePwd()
        {
            RuleFor(c => c.NewPassword)
                .NotNull()
                .Must(HasPwd)
                .WithMessage("必须包含数字小写英文");
            RuleFor(c=>c.OldPassword)
                 .NotNull()
                .Must(HasPwd)
                .WithMessage("必须包含数字小写英文");
            RuleFor(c => c.NewPasswords)
              .NotNull()
             .Must(HasPwd)
             .WithMessage("必须包含数字小写英文");
        }


        #region 验证密码规则

        public static bool HasPwd(string source)
        {
            return Regex.IsMatch(source, REG_CONTAIN_LOWERCASE_ASSERTION, RegexOptions.IgnoreCase) == true
                //   && Regex.IsMatch(source, REG_CONTAIN_UPPERCASE_ASSERTION, RegexOptions.IgnoreCase) == true
                && Regex.IsMatch(source, REG_CONTAIN_DIGIT_ASSERTION, RegexOptions.IgnoreCase) == true;
        }
        #endregion



    }
}
