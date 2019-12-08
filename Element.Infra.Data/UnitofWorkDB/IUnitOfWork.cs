using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Infra.Data.UnitofWorkDB
{
    public interface IUnitOfWork
    {
        //是否提交成功
        bool Commit();
    }
}
