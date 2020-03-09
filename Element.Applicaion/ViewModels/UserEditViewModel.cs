using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Applicaion.ViewModels
{
    public class UserEditViewModel
    {

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }


        /// <summary>
        /// 新密码
        /// </summary>
        public string NewPasswords { get; set; }


        /// <summary>
        /// 修改密码
        /// </summary>
        public string SecendPassword { get; set; }



        /// <summary>
        /// Ip
        /// </summary>
        public string Ip { get; set; }



    }
}
