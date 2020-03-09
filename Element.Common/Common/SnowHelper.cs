using System;
using System.Collections.Generic;
using Snowflake.Core;
using System.Text;

namespace Element.Common.Common
{
    public static class SnowHelper
    {
        private static long UnionId { get; } = 1;

        private static readonly IdWorker _Worker = new IdWorker(UnionId, 1, 10);


        public static long GetSnowId()
        {
            try
            {
                return (_Worker.NextId());
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return 0;

            }
        }


    }
}
