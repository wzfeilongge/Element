using Element.Common.Common;
using Element.Core.ObjectCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Element.Common.SeedData
{
    public static class DBSeed
    {
     

       public static  bool GetSeedData()
        {

            string[] str = new string[] {

                "IsSeedDefaultData"
            };



            return Appsettings.app(str).ObjToBool();

        }   
       





















    }
}
