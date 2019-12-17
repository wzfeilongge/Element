using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Common.ApiAttribute
{

    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public  sealed class CachingAttribute : Attribute
    {
    }
}
