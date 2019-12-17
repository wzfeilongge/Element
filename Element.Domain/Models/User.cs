using Element.Core.CoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Models
{
    public class User : Entity
    {   
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; private set; }


        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public  string Password { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }



        public User()
        {

        }
        public User(Guid Id,string IdCard,string Name,string Address,string Phone,string Password,string Email)
        {
            this.Id = Id;
            this.IdCard = IdCard;
            this.Name = Name;
            this.Address = Address;
            this.Phone = Phone;
            this.Password = Password;
            this.Email = Email;
        }




    }
}
