using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Element.Applicaion.ViewModels
{
    public class UserViewModel
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "The Phone is Required")]
        public string Phone { get; set; }


        /// <summary>
        /// 地址
        /// </summary>
       [Required(ErrorMessage = "The Address is Required")]
        public string Address { get; set; }


        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        public string Email { get; set; }


    }
}
