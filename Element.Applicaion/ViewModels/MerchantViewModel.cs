﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Element.Applicaion.ViewModels
{
    public class MerchantViewModel
    {

        [Key]
        public Guid Id { get; set; }

        
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "The Name is Required")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required(ErrorMessage = "The BirthDate is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Date in invalid format")]
        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }



        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "The Phone is Required")]
        [Phone]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Required(ErrorMessage = "The IdCard is Required")]
        public string MerchantIdCard { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [Required(ErrorMessage = "The Province is Required")]
        [DisplayName("Province")]
        public string Province { get; set; }


        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }

    }
}
