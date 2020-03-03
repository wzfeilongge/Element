using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Element.Common.HttpComm
{
   public static  class Extension
    {
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
