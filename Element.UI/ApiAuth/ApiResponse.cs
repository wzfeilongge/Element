using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Element.UI.ApiAuth
{
    public class ApiResponse
    {

        public int Code { get; set; } = 404;
        public object Msg { get; set; } = "No Found";

        public ApiResponse(StatusCode apiCode)
        {
            switch (apiCode)
            {
                case StatusCode.CODE401:
                {
                    Code = 401;
                    Msg = "很抱歉，您无权访问该接口，请确保已经登录!";
                }
                break;
                case StatusCode.CODE403:
                {
                    Code = 403;
                    Msg = "很抱歉，您的访问权限等级不够，联系管理员!";
                }
                break;
                case StatusCode.CODE500:
                {
                    Code = 500;
                    Msg = "很抱歉,服务器出现错误!";
                }
                break;
            }
        }
    }


    public enum StatusCode
    {
        CODE401,
        CODE403,
        CODE404,
        CODE500
    }
}
