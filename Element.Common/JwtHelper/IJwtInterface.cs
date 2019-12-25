using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Element.Common
{
  public  interface IJwtInterface
    {

        string IssueJwt(TokenModelJwt tokenModel);



        TokenModelJwt SerializeJwt(string jwtStr);

    }
}
