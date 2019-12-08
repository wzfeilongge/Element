using Element.Data.EntityFrameworkCores;
using Element.Infra.Data.UnitofWorkDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Element.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbcontextRepository _DbcontextRepository;



        public UnitOfWork()
        {
            _DbcontextRepository = DbcontextRepository.Context;
        }

        public bool Commit()
        {
           return _DbcontextRepository.SaveChanges() > 0;
        }
    }
}
