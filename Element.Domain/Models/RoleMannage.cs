using Element.Core.CoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Domain.Models
{
   public class RoleMannage:Entity
    {

        public RoleMannage(Guid Id,string RoleName,bool IsTrueRold)
        {
            this.Id = Id;
            this.RoleName = RoleName;
            this.CreateTime = DateTime.Now;
            this.IsTrueRold = IsTrueRold;
        }

        public RoleMannage()
        {

        }


        /// <summary>
        /// 名称
        /// </summary>
        public string RoleName { get;  private set; }

        /// <summary>
        /// 权限创建时间
        /// </summary>
        public DateTime CreateTime { get; }


        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsTrueRold { get; set; }


    }
}
